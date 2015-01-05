using UnityEngine;
using System.Collections;

public class TowerInfoController : MonoBehaviour
{
	public bool hasClickUpgrade { get; set; }

	public UISprite sellTexture;
	public UISprite upgradeTexture;

	public UILabel sellLabel;
	public UILabel upgradeLabel;
	public UITexture upgradeIcon;

	//Tower
	public UILabel Name;
	public UILabel ATK;
	public UILabel spawnShoot;
	public UILabel timeBuild;

	//Bullet
	public UITexture bulletIcon;
	public UILabel bulletAbility;
	public UILabel bulletRegion;
	public UISprite bulletEffect;

	[HideInInspector]
	public int rangeCurrent;
	[HideInInspector]
	public int rangeUpgrade;

	bool isLock;
	TowerController controller;
    HouseController house;

    [HideInInspector] public GameObject childTowerInfo;
    [HideInInspector] public GameObject childDragonInfo;
    public bool isOnTowerInfo { get; set; }

	void Start()
	{
		isLock = false;

        foreach (Transform child in transform)
        {
            if (child.name.Equals("Tower Info"))
                childTowerInfo = child.gameObject;
            else if (child.name.Equals("Dragon Info"))
                childDragonInfo = child.gameObject;
        }
	}

    #region TOWER
    // set info tower
	public void setTowerInfo(TowerController towerController)
	{
        isOnTowerInfo = true;   
        childTowerInfo.SetActive(true);

        if(childDragonInfo != null)
            childDragonInfo.SetActive(false);

		controller = towerController;

		sellLabel.text = (towerController.totalMoney / 2).ToString();
		Name.text = towerController.attribute.Name;
		ATK.text = towerController.attribute.MinATK + "-" + towerController.attribute.MaxATK;
		spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
		timeBuild.text = towerController.attribute.TimeBuild.ToString();
		
		//Set color tower name
		Color[] colors = PlayConfig.getColorTowerName(towerController.ID);
		Name.color = colors[0];
		Name.effectColor = colors[1];

		if(towerController.nextLevel == null)
		{
			//Khóa upgrade
			lockUpgrade(true);
			isLock = true;
		}
		else
			upgradeLabel.text = towerController.nextLevel.attribute.Cost.ToString();

		//Check bonus
		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
			ATK.text += " [8aff5c]+ " + towerController.Bonus.ATK + "[-]";
		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
			spawnShoot.text += " [8aff5c]- " + towerController.Bonus.SpawnShoot + "[-]";
		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
		{
			PlayManager playManager = PlayManager.Instance;

			playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
			float scale = (float)(towerController.attribute.Range + towerController.Bonus.Range) / 100f;
			playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
			playManager.rangeTowerBonus.SetActive(true);

			((SphereCollider)controller.collider).radius = controller.attribute.Range + controller.Bonus.Range;
		}
	}

	public void checkTowerBonus()
	{
		if(controller != null)
		{
			if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
				ATK.text += " [8aff5c]+ " + controller.Bonus.ATK + "[-]";
			else
				ATK.text = controller.attribute.MinATK + "-" + controller.attribute.MaxATK;

			if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
				spawnShoot.text += " [8aff5c]- " + controller.Bonus.SpawnShoot + "[-]";
			else
				spawnShoot.text = controller.attribute.SpawnShoot.ToString();

			if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
			{
				PlayManager playManager = PlayManager.Instance;
				
				playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
				float scale = (float)(controller.attribute.Range + controller.Bonus.Range) / 100f;
				playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
				playManager.rangeTowerBonus.SetActive(true);

				((SphereCollider)controller.collider).radius = controller.attribute.Range + controller.Bonus.Range;
			}
			else
				PlayManager.Instance.rangeTowerBonus.SetActive(false);
		}
	}

	public void setNextTowerInfo(TowerController towerController)
	{
		controller = towerController;
		
		Name.text = towerController.attribute.Name;
		ATK.text = towerController.attribute.MinATK + "-" + towerController.attribute.MaxATK;
		spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
		timeBuild.text = towerController.attribute.TimeBuild.ToString();
		
		//Check bonus
		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
		{
			ATK.text += " [8aff5c]+ " + (int)((towerController.attribute.MinATK + towerController.attribute.MaxATK) * ItemManager.Instance.BonusATK / 2) + "[-]";
		}
		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
		{
			spawnShoot.text += " [8aff5c]- " + towerController.attribute.SpawnShoot * ItemManager.Instance.BonusSpawnShoot + "[-]";
		}
		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
		{
			PlayManager playManager = PlayManager.Instance;
			
			playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
			float scale = (float)(controller.attribute.Range + (int)(towerController.attribute.Range * ItemManager.Instance.BonusRange)) / 100f;
			playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
			playManager.rangeTowerBonus.SetActive(true);
		}
	}

	public void setCurrentTowerInfo(TowerController towerController)
	{
		controller = towerController;
		
		Name.text = towerController.attribute.Name;
		ATK.text = towerController.attribute.MinATK + "-" + towerController.attribute.MaxATK;
		spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
		timeBuild.text = towerController.attribute.TimeBuild.ToString();
		
		//Check bonus
		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
			ATK.text += " [8aff5c]+ " + towerController.Bonus.ATK + "[-]";
		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
			spawnShoot.text += " [8aff5c]- " + towerController.Bonus.SpawnShoot + "[-]";
		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
			{
				PlayManager playManager = PlayManager.Instance;
				
				playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
				float scale = (float)(controller.attribute.Range + controller.Bonus.Range) / 100f;
				playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
				playManager.rangeTowerBonus.SetActive(true);

				((SphereCollider)controller.collider).radius = controller.attribute.Range + controller.Bonus.Range;
			}
	}

	public void setBulletInfo(STowerID ID, string s, GameObject bullet)
	{
		//Icon
		object[] value = PlayConfig.getBulletBuild(ID);
		SBulletAnchor b = (SBulletAnchor)value[1];

		bulletIcon.mainTexture = Resources.Load<Texture>(GameConfig.PathBulletIcon + value[0].ToString());
		bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
		bulletIcon.SetDimensions((int)b.Dimension.x, (int)b.Dimension.y);
		bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

		UIAnchor anchor = bulletIcon.GetComponent<UIAnchor>();
		anchor.relativeOffset = b.Anchor;
		anchor.enabled = true;

		UIStretch stretch = bulletIcon.GetComponent<UIStretch>();
		stretch.relativeSize.y = b.Stretch;
		stretch.enabled = true;

		//Label
		string[] str = PlayConfig.getBulletType(s);
		bulletAbility.text = str[0] + " TARGET";
		bulletRegion.text = str[1];

		//Bullet effect
		if (bullet != null)
		{
			BulletController bulletController = bullet.GetComponent<BulletController>();
			if (bulletController.effect == EBulletEffect.NONE)
				bulletEffect.gameObject.SetActive(false);
			else
			{
				bulletEffect.gameObject.SetActive(true);
				bulletEffect.GetComponent<UIPlay>().bulletEffect = bullet;
				bulletEffect.spriteName = "icon-effect-" + bullet.GetComponent<BulletController>().effect.ToString().ToLower();
			}
			bulletEffect.GetComponent<UIPlay>().bulletEffect = bullet;
		}
	}

	public void setNextTowerIcon(STowerID id)
	{
		if (isLock)
		{
			lockUpgrade(false);
			isLock = false;
		}

		string s = GameConfig.PathTowerIcon + id.Type.ToString().ToLower() + "-" + id.Level;
      
		upgradeIcon.mainTexture = Resources.Load<Texture>(s);

		Vector2 dimension = PlayConfig.getTowerIconSize(id);
		UITexture texture = upgradeIcon.GetComponent<UITexture>();
		if (texture.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
		{
			texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;
		}
		texture.SetDimensions((int)dimension.x, (int)dimension.y);
		texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

		UIStretch uiStretch = upgradeIcon.GetComponent<UIStretch>();
		//uiStretch.container = panelTowerBuild.gameObject;
		uiStretch.enabled = true;
	}
    #endregion

    #region HOUSE
    public void setHouseInfo(HouseController houseController)
    {
        isOnTowerInfo = false;
        childTowerInfo.SetActive(false);
        childDragonInfo.SetActive(true);

        TowerInfoDragonController infoController = childDragonInfo.GetComponent<TowerInfoDragonController>();

        house = houseController;

        sellLabel.text = (house.totalMoney / 2).ToString();
        infoController.houseName.text = house.attribute.Name;
        infoController.timeGenerateChild.text = house.attribute.TimeGenerateChild.ToString();
        infoController.limitChild.text = house.attribute.LimitChild.ToString();

        try
        {
            upgradeLabel.text = ReadDatabase.Instance.DragonInfo.House[house.ID.Level + 1].Cost.ToString();
        }
        catch
        {
            //Khóa upgrade
            lockUpgrade(true);
            isLock = true;
        }
    }

    public void setNextHouseIcon(STowerID id)
    {
        if (isLock)
        {
            lockUpgrade(false);
            isLock = false;
        }

        string s = GameConfig.PathHouseIcon + id.Level;

        upgradeIcon.mainTexture = Resources.Load<Texture>(s);

        Vector2 dimension = PlayConfig.getTowerIconSize(id);
        UITexture texture = upgradeIcon.GetComponent<UITexture>();
        if (texture.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
        {
            texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;
        }
        texture.SetDimensions((int)dimension.x, (int)dimension.y);
        texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

        UIStretch uiStretch = upgradeIcon.GetComponent<UIStretch>();
        //uiStretch.container = panelTowerBuild.gameObject;
        uiStretch.enabled = true;
    }
    #endregion

    public void setSelected(ETowerInfoType type)
	{
		switch (type)
		{
			case ETowerInfoType.RESET:
				sellTexture.color = PlayConfig.ColorTowerUpgradeDefault;
				upgradeTexture.color = PlayConfig.ColorTowerUpgradeDefault;
				break;
			case ETowerInfoType.UPGRADE:
				sellTexture.color = PlayConfig.ColorTowerUpgradeDefault;
				upgradeTexture.color = PlayConfig.ColorTowerUpgradeSelected;
				break;
			case ETowerInfoType.SELL:
				sellTexture.color = PlayConfig.ColorTowerUpgradeSelected;
				upgradeTexture.color = PlayConfig.ColorTowerUpgradeDefault;
				break;
			case ETowerInfoType.LOCK:
				break;
		}
	}

	void lockUpgrade(bool isLockUpgrade)
	{
		if (isLockUpgrade)
		{
			foreach (Transform child in this.transform)
			{
				if (child.name == PlayNameHashIDs.TowerInfoUpgrade)
				{
					child.GetComponent<UITowerInfo>().type = ETowerInfoType.LOCK;

					foreach (Transform c in child.transform)
					{
						if (c.name == PlayNameHashIDs.Label)
						{
							c.gameObject.SetActive(false);
						}
						else if (c.name == PlayNameHashIDs.Icon)
						{
							c.gameObject.SetActive(false);
						}
						else if (c.name == PlayNameHashIDs.TowerInfoLock)
						{
							c.gameObject.SetActive(true);
						}
					}
					break;
				}
			}
		}
		else
		{
			foreach (Transform child in this.transform)
			{
				if (child.name == PlayNameHashIDs.TowerInfoUpgrade)
				{
					child.GetComponent<UITowerInfo>().type = ETowerInfoType.UPGRADE;

					foreach (Transform c in child.transform)
					{
						if (c.name == PlayNameHashIDs.Label)
						{
							c.gameObject.SetActive(true);
						}
						else if (c.name == PlayNameHashIDs.Icon)
						{
							c.gameObject.SetActive(true);
						}
						else if (c.name == PlayNameHashIDs.TowerInfoLock)
						{
							c.gameObject.SetActive(false);
						}
					}
					break;
				}
			}
		}
	}


}
