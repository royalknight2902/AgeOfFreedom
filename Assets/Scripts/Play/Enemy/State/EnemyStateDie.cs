using UnityEngine;
using System.Collections;

public class EnemyStateDie : FSMState<EnemyController>
{
    public override void Enter(EnemyController obj)
    {
        obj.transform.GetChild(1).gameObject.SetActive(false);

        //remove from list
        //Dragon
        if (PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>().dragonAttack.listEnemy.Contains(obj.gameObject))
        {
            PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>().dragonAttack.listEnemy.Remove(obj.gameObject);
        }

        //list baby dragon
        for (int i = 0; i < PlayDragonManager.Instance.listBabyDragon.Count; i++)
        {
            if (PlayDragonManager.Instance.listBabyDragon[i].GetComponent<BabyDragonController>().babyAttack.listEnemy.Contains(obj.gameObject))
            {
                PlayDragonManager.Instance.listBabyDragon[i].GetComponent<BabyDragonController>().babyAttack.listEnemy.Remove(obj.gameObject);
            }
        }
    }

    public override void Execute(EnemyController obj)
    {
    }

    public override void Exit(EnemyController obj)
    {

    }
}

