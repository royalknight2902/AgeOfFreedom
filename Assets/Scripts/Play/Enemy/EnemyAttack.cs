using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    EnemyController controller;

    void Start()
    {
        controller = transform.parent.GetComponent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.Dragon)
        {
            if (controller.attribute.HP.Current <= 0)
                return;

            DragonController dragonController = other.GetComponent<DragonController>();
            if (!controller.isTargeted && !dragonController.isTargeted && dragonController.attribute.HP.Current > 0)
            {
                controller.stateAttack.target = other.gameObject;
                dragonController.isTargeted = true;
                controller.isTargeted = true;

                if (other.transform.position.x < controller.transform.position.x)
                    controller.stateMove.Direction = EDragonStateDirection.RIGHT;
                else
                    controller.stateMove.Direction = EDragonStateDirection.LEFT;

                controller.stateMove.State = EEnemyMovement.MOVE_TO_DRAGON;
            }
        }
        else if (other.tag == TagHashIDs.BabyDragon)
        {
            if (controller.attribute.HP.Current <= 0)
                return;

            BabyDragonController babyController = other.GetComponent<BabyDragonController>();
            if (!controller.isTargeted && !babyController.isTargeted && other.GetComponent<BabyDragonController>().attribute.HP.Current > 0)
            {
                controller.stateAttack.target = other.gameObject;
                babyController.isTargeted = true;
                controller.isTargeted = true;

                if (other.transform.position.x < controller.transform.position.x)
                    controller.stateMove.Direction = EDragonStateDirection.RIGHT;
                else
                    controller.stateMove.Direction = EDragonStateDirection.LEFT;

                controller.stateMove.State = EEnemyMovement.MOVE_TO_DRAGON;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagHashIDs.Dragon)
        {
            controller.StateAction = EEnemyStateAction.MOVE;
            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
            controller.isTargeted = false;
            other.GetComponent<DragonController>().isTargeted = false;
        }
        else if (other.tag == TagHashIDs.BabyDragon)
        {
            controller.StateAction = EEnemyStateAction.MOVE;
            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
            controller.isTargeted = false;
            other.GetComponent<BabyDragonController>().isTargeted = false;
        }
    }
}