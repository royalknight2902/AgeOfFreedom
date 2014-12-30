using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour
{
    public ExplosionHashIDs id;
    public bool pushDamage { get; set; }
    public int TowerATK { get; set; }

    Collider parentCollider;
    Collider childCollider;
    EBulletColliderType colliderType;

    void Start()
    {
        parentCollider = this.collider;
        if (parentCollider is SphereCollider)
        {
            childCollider = this.gameObject.GetComponentsInChildren<SphereCollider>()[1];
            colliderType = EBulletColliderType.SPHERE;
        }
        else if (parentCollider is BoxCollider)
        {
            childCollider = this.gameObject.GetComponentsInChildren<BoxCollider>()[1];
            colliderType = EBulletColliderType.BOX;
        }
        else if (parentCollider is CapsuleCollider)
        {
            childCollider = this.gameObject.GetComponentsInChildren<CapsuleCollider>()[1];
            colliderType = EBulletColliderType.CAPSULE;
        }
    }

    void Update()
    {
        if (pushDamage)
        {
            getChildColliderValue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (pushDamage)
        {
            if (other.gameObject.tag == TagHashIDs.Enemy && other.GetComponent<EnemyController>().region == EEnemyRegion.LAND)
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();
                PlayManager.Instance.pushDamagePhysics(enemyController, TowerATK);
                if (enemyController.attribute.HP.Current <= 0 && !enemyController.isDie)
                {
                    enemyController.die();
                }
            }
        }
    }

    void getChildColliderValue()
    {
        if (colliderType == EBulletColliderType.BOX)
        {
            BoxCollider childTemp = childCollider as BoxCollider;
            BoxCollider parentTemp = parentCollider as BoxCollider;

            parentTemp.center = childTemp.center;
            parentTemp.size = childTemp.size;
        }
        else if (colliderType == EBulletColliderType.CAPSULE)
        {
            CapsuleCollider childTemp = childCollider as CapsuleCollider;
            CapsuleCollider parentTemp = parentCollider as CapsuleCollider;

            parentTemp.center = childTemp.center;
            parentTemp.radius = childTemp.radius;
            parentTemp.height = childTemp.height;
        }
        else if (colliderType == EBulletColliderType.SPHERE)
        {
            SphereCollider childTemp = childCollider as SphereCollider;
            SphereCollider parentTemp = parentCollider as SphereCollider;

            parentTemp.center = childTemp.center;
            parentTemp.radius = childTemp.radius;
        }
    }
}
