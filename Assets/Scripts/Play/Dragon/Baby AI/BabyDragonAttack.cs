using UnityEngine;
using System.Collections;

public class BabyDragonAttack : MonoBehaviour {

    private BabyDragonController controller;

    void Start()
    {
        controller = transform.parent.GetComponent<BabyDragonController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.EnemyColliderATK)
        {
            if (controller.attribute.HP.Current <= 0)
            {
                return;
            }

            if (controller.stateAttack.target == null)
            {
                controller.stateAttack.target = other.transform.parent.gameObject;

                controller.isTargeted = true;
                other.transform.parent.GetComponent<EnemyController>().isTargeted = true;

                if (other.transform.position.x < controller.transform.position.x)
                    controller.stateMove.attackDirection = EDragonStateDirection.LEFT;
                else
                    controller.stateMove.attackDirection = EDragonStateDirection.RIGHT;

                controller.StateAction = EDragonStateAction.ATTACK;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagHashIDs.EnemyColliderATK)
        {
            if (controller.stateAttack.target == other.transform.parent.gameObject && controller.attribute.HP.Current > 0)
            {
                controller.StateAction = EDragonStateAction.IDLE;
                controller.stateAttack.target = null;

                controller.isTargeted = false;
                other.transform.parent.GetComponent<EnemyController>().isTargeted = false;
            }
        }
    }
}
