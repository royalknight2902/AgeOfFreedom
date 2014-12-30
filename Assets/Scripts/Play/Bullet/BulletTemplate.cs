using UnityEngine;
using System.Collections;

public class BulletTemplate
{
    protected BulletController bulletController;
    protected GameObject enemy;
    public bool isCollision;

    public BulletTemplate()
    {
        isCollision = false;
    }

    public virtual void updateController(BulletController controller)
    {
        bulletController = controller;
    }

    public virtual void updateEnemy(GameObject _enemy)
    {
        enemy = _enemy;
    }

    public virtual void Initalize(BulletController controller)
    {

    }

    public virtual void Update()
    {

    }
}
