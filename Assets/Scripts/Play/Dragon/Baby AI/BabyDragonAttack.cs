using UnityEngine;
using System.Collections;

public class BabyDragonAttack : MonoBehaviour {

    BabyDragonController controller;

    [HideInInspector]
    public System.Collections.Generic.List<GameObject> listEnemy;

    void Start()
    {
        controller = transform.parent.GetComponent<BabyDragonController>();
        listEnemy = new System.Collections.Generic.List<GameObject>();
    }

    public void attack(GameObject enemy)
    {
        if(enemy == null)
        {
            return;
        }

        if (!listEnemy.Contains(enemy))
            listEnemy.Add(enemy);

        controller.stateAttack.target = enemy;

        if(enemy.GetComponent<EnemyController>().StateAction != EEnemyStateAction.ATTACK)
            enemy.GetComponentInChildren<EnemyAttack>().attack(controller.gameObject);

        //if (other.transform.position.x < controller.transform.position.x)
        //    controller.stateMove.attackDirection = EDragonStateDirection.LEFT;
        //else
        //    controller.stateMove.attackDirection = EDragonStateDirection.RIGHT;

        controller.StateAction = EDragonStateAction.ATTACK;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.ColliderForDragonATK)
        {
            if (controller.attribute.HP.Current <= 0)
                return;

            if (!listEnemy.Contains(other.transform.parent.gameObject))
            {
                listEnemy.Add(other.transform.parent.gameObject);
            }

            if (controller.StateAction == EDragonStateAction.IDLE)
            {
                chooseEnemyToAttack();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagHashIDs.ColliderForDragonATK)
        {
            if (listEnemy.Contains(other.transform.parent.gameObject) && controller.attribute.HP.Current > 0
                && other.transform.parent.GetComponent<EnemyController>().attribute.HP.Current > 0)
            {
                listEnemy.Remove(other.transform.parent.gameObject);

                EnemyController enemy = other.transform.parent.GetComponent<EnemyController>();
                enemy.enemyAttack.listDragon.Remove(controller.gameObject);
                enemy.enemyAttack.chooseDragonToAttack();

                if (controller.StateAction == EDragonStateAction.IDLE)
                    chooseEnemyToAttack();
            }
        }
    }

    public void chooseEnemyToAttack()
    {
        if (controller.stateAttack.target == null && listEnemy.Count > 0)
            attack(listEnemy[0]);
        else
        {
            controller.stateAttack.target = null;

            if (controller.StateAction != EDragonStateAction.MOVE)
                controller.StateAction = EDragonStateAction.IDLE;
        }
    }
}
