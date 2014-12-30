using UnityEngine;
using System.Collections;

public class AutoExplosion : MonoBehaviour {
    public ExplosionHashIDs ID;

    public void initalize()
    {
        foreach(GameObject gameObject in ExplosionManager.Instance.explosions)
        {
            if (ID == gameObject.GetComponent<ExplosionController>().id)
            {
                GameObject explosion = Instantiate(gameObject) as GameObject;
                explosion.transform.parent = PlayManager.Instance.Temp.Explosion.transform;
                explosion.transform.localScale = Vector3.one;
                explosion.transform.position = this.transform.position;
                explosion.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueExplosion;
                break;
            }
        }
    }

    public void initalizeDamageExplosion(int towerATK)
    {
        foreach (GameObject gameObject in ExplosionManager.Instance.explosions)
        {
            if (ID == gameObject.GetComponent<ExplosionController>().id)
            {
                GameObject explosion = Instantiate(gameObject) as GameObject;
                explosion.transform.parent = PlayManager.Instance.Temp.Explosion.transform;
                explosion.transform.localScale = Vector3.one;
                explosion.transform.position = this.transform.position;
                explosion.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueExplosion;

                ExplosionController controller = explosion.GetComponent<ExplosionController>();
                controller.pushDamage = true;
                controller.TowerATK = towerATK;

                break;
            }
        }
    }
}
