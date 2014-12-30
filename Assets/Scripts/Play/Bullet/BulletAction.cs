using UnityEngine;
using System.Collections;
using System;

public enum EBulletColliderType
{
    NONE,
    BOX,
    SPHERE,
    CAPSULE,
}

public class BulletAction : MonoBehaviour
{
    public GameObject collision;

    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public TowerController towerController;
    [HideInInspector]
    public TowerAction towerAction;

    BulletController bulletController;
    BulletTemplate bulletTemplate;

    void Start()
    {
        transform.localPosition = transform.parent.InverseTransformPoint(transform.position);
        bulletController = this.GetComponent<BulletController>();
        bulletController.ATK = UnityEngine.Random.Range(towerController.attribute.MinATK 
		     + towerController.Bonus.ATK, towerController.attribute.MaxATK + towerController.Bonus.ATK);
        this.enemy = towerAction.enemy;
  
        switch (towerController.chaseType)
        {
            case EBulletChaseType.CHASE:
                bulletTemplate = new BulletChase();
                break;
            case EBulletChaseType.DROP:
                bulletTemplate = new BulletDrop();
                break;
            case EBulletChaseType.EXPLOSION:
                bulletTemplate = new BulletExplosion();
                break;
            case EBulletChaseType.BOMB:
                bulletTemplate = new BulletBomb();
                break;
            case EBulletChaseType.ARCHITECT:
                bulletTemplate = new BulletArchitect();
                break;
        }

        bulletTemplate.updateEnemy(enemy);
        bulletTemplate.Initalize(bulletController);
        bulletTemplate.Update();
    }

    void Update()
    {
        bulletTemplate.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagHashIDs.Enemy)
        {
            if (!bulletTemplate.isCollision)
                return;

            EnemyController _enemyController = other.gameObject.GetComponent<EnemyController>();
            if (_enemyController.isDie)
            {
                if (towerController.attackType == EBulletAttackType.SINGLE_LAND)
                {
                    Destroy(gameObject);
                    return;
                }
                else if (towerController.attackType == EBulletAttackType.MULTIPLE_LAND)
                {
                    return;
                }
            }

            if (towerController.attackType != EBulletAttackType.MULTIPLE_LAND)
            {
                if (enemy != other.gameObject)
                    return;
            }

            if (collision != null)
            {
                showCollisionObject(other.gameObject);
            }


            //Active effect if bullet has effect
            if (bulletController.hasEffect)
            {
                bulletController.initEffect(other.gameObject);
            }

            PlayManager.Instance.pushDamagePhysics(_enemyController, towerController);

            if (_enemyController.attribute.HP.Current <= 0 && !_enemyController.isDie)
            {
                _enemyController.die();
            }

            if (bulletTemplate.GetType().Name == PlayNameHashIDs.BulletArchitect)
            {
                Destroy(gameObject);
            }
        }
        
    }

    // Hien thi collision khi cham quai
    void showCollisionObject(GameObject enemyObject)
    {
        GameObject _collision = Instantiate(collision) as GameObject;
        _collision.transform.parent = PlayManager.Instance.Temp.Collision.transform;
        _collision.transform.localScale = Vector3.one;
        _collision.transform.position = enemyObject.transform.position;
        _collision.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueCollision;
    }
}
