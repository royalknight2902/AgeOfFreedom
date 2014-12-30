using UnityEngine;
using System.Collections;

public class BulletSlowEffect : BulletController
{
    public float existTime;
    public float slowValue;

    public override void initEffect(GameObject enemy)
    {
        bool hasSlowEffect = false;
        if (enemy.GetComponent<EnemyController>().listEffected.Contains(EBulletEffect.SLOW))
        {
            enemy.GetComponent<BulletEffectController>().reset = true;
            hasSlowEffect = true;
        }

        if (!hasSlowEffect)
            BulletEffectManager.Instance.initEffect(EBulletEffect.SLOW, enemy, slowValue, existTime);
    }
}