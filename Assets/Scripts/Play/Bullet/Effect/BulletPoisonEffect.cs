using UnityEngine;
using System.Collections;

public class BulletPoisonEffect : BulletController
{

    public float existTime;
    public float timeFrame;
    public int damageEachFrame;

    public override void initEffect(GameObject enemy)
    {
        bool hasPoisonEffect = false;
        foreach (Transform child in enemy.transform)
        {
            if (child.name == PlayNameHashIDs.BulletEffeftPoisoning)
            {
                hasPoisonEffect = true;
                Animator animator = child.GetComponentInChildren<Animator>();
                animator.SetBool("Loop", true);
                animator.SetBool("Finish", false);

                child.GetComponent<BulletEffectController>().reset = true;

                break;
            }
        }
        if(!hasPoisonEffect)
            BulletEffectManager.Instance.initEffect(EBulletEffect.POISON, enemy, timeFrame, damageEachFrame, existTime);
    }
}