using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    EnemyController controller;

    [HideInInspector]
    public System.Collections.Generic.List<GameObject> listDragon;

    void Start()
    {
        controller = transform.parent.GetComponent<EnemyController>();
        listDragon = new System.Collections.Generic.List<GameObject>();
    }

    public void attack(GameObject dragon)
    {
        if (!listDragon.Contains(dragon))
            listDragon.Add(dragon);

        controller.stateAttack.target = dragon.gameObject;

        if (dragon.transform.position.x < controller.transform.position.x)
            controller.stateMove.Direction = EDragonStateDirection.RIGHT;
        else
            controller.stateMove.Direction = EDragonStateDirection.LEFT;

        controller.stateMove.State = EEnemyMovement.MOVE_TO_DRAGON;
    }

    void attackPlayerDragon(DragonController dragon)
    {
        dragon.dragonAttack.listEnemy.Add(this.transform.parent.gameObject);
        dragon.dragonAttack.chooseEnemyToAttack();

        attack(dragon.gameObject);
    }

    void attackBabyDragon(BabyDragonController babyDragon)
    {
        babyDragon.babyAttack.listEnemy.Add(this.transform.parent.gameObject);
        babyDragon.babyAttack.chooseEnemyToAttack();

        attack(babyDragon.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.ColliderForEnemyATK)
        {
            if (controller.attribute.HP.Current <= 0)
                return;

            if (other.transform.parent.GetComponent<DragonController>() != null)
            {
                DragonController dragonController = other.transform.parent.GetComponent<DragonController>();

                if (dragonController.StateOffense == EDragonStateOffense.NONE
                    && dragonController.attribute.HP.Current > 0
                    && !dragonController.dragonAttack.listEnemy.Contains(this.transform.parent.gameObject)
                    && !listDragon.Contains(other.transform.parent.gameObject))
                {
                    //attackPlayerDragon(dragonController);
                    listDragon.Add(other.transform.parent.gameObject);
                }

                if (controller.StateAction == EEnemyStateAction.MOVE && controller.stateAttack.target == null
                    && dragonController.StateAction != EDragonStateAction.ATTACK
                    && dragonController.StateAction != EDragonStateAction.MOVE)
                    chooseDragonToAttack();
            }
            else
            {
                BabyDragonController babyController = other.transform.parent.GetComponent<BabyDragonController>();

                if (babyController.attribute.HP.Current > 0
                    && !babyController.babyAttack.listEnemy.Contains(this.transform.parent.gameObject)
                    && !listDragon.Contains(other.transform.parent.gameObject))
                {
                    //attackPlayerDragon(dragonController);
                    listDragon.Add(other.transform.parent.gameObject);
                }

                if (controller.StateAction == EEnemyStateAction.MOVE && controller.stateAttack.target == null
                    && babyController.StateAction != EDragonStateAction.ATTACK
                    && babyController.StateAction != EDragonStateAction.MOVE)
                    chooseDragonToAttack();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (controller.attribute.HP.Current <= 0)
            return;

        if (other.tag == TagHashIDs.ColliderForEnemyATK)
        {
            if (other.transform.parent.GetComponent<DragonController>() != null)
            {
                if (listDragon.Contains(other.transform.parent.gameObject) && controller.attribute.HP.Current > 0
                    && other.transform.parent.GetComponent<DragonController>().attribute.HP.Current > 0)
                {
                    listDragon.Remove(other.transform.parent.gameObject);

                    if (controller.StateAction == EEnemyStateAction.MOVE)
                        chooseDragonToAttack();
                    else if (controller.StateAction == EEnemyStateAction.ATTACK)
                    {
                        if (controller.stateAttack.target == other.transform.parent.gameObject
                            && listDragon.Count == 0)
                        {
                            controller.stateAttack.target = null;
                            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
                            controller.StateAction = EEnemyStateAction.MOVE;
                        }
                    }
                }
            }
            else
            {
                if (listDragon.Contains(other.transform.parent.gameObject) && controller.attribute.HP.Current > 0
                    && other.transform.parent.GetComponent<BabyDragonController>().attribute.HP.Current > 0)
                {
                    listDragon.Remove(other.transform.parent.gameObject);

                    if (controller.StateAction == EEnemyStateAction.MOVE)
                        chooseDragonToAttack();
                    else if (controller.StateAction == EEnemyStateAction.ATTACK)
                    {
                        if (controller.stateAttack.target == other.transform.parent.gameObject
                            && listDragon.Count == 0)
                        {
                            controller.stateAttack.target = null;
                            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
                            controller.StateAction = EEnemyStateAction.MOVE;
                        }
                    }
                }
            }
        }
    }

    public void chooseDragonToAttack()
    {
        if (controller.stateAttack.target == null && listDragon.Count > 0)
        {
            bool hasTarget = false;
            for (int i = 0; i < listDragon.Count; i++)
            {
                if (listDragon[i] == null)
                    break;

                if (listDragon[i].activeSelf)
                {
                    if (listDragon[i].GetComponent<DragonController>() != null)
                    {
                        DragonController dragon = listDragon[i].GetComponent<DragonController>();
                        if (dragon.StateAction == EDragonStateAction.IDLE)
                        {
                            attackPlayerDragon(dragon);
                            break;
                        }
                    }
                    else
                    {
                        BabyDragonController baby = listDragon[i].GetComponent<BabyDragonController>();
                        if (baby.StateAction == EDragonStateAction.IDLE)
                        {
                            attackBabyDragon(baby);
                            break;
                        }
                    }
                }
            }

            if (hasTarget)
            {
                controller.stateAttack.target = null;
                controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
                controller.StateAction = EEnemyStateAction.MOVE;
            }
        }
        else
        {
            controller.stateAttack.target = null;
            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
            controller.StateAction = EEnemyStateAction.MOVE;
        }
    }
}