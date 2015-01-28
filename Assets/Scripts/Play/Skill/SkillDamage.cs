using UnityEngine;
using System.Collections;

public class SkillDamage : MonoBehaviour
{
    SkillController controller;
    SkillState state;

    void Awake()
    {
        controller = transform.parent.GetComponent<SkillController>();
        state = controller.listState[controller.StateAction];
    }

    public void damage(GameObject enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController.StateAction == EEnemyStateAction.DIE)
            return;

        if (state.hasDamage)
            runDamage(enemyController);

        if (state.effectType != EBulletEffect.NONE)
            runEffect(enemyController);

        if (controller.StateAction == ESkillAction.ARMAGGEDDON)
            runArmageddon(enemyController);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.Enemy)
        {
            damage(other.gameObject);
        }
    }

    void runDamage(EnemyController enemy)
    {
        enemy.attribute.HP.Current -= 20;
        Debug.Log("OK");

        showCollisionObject(enemy.gameObject);

        if (enemy.attribute.HP.Current < 0)
            enemy.attribute.HP.Current = 0;

        enemy.updateHP();

        if (enemy.attribute.HP.Current == 0)
            enemy.die();
    }

    void runEffect(EnemyController enemy)
    {
        switch (state.effectType)
        {
            #region BURN
            case EBulletEffect.BURN:
                bool hasEffect = false;
                foreach (Transform child in enemy.transform)
                {
                    if (child.name == PlayNameHashIDs.BulletEffectBurning)
                    {
                        hasEffect = true;

                        Animator animator = child.GetComponentInChildren<Animator>();
                        animator.SetBool("Loop", true);
                        animator.SetBool("Finish", false);

                        child.GetComponent<BulletEffectController>().reset = true;

                        break;
                    }
                }

                if (!hasEffect)
                {
                    string[] values = (string[])state.effectValue;
                    BulletEffectManager.Instance.initEffect(EBulletEffect.BURN, enemy.gameObject,
                        float.Parse(values[0]), int.Parse(values[1]), float.Parse(values[2]));
                }
                break;
            #endregion
            #region SLOW
            case EBulletEffect.SLOW:
                hasEffect = false;
                if (enemy.GetComponent<EnemyController>().listEffected.Contains(EBulletEffect.SLOW))
                {
                    enemy.GetComponent<BulletEffectController>().reset = true;
                    hasEffect = true;
                }

                if (!hasEffect)
                {
                    string[] values = (string[])state.effectValue;
                    BulletEffectManager.Instance.initEffect(EBulletEffect.SLOW, enemy.gameObject, 
                        float.Parse(values[0]), float.Parse(values[1]));
                }
                break;
            #endregion
            #region POISON
            case EBulletEffect.POISON:
                break;
            #endregion
            #region STUN
            case EBulletEffect.STUN:
                hasEffect = false;
                foreach (Transform child in enemy.transform)
                {
                    if (child.name == PlayNameHashIDs.BulletEffectBurning)
                    {
                        hasEffect = true;
                        child.GetComponent<BulletEffectController>().reset = true;
                        break;
                    }
                }

                if (!hasEffect)
                {
                    string[] values = (string[])state.effectValue;
                    BulletEffectManager.Instance.initEffect(EBulletEffect.STUN, enemy.gameObject,
                        controller.getCurrentState().effectObjectID, values[0]);
                }
                break;
            #endregion
        }
    }

    void runArmageddon(EnemyController enemy)
    {
        switch(((SkillStateArmaggeddon)state).type)
        {
            case ESkillArmaggeddon.METEOR:
                StartCoroutine(atkEnemy(enemy, 0.2f, 2));
                break;
            case ESkillArmaggeddon.TORNADO:
                StartCoroutine(subThunder(enemy, 0.4f, 10));
                break;
        }
    }

    IEnumerator atkEnemy(EnemyController enemy, float timeFrame, int damage)
    {
        float elapsedTime = timeFrame;
        while (true)
        {
            if(enemy == null)
                yield break;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeFrame)
            {
                elapsedTime = 0.0f;
                enemy.attribute.HP.Current -= damage;

                if (enemy.attribute.HP.Current < 0)
                    enemy.attribute.HP.Current = 0;

                enemy.updateHP();

                if (enemy.attribute.HP.Current == 0)
                {
                    enemy.die();
                    yield break;
                }
            }
            yield return 0;
        }
    }

    #region SUB SKILL (skill phu cho skill chinh)
    IEnumerator subThunder(EnemyController enemy, float timeFrame, int damage)
    {
        float elapsedTime = 0.0f;
        float randomTime = Random.Range(1.0f, 6.0f);

        while (true)
        {
            if (enemy == null)
                yield break;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= randomTime)
            {
                elapsedTime = 0.0f;
                randomTime = Random.Range(1.0f, 6.0f);

                if(enemy.attribute.HP.Current > 0)
                    BulletEffectManager.Instance.initEffect(EBulletEffect.THUNDER, enemy.gameObject, 
                    EBulletEffect.THUNDER.ToString());
            }
            yield return 0;
        }
    }
    #endregion

    void showCollisionObject(GameObject enemyObject)
    {
        string path = "Prefab/Collision/Collision " + state.collisionNum;

        GameObject _collision = Instantiate(Resources.Load<GameObject>(path)) as GameObject;
        _collision.transform.parent = PlayManager.Instance.Temp.Collision.transform;
        _collision.transform.localScale = Vector3.one;
        _collision.transform.position = enemyObject.transform.position;
        _collision.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueCollision;
    }
}
