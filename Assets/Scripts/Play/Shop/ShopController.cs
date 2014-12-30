using UnityEngine;
using System.Collections;

public enum ETowerShopState
{
	TOWER_BUY_DEFAULT,
	TOWER_BUY_ENABLE,
	TOWER_BUY_UNABLE,
}

public enum EShopType
{
	NONE,
	TOWER,
	ITEM,
}

public enum EPaymentType
{
	NONE,
	DIAMOND,
	GOLD,
}

public class ShopController : Singleton<ShopController>
{
	public GameObject towerPanel;
	public GameObject towerShopModel;
	public GameObject infoPanel;
	public GameObject towerInfoModel;
	public GameObject itemShopModel;
	public GameObject itemShopInfoModel;
	public GameObject towerShopPanel;
	public GameObject paymentTowerPanel;
	[HideInInspector] public GameObject paymentItemPanel;

	public UILabel labelTowerName;
	public UILabel labelDiamond;
	public UILabel labelMoney;

	[HideInInspector]
	public GameObject target;

	public ETowerShopState[] towerShopType { get; set; }
	public EShopType shopType { get; set; }

	bool initWhenStart = false;

	int money;
	public int Money
	{
		set
		{
			money = value;
			labelMoney.text = money.ToString();
		}
		get
		{
			return money;
		}
	}

	int diamond;
	public int Diamond
	{
		set
		{
			diamond = value;
			labelDiamond.text = diamond.ToString();
		}
		get
		{
			return diamond;
		}
	}

	void Start()
	{
		shopType = EShopType.NONE;

		if(!initWhenStart)
			towerShopType = new ETowerShopState[ObjectManager.Instance.Towers.Length];

		initPrefab ();

		if(!initWhenStart)
			loadTower();
	}

	void OnEnable()
	{
		Money = PlayInfo.Instance.Money;
		Diamond = PlayerInfo.Instance.userInfo.diamond;
	}

	void initPrefab()
	{
		paymentItemPanel = Instantiate(Resources.Load<GameObject> ("Prefab/Shop/Payment Item")) as GameObject;
		paymentItemPanel.transform.parent = this.transform;
		paymentItemPanel.transform.localPosition = Vector3.zero;
		paymentItemPanel.transform.localScale = Vector3.one;
		paymentItemPanel.GetComponent<UIStretch> ().container = this.gameObject;
		paymentItemPanel.SetActive (false);
	}

	public void reset()
	{
		target.GetComponent<TowerShopController>().setColor(false);
		target = null;
	}

	void setSpriteButton(EShopType type)
	{
		foreach(Transform child_1 in this.transform)
		{
			if(child_1.name == "Buttons")
			{
				foreach(Transform child_2 in child_1.transform)
				{
					switch(child_2.name)
					{
					case "Buy Tower":
						if(type == EShopType.TOWER)
						{
							child_2.transform.GetChild(0).GetComponent<UISprite>().spriteName = "button-bg-2";
							child_2.transform.GetChild(0).GetComponent<UIButton>().normalSprite = "button-bg-2";
						}
						else if(type == EShopType.ITEM)
						{
							child_2.transform.GetChild(0).GetComponent<UISprite>().spriteName = "button-bg-1";
							child_2.transform.GetChild(0).GetComponent<UIButton>().normalSprite = "button-bg-1";
						}
						break;
					case "Buy Item":
						if(type == EShopType.TOWER)
						{
							child_2.transform.GetChild(0).GetComponent<UISprite>().spriteName = "button-bg-1";
							child_2.transform.GetChild(0).GetComponent<UIButton>().normalSprite = "button-bg-1";
						}
						else if(type == EShopType.ITEM)
						{
							child_2.transform.GetChild(0).GetComponent<UISprite>().spriteName = "button-bg-2";
							child_2.transform.GetChild(0).GetComponent<UIButton>().normalSprite = "button-bg-2";
						}
						break;
					}
				}

				break;
			}
		}
	}

	public void loadTower()
	{
		if (shopType == EShopType.TOWER)
			return;

		shopType = EShopType.TOWER;
		target = null;

		AutoDestroy.destroyChildren(towerPanel, PlayNameHashIDs.Collider);
		towerPanel.transform.localPosition = Vector3.zero;
		towerPanel.GetComponent<UIPanel> ().clipOffset = Vector2.zero;

		setSpriteButton (EShopType.TOWER); 

		int length = ObjectManager.Instance.Towers.Length;
		for (int i = 0; i < length; i++)
		{
			TowerController currentTower = ObjectManager.Instance.Towers[i].GetComponent<TowerController>();

			GameObject _towerShop = Instantiate(towerShopModel) as GameObject;
			_towerShop.transform.parent = towerPanel.transform;
			_towerShop.transform.localScale = Vector3.one;
			_towerShop.name = currentTower.name;

			UIAnchor anchor = _towerShop.GetComponent<UIAnchor>();
			anchor.container = towerPanel.gameObject;
			anchor.relativeOffset = new Vector2(PlayConfig.TowerShopAnchor.PanelAnchorStart + i * PlayConfig.TowerShopAnchor.PanelAnchorDistance, 0);

			TowerShopController towerShopController = _towerShop.GetComponent<TowerShopController>();
			towerShopController.Index = i;
			towerShopController.ID = currentTower.ID;
			towerShopController.Name.text = currentTower.attribute.Name.Split()[0];

			//set background stretch
			foreach(Transform child in _towerShop.transform)
			{
				if(child.name == "Background")
				{
					child.GetComponent<UIStretch>().container = towerPanel;
					break;
				}
			}

            //set diamond and gold
            STowerCost cost = ReadDatabase.Instance.TowerCostInfo[towerShopController.ID.Type.ToString()];
            towerShopController.Money = cost.Gold;
            towerShopController.Diamond = cost.Diamond;
            
			string s = GameConfig.PathTowerIcon + currentTower.ID.Type.ToString() + "-" + currentTower.ID.Level;

			towerShopController.icon.mainTexture = Resources.Load<Texture>(s);
			towerShopController.setColor(false);

			Vector2 dimension = PlayConfig.getTowerIconSize(towerShopController.ID);
			towerShopController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
			towerShopController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

			UIStretch uiStretch = towerShopController.icon.GetComponent<UIStretch>();
			uiStretch.enabled = true;

			if (target == null)
				target = _towerShop;

			if(!initWhenStart)
			{
				towerShopType[i] = WaveController.Instance.infoMap.TowerUsed.ToUpper().Contains(currentTower.ID.Type.ToString()) ? ETowerShopState.TOWER_BUY_DEFAULT : ETowerShopState.TOWER_BUY_ENABLE;
			}

			_towerShop.GetComponentInChildren<UITowerShop>().type = towerShopType[i];
			
		}
		if (target != null)
			target.GetComponentInChildren<TowerShopController>().setColor(true);

		loadInfoTower();

		if(!initWhenStart)
			initWhenStart  =true;
	}

    public void loadTowerPassive()
    {
        if (shopType == EShopType.TOWER)
            return;

        shopType = EShopType.TOWER;
        target = null;

        //AutoDestroy.destroyChildren(towerPanel, PlayNameHashIDs.Collider);
        //towerPanel.transform.localPosition = Vector3.zero;
        //towerPanel.GetComponent<UIPanel>().clipOffset = Vector2.zero;

        setSpriteButton(EShopType.TOWER);
        int TowersLength = ObjectManager.Instance.Towers.Length;
        int length = ObjectManager.Instance.TowersPassive.Length;
        for (int i = 0; i < length; i++)
        {
            TowerPassiveController currentTower = ObjectManager.Instance.TowersPassive[i].GetComponent<TowerPassiveController>();

            GameObject _towerShop = Instantiate(towerShopModel) as GameObject;
            _towerShop.transform.parent = towerPanel.transform;
            _towerShop.transform.localScale = Vector3.one;
            _towerShop.name = currentTower.name;

            UIAnchor anchor = _towerShop.GetComponent<UIAnchor>();
            anchor.container = towerPanel.gameObject;
            anchor.relativeOffset = new Vector2(PlayConfig.TowerShopAnchor.PanelAnchorStart + (i + TowersLength) * PlayConfig.TowerShopAnchor.PanelAnchorDistance, 0);

            TowerShopController towerShopController = _towerShop.GetComponent<TowerShopController>();
            towerShopController.Index = i;
            towerShopController.ID = currentTower.ID;
            towerShopController.Name.text = currentTower.attribute.Name.Split()[0];

            //set background stretch
            foreach (Transform child in _towerShop.transform)
            {
                if (child.name == "Background")
                {
                    child.GetComponent<UIStretch>().container = towerPanel;
                    break;
                }
            }

            //set diamond and gold
            STowerCost cost = ReadDatabase.Instance.TowerCostInfo[towerShopController.ID.Type.ToString()];
            towerShopController.Money = cost.Gold;
            towerShopController.Diamond = cost.Diamond;

            string s = GameConfig.PathTowerIcon + currentTower.ID.Type.ToString() + "-" + currentTower.ID.Level;

            towerShopController.icon.mainTexture = Resources.Load<Texture>(s);
            towerShopController.setColor(false);

            Vector2 dimension = PlayConfig.getTowerIconSize(towerShopController.ID);
            towerShopController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
            towerShopController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

            UIStretch uiStretch = towerShopController.icon.GetComponent<UIStretch>();
            uiStretch.enabled = true;

            if (target == null)
                target = _towerShop;

            if (!initWhenStart)
            {
                towerShopType[i] = WaveController.Instance.infoMap.TowerUsed.ToUpper().Contains(currentTower.ID.Type.ToString()) ? ETowerShopState.TOWER_BUY_DEFAULT : ETowerShopState.TOWER_BUY_ENABLE;
            }

            _towerShop.GetComponentInChildren<UITowerShop>().type = towerShopType[i];

        }
        if (target != null)
            target.GetComponentInChildren<TowerShopController>().setColor(true);

        loadInfoTowerPassive();

        if (!initWhenStart)
            initWhenStart = true;
    }

	public void loadInfoTower()
	{
		if (target != null)
		{
			AutoDestroy.destroyChildren(infoPanel, PlayNameHashIDs.Collider);

			//active tower name
			if (!labelTowerName.gameObject.activeSelf)
				labelTowerName.gameObject.SetActive(true);

			TowerShopController targetController = target.GetComponent<TowerShopController>();
			labelTowerName.text = targetController.Name.text.ToUpper() + " TOWER";

			//Set towe name color
			Color[] nameColor = PlayConfig.getColorTowerName(targetController.ID);
			labelTowerName.color = nameColor[0];
			labelTowerName.effectColor = nameColor[1];

			STowerID towerID = targetController.ID;

			GameObject[] towers = ObjectManager.Instance.Towers;
			int length = towers.Length;
			int count = 0;

			for (int i = 0; i < length; i++)
			{
				TowerController towerController = towers[i].GetComponent<TowerController>();

				if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
				{
					while (true)
					{
						GameObject info = Instantiate(towerInfoModel) as GameObject;
						info.transform.parent = infoPanel.transform;
						info.transform.localScale = Vector3.one;
						info.name = towerController.name;
                        
                        UIAnchor anchor = info.GetComponent<UIAnchor>();
						anchor.container = infoPanel.gameObject;
						anchor.relativeOffset = new Vector2(0, PlayConfig.InfoShopAnchor.PanelAnchorStart - count * PlayConfig.InfoShopAnchor.PanelAnchorDistance);

						#region TOWER
						TowerShopInfoController infoController = info.GetComponent<TowerShopInfoController>();
						infoController.icon.mainTexture = Resources.Load<Texture>(PlayConfig.getTowerIcon(towerID));
						infoController.level.text = "Level " + ((int)towerID.Level).ToString();
						infoController.atk.text = towerController.attribute.MinATK.ToString() + " - " + towerController.attribute.MaxATK.ToString();
						infoController.spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
						infoController.timeBuild.text = towerController.attribute.TimeBuild.ToString();

						//Bullet label
						string[] str = PlayConfig.getBulletType(towerController.attackType.ToString());
						infoController.bulletAbility.text = str[0] + " TARGET";
						infoController.bulletRegion.text = str[1];

						//set icon fix size
						Vector2 dimension = PlayConfig.getTowerIconSize(towerController.ID);
						infoController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
						infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

						UIStretch uiStretch = infoController.icon.GetComponent<UIStretch>();
						uiStretch.enabled = true;
						#endregion

						#region BULLET
						object[] bulletData = PlayConfig.getBulletShop(towerID);
						SBulletTowerShop bulletTowerShop = (SBulletTowerShop)bulletData[1];
						infoController.bulletIcon.mainTexture = Resources.Load<Texture>("Image/Bullet/Bullet Icon/" + bulletData[0].ToString());


						infoController.bulletIcon.SetDimensions((int)bulletTowerShop.Dimension.x, (int)bulletTowerShop.Dimension.y);
						infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

						UIStretch stretch = infoController.bulletIcon.GetComponent<UIStretch>();
						stretch.relativeSize.y = bulletTowerShop.Stretch;
						stretch.enabled = true;

						//Add effect
						infoController.bullet = towerController.bullet;
						#endregion

						count++;

						//get Next Tower
						if (towerController.nextLevel != null)
						{
							towerID = towerController.nextLevel.GetComponent<TowerController>().ID;
							towerController = towerController.nextLevel;
						}
						else
							break;
					}
					break;
				}
			}
		}
	}
    public void loadInfoTowerPassive()
    {
        if (target != null)
        {
            //AutoDestroy.destroyChildren(infoPanel, PlayNameHashIDs.Collider);

            //active tower name
            if (!labelTowerName.gameObject.activeSelf)
                labelTowerName.gameObject.SetActive(true);

            TowerShopController targetController = target.GetComponent<TowerShopController>();
            labelTowerName.text = targetController.Name.text.ToUpper() + " TOWER";

            //Set towe name color
            Color[] nameColor = PlayConfig.getColorTowerName(targetController.ID);
            labelTowerName.color = nameColor[0];
            labelTowerName.effectColor = nameColor[1];

            STowerID towerID = targetController.ID;

            GameObject[] towersPassive = ObjectManager.Instance.TowersPassive;
            int length = towersPassive.Length;
            int count = 0;

            for (int i = 0; i < length; i++)
            {
                TowerPassiveController towerController = towersPassive[i].GetComponent<TowerPassiveController>();

                if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
                {
                    while (true)
                    {
                        GameObject info = Instantiate(towerInfoModel) as GameObject;
                        info.transform.parent = infoPanel.transform;
                        info.transform.localScale = Vector3.one;
                        info.name = towerController.name;

                        UIAnchor anchor = info.GetComponent<UIAnchor>();
                        anchor.container = infoPanel.gameObject;
                        anchor.relativeOffset = new Vector2(0, PlayConfig.InfoShopAnchor.PanelAnchorStart - count * PlayConfig.InfoShopAnchor.PanelAnchorDistance);

                        #region TOWER
                        TowerShopInfoController infoController = info.GetComponent<TowerShopInfoController>();
                        infoController.icon.mainTexture = Resources.Load<Texture>(PlayConfig.getTowerIcon(towerID));
                        infoController.level.text = "Level " + ((int)towerID.Level).ToString();
                        infoController.atk.text = towerController.attribute.MinATK.ToString() + " - " + towerController.attribute.MaxATK.ToString();
                        infoController.spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
                        infoController.timeBuild.text = towerController.attribute.TimeBuild.ToString();

                        //Bullet label
                        string[] str = PlayConfig.getBulletType(towerController.attackType.ToString());
                        infoController.bulletAbility.text = str[0] + " TARGET";
                        infoController.bulletRegion.text = str[1];

                        //set icon fix size
                        Vector2 dimension = PlayConfig.getTowerIconSize(towerController.ID);
                        infoController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
                        infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                        UIStretch uiStretch = infoController.icon.GetComponent<UIStretch>();
                        uiStretch.enabled = true;
                        #endregion

                        #region BULLET
                        object[] bulletData = PlayConfig.getBulletShop(towerID);
                        SBulletTowerShop bulletTowerShop = (SBulletTowerShop)bulletData[1];
                        infoController.bulletIcon.mainTexture = Resources.Load<Texture>("Image/Bullet/Bullet Icon/" + bulletData[0].ToString());


                        infoController.bulletIcon.SetDimensions((int)bulletTowerShop.Dimension.x, (int)bulletTowerShop.Dimension.y);
                        infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                        UIStretch stretch = infoController.bulletIcon.GetComponent<UIStretch>();
                        stretch.relativeSize.y = bulletTowerShop.Stretch;
                        stretch.enabled = true;

                        //Add effect
                        infoController.bullet = towerController.bullet;
                        #endregion

                        count++;

                        //get Next Tower
                        if (towerController.nextLevel != null)
                        {
                            towerID = towerController.nextLevel.GetComponent<TowerPassiveController>().ID;
                            towerController = towerController.nextLevel as TowerPassiveController;
                        }
                        else
                            break;
                    }
                    break;
                }
            }
        }
    }
	public void loadInfoTower(STowerID ID)
	{
		if (target != null)
		{
			TowerShopController towerShopController = target.GetComponent<TowerShopController>();
			if (towerShopController.ID != ID)
				towerShopController.setColor(false);
			else
			{
				return;
			}
		}

		foreach (Transform child in towerShopPanel.transform)
		{
			TowerShopController towerShopController = child.GetComponent<TowerShopController>();
			if (towerShopController != null)
			{
				if (towerShopController.ID == ID)
				{
					target = towerShopController.gameObject;
					ShopController.Instance.target.GetComponent<TowerShopController>().setColor(true);
                   
					loadInfoTower();
					break;
				}
			}
		}
	}

	public void loadItem()
	{
		if (shopType == EShopType.ITEM)
			return;
		
		shopType = EShopType.ITEM;
		target = null;
		
		AutoDestroy.destroyChildren(towerPanel, PlayNameHashIDs.Collider);
		towerPanel.transform.localPosition = Vector3.zero;
		towerPanel.GetComponent<UIPanel> ().clipOffset = Vector2.zero;

		setSpriteButton (EShopType.ITEM);

		int i = 0;
		foreach(System.Collections.Generic.KeyValuePair<string, ItemData> item in ReadDatabase.Instance.ItemInfo)
		{
			GameObject _itemShop = Instantiate(itemShopModel) as GameObject;
			_itemShop.transform.parent = towerPanel.transform;
			_itemShop.transform.localScale = Vector3.one;
			
			UIAnchor anchor = _itemShop.GetComponent<UIAnchor>();
			anchor.container = towerPanel.gameObject;
			anchor.relativeOffset = new Vector2(PlayConfig.TowerShopAnchor.PanelAnchorStart + i * PlayConfig.TowerShopAnchor.PanelAnchorDistance, 0);
			
			ItemShopController itemShopController = _itemShop.GetComponent<ItemShopController>();
			itemShopController.ID = item.Key;
			itemShopController.ItemState = ItemManager.getItemState(item.Key);

			//set name
			string[] arr = item.Value.Name.Split('-');
			string text = "";
			string mainName = "";
			for(int k = 0; k < arr.Length; k++)
			{
				text += arr[k];
				mainName += arr[k] + " ";
				if(k+1 < arr.Length)
					text += "\n";
			}
			text = text.Trim();
			mainName = mainName.Trim();

			itemShopController.Name.text = text;
			itemShopController.MainName = mainName;

			//set text value
			itemShopController.Value.text = item.Value.ValueText;

			//set background stretch
			foreach(Transform child in _itemShop.transform)
			{
				if(child.name == "Background")
				{
					child.GetComponent<UIStretch>().container = towerPanel;
					break;
				}
			}

			//set icon
			itemShopController.icon.spriteName = "item-" + item.Key.ToLower();
			itemShopController.setColor(false);
			
			Vector2 dimension = PlayConfig.getSizeItem(item.Key);
			itemShopController.icon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
			itemShopController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
			itemShopController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
			
			UIStretch uiStretch = itemShopController.icon.GetComponent<UIStretch>();
			uiStretch.enabled = true;
			
			if (target == null)
				target = _itemShop;

			i++;
		}
		if (target != null)
			target.GetComponentInChildren<ItemShopController>().setColor(true);
		
		loadInfoItem();
	}

	public void loadInfoItem()
	{
		if (target != null)
		{
			AutoDestroy.destroyChildren(infoPanel, PlayNameHashIDs.Collider);
			
			//active tower name
			if (!labelTowerName.gameObject.activeSelf)
				labelTowerName.gameObject.SetActive(true);
			
			ItemShopController targetController = target.GetComponent<ItemShopController>();
			labelTowerName.text = targetController.MainName;
			
			//Set name color
			labelTowerName.color = PlayConfig.ColorShopItemName;
			labelTowerName.effectColor = PlayConfig.ColorShopItemNameOutline;
			
			GameObject info = Instantiate(itemShopInfoModel) as GameObject;
			info.transform.parent = infoPanel.transform;
			info.transform.localScale = Vector3.one;
						
			info.GetComponent<UIAnchor>().container = infoPanel.gameObject;
			info.GetComponent<UIStretch>().container = infoPanel.gameObject;

			//Set value
			ItemShopInfoController infoController = info.GetComponent<ItemShopInfoController>();
			infoController.icon.spriteName = targetController.icon.spriteName;
			infoController.value.text = targetController.Value.text;
					
			//set icon fix size
			Vector2 dimension = PlayConfig.getSizeItem(targetController.ID);
			infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
			infoController.icon.SetDimensions((int)dimension.x, (int)dimension.y);
			infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

			//set package
			ItemData itemData = ReadDatabase.Instance.ItemInfo[targetController.ID];
			int length = itemData.Packages.Count;

			for(int i=0;i<length;i++)
			{
				ItemPackage itemPackage = (ItemPackage)itemData.Packages[i];
				ItemShopPackageController packageController = infoController.packages[i].GetComponent<ItemShopPackageController>();

				packageController.labelDiamond.text = itemPackage.Diamond.ToString();
				packageController.labelGold.text = itemPackage.Gold.ToString();

				int result = -1;
				if(int.TryParse(itemPackage.Wave,out result))
				{
					if(result <= 1)
						packageController.labelDuration.text = "Duration: " + result + " wave";
					else
						packageController.labelDuration.text = "Duration: " + result + " waves";
				}
				else
				{
					if(itemPackage.Wave.Equals("Half"))
						packageController.labelDuration.text = "Duration: " + WaveController.Instance.infoMap.WaveLength/2 + " waves";
					else if(itemPackage.Wave.Equals("Full"))
						packageController.labelDuration.text = "Duration: all waves"; 
				}
			}
		}
	}

	public void openPaymentItemPanel()
	{
		PaymentItemController controller = paymentItemPanel.GetComponent<PaymentItemController> ();

		ItemData itemData = ReadDatabase.Instance.ItemInfo[target.GetComponent<ItemShopController>().ID];
		int length = itemData.Packages.Count;
        
		for (int i=0; i<length; i++)
		{
			ItemPackage itemPackage = (ItemPackage)itemData.Packages[i];

			UIPaymentItemShop uiPayment = controller.packages[i].GetComponentInChildren<UIPaymentItemShop>();
			uiPayment.Diamond = itemPackage.Diamond;
			uiPayment.Gold = itemPackage.Gold;

			int result = -1;
			if(int.TryParse(itemPackage.Wave,out result))
			{
				uiPayment.wave = result;
			}
			else
			{
				if(itemPackage.Wave.Equals("Half"))
				{
					if(WaveController.Instance.WaveCurrent > WaveController.Instance.infoMap.WaveLength /2)
						uiPayment.wave = WaveController.Instance.infoMap.WaveLength - WaveController.Instance.WaveCurrent;
					else
						uiPayment.wave = WaveController.Instance.infoMap.WaveLength / 2;

				}
				else if(itemPackage.Wave.Equals("Full"))
				{
					if(WaveController.Instance.WaveCurrent <= 1)
					{
						uiPayment.wave = WaveController.Instance.infoMap.WaveLength;
					}
					else
					{
						uiPayment.wave = WaveController.Instance.infoMap.WaveLength - WaveController.Instance.WaveCurrent;
					}
				}
			}
		}
	}

	public void checkInitArrShopType()
	{
		if(!initWhenStart)
			towerShopType = new ETowerShopState[ObjectManager.Instance.Towers.Length];
	}

	public void showToast(ETowerShopState type)
	{
		switch (type)
		{
			case ETowerShopState.TOWER_BUY_DEFAULT:
				DeviceService.Instance.openToast(GameConfig.ToastTowerBuyDefault);
				break;
			case ETowerShopState.TOWER_BUY_ENABLE:
				DeviceService.Instance.openToast(GameConfig.ToastTowerBuyEnable);
				break;
			case ETowerShopState.TOWER_BUY_UNABLE:
				DeviceService.Instance.openToast(GameConfig.ToastTowerBuyUnable);
				break;
		}
	}
}
