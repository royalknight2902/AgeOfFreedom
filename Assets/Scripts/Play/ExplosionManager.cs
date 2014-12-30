using UnityEngine;
using System.Collections;

public class ExplosionManager : Singleton<ExplosionManager> 
{
    [HideInInspector]
    public GameObject[] explosions;

    void Awake()
    {
        initComponent();
    }
    void initComponent()
    {
        explosions = new GameObject[11];
        explosions[0] = Resources.Load<GameObject>("Prefab/Explosion/Rock/Explosion Rock 1");
        explosions[1] = Resources.Load<GameObject>("Prefab/Explosion/Rock/Explosion Rock 2");
        explosions[2] = Resources.Load<GameObject>("Prefab/Explosion/Rock/Explosion Rock 3");
        explosions[3] = Resources.Load<GameObject>("Prefab/Explosion/Rock/Explosion Rock 4");
        explosions[4] = Resources.Load<GameObject>("Prefab/Explosion/Rock/Explosion Rock 5");
        explosions[5] = Resources.Load<GameObject>("Prefab/Explosion/Ice/Explosion Ice 1");
        explosions[6] = Resources.Load<GameObject>("Prefab/Explosion/Ice/Explosion Ice 2");
        explosions[7] = Resources.Load<GameObject>("Prefab/Explosion/Ice/Explosion Ice 3");
        explosions[8] = Resources.Load<GameObject>("Prefab/Explosion/Fire/Explosion Fire 1");
        explosions[9] = Resources.Load<GameObject>("Prefab/Explosion/Fire/Explosion Fire 2");
        explosions[10] = Resources.Load<GameObject>("Prefab/Explosion/Fire/Explosion Fire 3");
    }
   
}
