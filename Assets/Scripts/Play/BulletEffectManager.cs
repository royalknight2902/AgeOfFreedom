using UnityEngine;
using System.Collections;

public enum EBulletEffect
{
    NONE,
    SLOW,
    BURN,
    POISON,
    STUN,
    THUNDER,
}

public class BulletEffectManager : Singleton<BulletEffectManager>
{
    public void initEffect(EBulletEffect type, GameObject enemy, params object[] obj)
    {
        switch (type)
        {
            #region SLOW
            case EBulletEffect.SLOW:
                float slowValue = (float)obj[0];
                //Set animation
                float timeFrame = enemy.transform.GetChild(0).gameObject.GetComponent<AnimationFrames>().timeFrame;

                enemy.transform.GetChild(0).gameObject.GetComponent<AnimationFrames>().timeFrame = timeFrame * (1 + slowValue);
                //Set enemy speed
                float moveSpeed = enemy.GetComponent<EnemyController>().speed;

                enemy.GetComponent<EnemyController>().speed *= (1 - slowValue);
                //Set enemy color
                enemy.GetComponentInChildren<SpriteRenderer>().color = PlayConfig.ColorSlowEffect;
                enemy.GetComponent<EnemyController>().listEffected.Add(EBulletEffect.SLOW);
                enemy.AddComponent<BulletEffectController>();
                StartCoroutine(slow(enemy, slowValue, (float)obj[1]));
                break;
            #endregion
            #region BURN
            case EBulletEffect.BURN:
                GameObject burning = Instantiate(Resources.Load<GameObject>("Prefab/Effect/Effect Burning")) as GameObject;
                burning.transform.parent = enemy.transform;
                burning.transform.localScale = Vector3.one;
                burning.GetComponent<UIAnchor>().container = enemy;
                burning.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletEffect - WaveController.Instance.countEnemy;
                burning.name = PlayNameHashIDs.BulletEffectBurning;

                enemy.GetComponent<EnemyController>().listEffected.Add(EBulletEffect.BURN);

                StartCoroutine(burn(burning, enemy, (float)obj[0], (int)obj[1], (float)obj[2]));
                break;
            #endregion
            #region POISON
            case EBulletEffect.POISON:
                GameObject poisoning = Instantiate(Resources.Load<GameObject>("Prefab/Effect/Effect Poisoning")) as GameObject;
                GameObject bgHealth = enemy.transform.GetChild(1).transform.GetChild(1).gameObject;

                poisoning.transform.parent = enemy.transform;
                poisoning.transform.localScale = Vector3.one;
                poisoning.GetComponent<UIAnchor>().container = bgHealth;
                poisoning.GetComponent<UIStretch>().container = bgHealth;
                poisoning.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletEffect - WaveController.Instance.countEnemy;
                poisoning.name = PlayNameHashIDs.BulletEffeftPoisoning;

                enemy.GetComponent<EnemyController>().listEffected.Add(EBulletEffect.POISON);

                StartCoroutine(poison(poisoning, enemy, (float)obj[0], (int)obj[1], (float)obj[2]));
                break;
            #endregion
            #region STUN
            case EBulletEffect.STUN:
                GameObject stunning = Instantiate(Resources.Load<GameObject>("Prefab/Object/Object")) as GameObject;
                stunning.name = "Stun Effect";
                stunning.transform.parent = enemy.transform;
                stunning.transform.localScale = Vector3.one;
                stunning.transform.localPosition = Vector3.one;
                stunning.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletEffect - WaveController.Instance.countEnemy;

                ObjectController objectController = stunning.GetComponent<ObjectController>();
                objectController.initalize(obj[0].ToString()); //ID

                enemy.GetComponent<EnemyController>().listEffected.Add(EBulletEffect.STUN);
                enemy.AddComponent<BulletEffectController>();

                StartCoroutine(stun(stunning, enemy, float.Parse(obj[1].ToString())));
                break;
            #endregion
            #region THUNDER
            case EBulletEffect.THUNDER:
                GameObject thundering = Instantiate(Resources.Load<GameObject>("Prefab/Object/Object")) as GameObject;
                thundering.name = "Thunder Effect";
                thundering.transform.parent = enemy.transform;
                thundering.transform.localScale = Vector3.one;
                thundering.transform.localPosition = Vector3.one;
                thundering.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletEffect - WaveController.Instance.countEnemy;

                objectController = thundering.GetComponent<ObjectController>();
                objectController.initalize(obj[0].ToString()); //ID

                StartCoroutine(thunder(thundering, enemy, objectController.objectAnimation.getDuration(), 10));
                //enemy.GetComponent<EnemyController>().listEffected.Add(EBulletEffect.THUNDER);
                //enemy.AddComponent<BulletEffectController>();
                break;
            #endregion
        }
    }

    IEnumerator slow(GameObject enemy, float slowValue, float timeExist)
    {
        float elapsedTimeExist = 0;
        BulletEffectController effectController = enemy.GetComponent<BulletEffectController>();

        while (true)
        {
            if (enemy != null)
            {
                if (effectController.reset)
                {
                    elapsedTimeExist = 0;
                    effectController.reset = false;
                }

                elapsedTimeExist += Time.deltaTime;
                if (elapsedTimeExist >= timeExist)
                {
                    float timeFrame = enemy.transform.GetChild(0).gameObject.GetComponent<AnimationFrames>().timeFrame;
                    enemy.GetComponentInChildren<AnimationFrames>().timeFrame = timeFrame * (1 - slowValue);
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;

                    EnemyController controller = enemy.GetComponent<EnemyController>();
                    controller.speed /= (1 - slowValue);
                    controller.listEffected.Remove(EBulletEffect.SLOW);

                    Destroy(enemy.GetComponent<BulletEffectController>());

                    yield break;
                }
                yield return 0;
            }
            else
                yield break;
        }
    }

    IEnumerator burn(GameObject burning, GameObject enemy, float timeFrame, int DamageEachFrame, float timeExist)
    {
        float elapsedTimeDamage = 0;
        float elapsedTimeExist = 0;
        BulletEffectController effectController = burning.GetComponent<BulletEffectController>();
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        while (true)
        {
            if (enemy != null)
            {
                if (effectController.reset)
                {
                    elapsedTimeExist = 0;
                    effectController.reset = false;
                }

                elapsedTimeDamage += Time.deltaTime;
                elapsedTimeExist += Time.deltaTime;

                if (enemyController.attribute.HP.Current <= 0 && !enemyController.isDie)
                {
                    Animator animator = burning.GetComponentInChildren<Animator>();
                    animator.SetBool("Loop", false);
                    animator.SetBool("Finish", true);

                    enemyController.die();
                    yield break;
                }

                if (elapsedTimeDamage >= timeFrame)
                {
                    PlayManager.Instance.pushDamageEffect(enemyController, DamageEachFrame);
                    elapsedTimeDamage = 0;
                }
                if (elapsedTimeExist >= timeExist)
                {
                    Animator animator = burning.GetComponentInChildren<Animator>();
                    animator.SetBool("Loop", false);
                    animator.SetBool("Finish", true);
                    enemy.GetComponent<EnemyController>().listEffected.Remove(EBulletEffect.BURN);
                    yield break;
                }
                yield return 0;
            }
            else
                yield break;
        }
    }

    IEnumerator poison(GameObject poisoning, GameObject enemy, float timeFrame, int damageEachFrame, float timeExist)
    {
        float elapsedTimeDamage = 0;
        float elapsedTimeExist = 0;
        BulletEffectController effectController = poisoning.GetComponent<BulletEffectController>();
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        while (true)
        {
            if (enemy != null)
            {
                if (effectController.reset)
                {
                    elapsedTimeExist = 0;
                    effectController.reset = false;
                }
                elapsedTimeDamage += Time.deltaTime;
                elapsedTimeExist += Time.deltaTime;
                if (enemyController.attribute.HP.Current <= 0 && !enemyController.isDie)
                {
                    Animator animator = poisoning.GetComponentInChildren<Animator>();
                    animator.SetBool("Loop", false);
                    animator.SetBool("Finish", true);

                    enemyController.die();
                    yield break;
                }

                if (elapsedTimeDamage >= timeFrame)
                {
                    PlayManager.Instance.pushDamageEffect(enemyController, damageEachFrame);
                    elapsedTimeDamage = 0;
                }
                if (elapsedTimeExist >= timeExist)
                {
                    Animator animator = poisoning.GetComponentInChildren<Animator>();
                    animator.SetBool("Loop", false);
                    animator.SetBool("Finish", true);
                    enemy.GetComponent<EnemyController>().listEffected.Remove(EBulletEffect.POISON);
                    yield break;
                }

                yield return 0;
            }
            else
                yield break;
        }
    }

    IEnumerator stun(GameObject stun, GameObject enemy, float duration)
    {
        float elapsedTimeExist = 0;
        BulletEffectController effectController = enemy.GetComponent<BulletEffectController>();
        EnemyController controller = enemy.GetComponent<EnemyController>();
        controller.stop();

        while (true)
        {
            if (enemy != null)
            {
                if (effectController.reset)
                {
                    elapsedTimeExist = 0;
                    effectController.reset = false;
                }

                elapsedTimeExist += Time.deltaTime;
                if (elapsedTimeExist >= duration)
                {
                    ObjectController obj = stun.GetComponent<ObjectController>();
                    obj.listState[obj.StateAction].goNextState();

                    controller.resume();
                    controller.listEffected.Remove(EBulletEffect.STUN);

                    Destroy(enemy.GetComponent<BulletEffectController>());

                    yield break;
                }
                yield return 0;
            }
            else
                yield break;
        }
    }

    IEnumerator thunder(GameObject thunder, GameObject enemy, float duration, int damage)
    {
        float existTime = 0.0f;
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        while(true)
        {
            if (enemy == null)
                yield break;

            existTime += Time.deltaTime;
            if(existTime >= duration)
            {
                ObjectController obj = thunder.GetComponent<ObjectController>();
                obj.listState[obj.StateAction].goNextState();

                enemyController.attribute.HP.Current -= damage;

                if (enemyController.attribute.HP.Current < 0)
                    enemyController.attribute.HP.Current = 0;

                enemyController.updateHP();

                if (enemyController.attribute.HP.Current == 0)
                {
                    enemyController.die();
                    yield break;
                }

                yield break;
            }
            yield return 0;
        }
    }
}
