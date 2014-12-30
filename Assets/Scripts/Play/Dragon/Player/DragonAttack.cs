using UnityEngine;
using System.Collections;

public class DragonAttack : MonoBehaviour 
{
	DragonController controller;

	void Start()
	{
		controller = transform.parent.GetComponent<DragonController> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == TagHashIDs.EnemyColliderATK)
		{
            if (controller.attribute.HP.Current <= 0)
                return;

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
                //controller.StateAction = EDragonStateAction.MOVE;
                //controller.stateMove.Movement = EDragonMovement.MOVE_TO_ENEMY;
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
                controller.stateMove.Movement = EDragonMovement.MOVE_TOUCH;
                controller.stateAttack.target = null;

                controller.isTargeted = false;
                other.transform.parent.GetComponent<EnemyController>().isTargeted = false;
            }
        }
	}
}
