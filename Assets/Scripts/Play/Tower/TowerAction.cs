using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TowerController))]
public class TowerAction : MonoBehaviour
{
    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public ArrayList enemyArray;

    [HideInInspector]
    public bool isShooting;
    [HideInInspector]
    public bool isActivity;

    TowerController towerController;
    TowerAnimation towerAnimation;
    protected float elaspedTime = 0.0f;
    bool isUpdateStart;
    bool hasEnemy;

    Vector3 positionAnimation = new Vector3();

    public virtual void Start()
    {
        towerController = GetComponent<TowerController>();
        towerAnimation = GetComponentInChildren<TowerAnimation>();

        elaspedTime = towerController.attribute.SpawnShoot + 1;
        enemyArray = new ArrayList();
        SphereCollider collider = (SphereCollider)gameObject.collider;
        collider.radius = towerController.attribute.Range;
        isShooting = false;
        isActivity = false;
        isUpdateStart = false;
        hasEnemy = false;
        positionAnimation = towerAnimation.transform.localPosition;
        StartCoroutine(building());
    }

    IEnumerator building()
    {
        towerAnimation.building(towerController.attribute.TimeBuild);

        float valuePerFrame = Time.fixedDeltaTime / (towerController.attribute.TimeBuild / PlayerInfo.Instance.userInfo.timeScale);
        while (true)
        {
            towerController.updateBuilding(valuePerFrame);
            if (towerController.getBuildingValue() >= 1.0f)
            {
                towerController.updateBuilding(1.0f);
                towerController.enableHealth(false);
                towerAnimation.idle();
                isActivity = true;
                yield break;
            }
            yield return 0;
        }
    }

    public virtual void Update()
    {
        if (!hasEnemy)
        {
            if (isActivity && elaspedTime < (towerController.attribute.SpawnShoot - towerController.Bonus.SpawnShoot))
                elaspedTime += Time.deltaTime;
        }

        // fix postion for collider and show bullet
        if (positionAnimation != towerAnimation.transform.localPosition)
        {
            Vector3 temp = towerAnimation.transform.localPosition;
            // set posion for collider and bullet
            Transform[] all_child = transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in all_child)
            {
                if (child.name == "Collider")
                {
                    child.transform.localPosition += temp - positionAnimation;
                }
                if (child.name == "PositonAppearBullet")
                {
                    child.transform.localPosition += temp - positionAnimation;
                }
            }
            positionAnimation = temp;
        }
    }

    IEnumerator shootEnemy()
    {
        while (true)
        {
            // if tower have enemy shooting
            if (enemy && !enemy.GetComponent<EnemyController>().isDie)
            {
                // if enough time shooting and activity
                if (isActivity && elaspedTime >= (towerController.attribute.SpawnShoot - towerController.Bonus.SpawnShoot))
                {
                    StartCoroutine(generateBullet());
                    elaspedTime = 0;
                }
                // if no enough time shooting
                else
                {
                    elaspedTime += Time.deltaTime;
                }
            }
            // if tower haven't enemy shooting
            else
            {
                if (enemyArray.Count == 0)
                {
                    hasEnemy = false;
                    elaspedTime += Time.deltaTime;
                    isShooting = false;
                    isUpdateStart = false;
                    yield break;
                }
                else
                {
                    enemy = (GameObject)enemyArray[0];
                    enemyArray.RemoveAt(0);

                    if (isActivity && elaspedTime >= towerController.attribute.SpawnShoot)
                    {
                        StartCoroutine(generateBullet());
                        elaspedTime = 0;
                    }
                    // if no enough time shooting
                    else
                    {
                        elaspedTime += Time.deltaTime;
                    }
                }
            }
            yield return 0;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagHashIDs.Enemy)
        {
            EEnemyRegion enemyRegion = other.GetComponent<EnemyController>().region;
            EBulletAttackType towerRegion = towerController.attackType;

            string s = towerRegion.ToString().Split('_')[1];

            if (s == enemyRegion.ToString() || s == "ALL")
            {
                enemyArray.Add(other.gameObject);

                if (!isUpdateStart)
                {
                    hasEnemy = true;
                    StartCoroutine(shootEnemy());
                    isUpdateStart = true;
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagHashIDs.Enemy)
        {
            // if object exit trigger is enemy tower shooting
            if (other.gameObject == enemy)
            {
                // if no enemy in queue
                if (enemyArray.Count == 0)
                {
                    enemy = null;
                }
                else
                {
                    enemy = (GameObject)enemyArray[0];
                    enemyArray.RemoveAt(0);
                }
            }
            else
            {
                enemyArray.Remove(other.gameObject);
            }
        }
    }

    IEnumerator generateBullet()
    {
        int shootNumber = (int)towerController.shootType + 1;
        float timeDistance = towerController.attribute.SpawnShoot / 4 / shootNumber;
        int count = 0;

        while (true)
        {
            if (enemy == null)
            {
                yield break;
            }
            shoot();
            count++;
            if (count == shootNumber)
            {
                yield break;
            }
            yield return new WaitForSeconds(timeDistance);
        }
    }

    void shoot()
    {
        GameObject bullet = Instantiate(towerController.bullet) as GameObject;
        bullet.transform.parent = PlayManager.Instance.Temp.Bullet.transform;
        if (bullet.GetComponent<BulletController>().isDown)
            bullet.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletDown;
        else
            bullet.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueBulletUp;

        switch (towerController.chaseType)
        {
            case EBulletChaseType.CHASE:
                bullet.transform.position = towerController.appearBullet.transform.position;
                break;
            case EBulletChaseType.DROP:
                //bullet.transform.parent = towerController.appearBullet.transform;
                //bullet.transform.localPosition = Vector3.zero;
                break;
            case EBulletChaseType.EXPLOSION:
                bullet.transform.position = enemy.transform.position;
                break;
            case EBulletChaseType.BOMB:
                bullet.transform.position = towerController.appearBullet.transform.position;
                break;
            case EBulletChaseType.ARCHITECT:
                bullet.transform.position = towerController.appearBullet.transform.position;
                break;
        }

        bullet.transform.localScale = Vector3.one;

        BulletAction bulletAction = bullet.GetComponent<BulletAction>();
        bulletAction.towerController = this.towerController;
        bulletAction.towerAction = this;
    }
}
