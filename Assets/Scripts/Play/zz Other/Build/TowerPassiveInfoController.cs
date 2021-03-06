﻿using UnityEngine;
using System.Collections;

public class TowerPassiveInfoController : MonoBehaviour
{
	public bool hasClickUpgrade { get; set; }

	public UISprite sellTexture;
	public UISprite upgradeTexture;

	public UILabel sellLabel;
	public UILabel upgradeLabel;
	public UITexture upgradeIcon;

	//Tower
	public UILabel Name;
	public UILabel value;
	public UILabel updateTime;
	public UILabel timeBuild;
	public UILabel typeTower;

	//Icon
	public UITexture goldIcon;
	public UILabel goldAbility;
	public UISprite goldEffect;

	//bool isLock;
	TowerPassiveController controller;
    HouseController house;

    [HideInInspector] public GameObject childTowerInfo;
    [HideInInspector] public GameObject childDragonInfo;
    public bool isOnTowerInfo { get; set; }

	void Start()
	{
		//isLock = false;

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
	public void setTowerInfo(TowerPassiveController towerController)
	{
        isOnTowerInfo = true;   
        childTowerInfo.SetActive(true);

        if(childDragonInfo != null)
            childDragonInfo.SetActive(false);

		controller = towerController;

		sellLabel.text = (towerController.totalMoney / 2).ToString();
		Name.text = towerController.attribute.Name;
		value.text = towerController.passiveAttribute.Value.ToString();
		updateTime.text = towerController.passiveAttribute.UpdateTime.ToString();
		timeBuild.text = towerController.passiveAttribute.TimeBuild.ToString ();
		typeTower.text = towerController.passiveAttribute.Type.ToString ();
		value.parent.transform.GetComponentInChildren<UISprite> ().spriteName = "icon-gold";
		//Set color tower name
		Color[] colors = PlayConfig.getColorTowerName(towerController.ID);
		Name.color = colors[0];
		Name.effectColor = colors[1];

		if(towerController.nextLevel == null)
		{
			//Khóa upgrade
			lockUpgrade(true);
			TowerInfoController.isLock = true;
		}
		else
			upgradeLabel.text = towerController.nextLevel.attribute.Cost.ToString();

		//Check bonus
//		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
//			ATK.text += " [8aff5c]+ " + towerController.Bonus.ATK + "[-]";
//		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
//			spawnShoot.text += " [8aff5c]- " + towerController.Bonus.SpawnShoot + "[-]";
//		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
//		{
//			PlayManager playManager = PlayManager.Instance;
//
//			playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
//			float scale = (float)(towerController.attribute.Range + towerController.Bonus.Range) / 100f;
//			playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
//			playManager.rangeTowerBonus.SetActive(true);
//
//			((SphereCollider)controller.collider).radius = controller.attribute.Range + controller.Bonus.Range;
//		}
	}

	public void setNextTowerInfo(TowerPassiveController towerController)
	{
		controller = towerController;
		
		Name.text = towerController.attribute.Name;
		value.text = towerController.passiveAttribute.Value.ToString();
		updateTime.text = towerController.passiveAttribute.UpdateTime.ToString ();
		timeBuild.text = towerController.passiveAttribute.TimeBuild.ToString ();
		//Check bonus
//		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
//		{
//			ATK.text += " [8aff5c]+ " + (int)((towerController.attribute.MinATK + towerController.attribute.MaxATK) * ItemManager.Instance.BonusATK / 2) + "[-]";
//		}
//		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
//		{
//			spawnShoot.text += " [8aff5c]- " + towerController.attribute.SpawnShoot * ItemManager.Instance.BonusSpawnShoot + "[-]";
//		}
//		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
//		{
//			PlayManager playManager = PlayManager.Instance;
//			
//			playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
//			float scale = (float)(controller.attribute.Range + (int)(towerController.attribute.Range * ItemManager.Instance.BonusRange)) / 100f;
//			playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
//			playManager.rangeTowerBonus.SetActive(true);
//		}
	}

	public void setCurrentTowerInfo(TowerPassiveController towerController)
	{
		controller = towerController;
		
		Name.text = towerController.attribute.Name;
		value.text = towerController.passiveAttribute.Value.ToString();
		updateTime.text = towerController.passiveAttribute.UpdateTime.ToString ();
		timeBuild.text = towerController.passiveAttribute.TimeBuild.ToString ();
		
		//Check bonus
//		if(ItemManager.Instance.listItemState.Contains(EItemState.ATK))
//			ATK.text += " [8aff5c]+ " + towerController.Bonus.ATK + "[-]";
//		if(ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
//			spawnShoot.text += " [8aff5c]- " + towerController.Bonus.SpawnShoot + "[-]";
//		if(ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
//			{
//				PlayManager playManager = PlayManager.Instance;
//				
//				playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
//				float scale = (float)(controller.attribute.Range + controller.Bonus.Range) / 100f;
//				playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
//				playManager.rangeTowerBonus.SetActive(true);
//
//				((SphereCollider)controller.collider).radius = controller.attribute.Range + controller.Bonus.Range;
//			}
	}

	public void setValueInfo(STowerID ID, string strDescribe)
	{
		//Icon
		object[] value = PlayConfig.getBulletBuild(ID);
        SAnchor b = (SAnchor)value[1];
		object[] bulletData = PlayConfig.getBulletShop(ID);
		goldIcon.mainTexture = Resources.Load<Texture>(GameConfig.PathBulletIcon + bulletData[0]);
		goldIcon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
		goldIcon.SetDimensions(goldIcon.mainTexture.width, goldIcon.mainTexture.height);
		goldIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		
		UIAnchor anchor = goldIcon.GetComponent<UIAnchor>();
		anchor.relativeOffset = b.Anchor;
		anchor.enabled = true;
		
		UIStretch stretch = goldIcon.GetComponent<UIStretch>();
		stretch.relativeSize.y = b.Stretch;
		stretch.enabled = true;
//


		//Label
		//string[] str = PlayConfig.getBulletType(s);

		goldAbility.text = strDescribe;


		goldEffect.gameObject.SetActive(false);
	
	}

	public void setNextTowerIcon(STowerID id)
	{
		if (TowerInfoController.isLock)
		{
			lockUpgrade(false);
			TowerInfoController.isLock = false;
		}

		string s = GameConfig.PathTowerIcon + id.Type.ToString().ToLower() + "-" + id.Level;

		upgradeIcon.mainTexture = Resources.Load<Texture>(s);

		UITexture texture = upgradeIcon.GetComponent<UITexture>();
        texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;
        Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
        texture.SetDimensions((int)localSize.x, (int)localSize.y);
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
			TowerInfoController.isLock = true;
        }
    }

    public void setNextHouseIcon(STowerID id)
    {
		if (TowerInfoController.isLock)
        {
            lockUpgrade(false);
			TowerInfoController.isLock = false;
        }

        string s = GameConfig.PathHouseIcon + id.Level;

        upgradeIcon.mainTexture = Resources.Load<Texture>(s);

        UITexture texture = upgradeIcon.GetComponent<UITexture>();
        texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;
        Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
        texture.SetDimensions((int)localSize.x, (int)localSize.y);
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
