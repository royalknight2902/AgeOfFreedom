using UnityEngine;
using System.Collections;

public class TowerShopInfoController : MonoBehaviour
{
    public UITexture icon;
    public UILabel level;
    public UILabel atk;
    public UILabel spawnShoot;
    public UILabel timeBuild;

    public UITexture bulletIcon;
    public UILabel bulletAbility;
    public UILabel bulletRegion;
    public UISprite bulletEffect;

    [HideInInspector]
    public GameObject bullet;

    void Start()
    {
        if (bullet != null)
        {
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController.effect == EBulletEffect.NONE)
                bulletEffect.gameObject.SetActive(false);
            else
            {
                bulletEffect.gameObject.SetActive(true);
                bulletEffect.GetComponent<UIPlay>().bulletEffect = bullet;
                loadEffectIcon();
            }
        }
    }

    void loadEffectIcon()
    {
        bulletEffect.spriteName = "icon-effect-" + bullet.GetComponent<BulletController>().effect.ToString().ToLower();
    }
}
