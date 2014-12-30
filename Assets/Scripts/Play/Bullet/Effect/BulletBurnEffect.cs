using UnityEngine;
using System.Collections;

public class BulletBurnEffect : BulletController
{
    public float existTime;
    public float timeFrame;
    public int damageEachFrame;

    public override void initEffect(GameObject enemy)
    {
        bool hasFireEffect = false;
        foreach (Transform child in enemy.transform)
        {
            if(child.name == PlayNameHashIDs.BulletEffectBurning)
            {
                hasFireEffect = true;

                Animator animator = child.GetComponentInChildren<Animator>();
                animator.SetBool("Loop", true);
                animator.SetBool("Finish", false);

                child.GetComponent<BulletEffectController>().reset = true;

                break;
            }
        }

        if(!hasFireEffect)
            BulletEffectManager.Instance.initEffect(EBulletEffect.BURN, enemy, timeFrame, damageEachFrame, existTime);
    }
}
