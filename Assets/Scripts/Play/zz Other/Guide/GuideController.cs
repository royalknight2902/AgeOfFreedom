using UnityEngine;
using System.Collections;

public class GuideController : Singleton<GuideController>
{
    public GameObject guideSelected;

    public GameObject regSelected;
    public GameObject regInfo;
    public GameObject regNote;

    public GameObject towerLevelSelected;
    public GameObject selectedRegionPanel;
    public GameObject InfoPanel;
    public GameObject infoTower;
    public GameObject infoEnemy;
    public UILabel infoName;
    public GameObject[] levels;

    [HideInInspector]
    public GameObject target;

    public static bool hasUpdateEnemy = false;
    public int maxLevel { get; set; }
    public int currentLevel { get; set; }

    UIGuideButton guideType;
    ArrayList listTower = new ArrayList();
    string[] enemies;

    void Start()
    {
        setEnemyArray();
        loadTower();
    }

    void OnEnable()
    {
        if (guideType == UIGuideButton.ENEMY && hasUpdateEnemy)
        {
            loadEnemy();
            hasUpdateEnemy = false;
        }
    }

    #region TOWERS
    public void loadTower()
    {
        AutoDestroy.destroyChildren(selectedRegionPanel);

        infoTower.gameObject.SetActive(true);
        infoEnemy.gameObject.SetActive(false);

        target = null;
        guideType = UIGuideButton.TOWER;

        //set grid
        UIGrid grid = selectedRegionPanel.GetComponent<UIGrid>();
        grid.maxPerLine = PlayConfig.GridGuideTower.MaxPerLine;
        grid.cellWidth = PlayConfig.GridGuideTower.CellWidth;
        grid.cellHeight = PlayConfig.GridGuideTower.CellHeight;
        grid.enabled = true;
        grid.repositionNow = true;

        int length = ObjectManager.Instance.Towers.Length;
        for (int i = 0; i < length; i++)
        {
            TowerController towerController = ObjectManager.Instance.Towers[i].GetComponent<TowerController>();

            GameObject towerGuide = Instantiate(PlayManager.Instance.modelPlay.TowerGuide) as GameObject;
            towerGuide.transform.parent = selectedRegionPanel.transform;

            towerGuide.GetComponent<UIStretch>().container = selectedRegionPanel;
            towerGuide.GetComponentInChildren<TowerGuideController>().ID = towerController.ID;
		
            //Set icon
            towerGuide.GetComponent<UISprite>().spriteName = "tower-info-" + towerController.ID.Type.ToString().ToLower();
            towerGuide.name = towerController.ID.Type.ToString();

            //Set name

			string name = towerController.ID.Type.ToString();
		
            UILabel label = towerGuide.GetComponentInChildren<UILabel>();
            label.text = name[0] + name.Substring(1, name.Length - 1).ToLower();

            //Set color
            Color[] colors = PlayConfig.getColorTowerName(towerController.ID);
            label.color = colors[0];
            label.effectColor = colors[1];

            if (target == null)
                target = towerGuide;
        }
        if (target != null)
        {
            target.GetComponentInChildren<TowerGuideController>().setColor(true);
        }

        loadTowerInfo();



	 	length = ObjectManager.Instance.TowersPassive.Length;
		for (int i = 0; i < length; i++)
		{
			TowerPassiveController towerController = ObjectManager.Instance.TowersPassive[i].GetComponent<TowerPassiveController>();
			
			GameObject towerGuide = Instantiate(PlayManager.Instance.modelPlay.TowerGuide) as GameObject;
			towerGuide.transform.parent = selectedRegionPanel.transform;
			
			towerGuide.GetComponent<UIStretch>().container = selectedRegionPanel;
			towerGuide.GetComponentInChildren<TowerGuideController>().ID = towerController.ID;

		
			//Set icon
			towerGuide.GetComponent<UISprite>().spriteName = "tower-info-" + towerController.ID.Type.ToString().ToLower();
			towerGuide.name = towerController.ID.Type.ToString();
			
			//Set name
			
			string name = towerController.ID.Type.ToString();
			
			UILabel label = towerGuide.GetComponentInChildren<UILabel>();
			label.text = name[0] + name.Substring(1, name.Length - 1).ToLower();
			
			//Set color
			Color[] colors = PlayConfig.getColorTowerName(towerController.ID);
			label.color = colors[0];
			label.effectColor = colors[1];
			
			if (target == null)
				target = towerGuide;
		}
		if (target != null)
		{
			target.GetComponentInChildren<TowerGuideController>().setColor(true);
		}
		
		loadTowerPassiveInfo();
    }
	public void loadTowerPassiveInfo()
	{

		if (target != null)
		{
			//active tower name
			if (!infoName.gameObject.activeSelf)
				infoName.gameObject.SetActive(true);
			
			TowerGuideController targetController = target.GetComponentInChildren<TowerGuideController>();
			string name = targetController.ID.Type.ToString();
			infoName.text = name[0] + name.Substring(1, name.Length - 1).ToLower() + " LV " + targetController.ID.Level;
			
			//Set towe name color
			Color[] nameColor = PlayConfig.getColorTowerName(targetController.ID);
			infoName.color = nameColor[0];
			infoName.effectColor = nameColor[1];
			
			STowerID towerID = targetController.ID;
			
			GameObject[] towers = ObjectManager.Instance.TowersPassive;
			int length = towers.Length;

			int count = 0;
			for (int i = 0; i < length; i++)
			{
			
				TowerPassiveController towerController = towers[i].GetComponent<TowerPassiveController>();

				if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
				{
				
					while (true)
					{
						GameObject info = Instantiate(PlayManager.Instance.modelPlay.TowerGuideInfo) as GameObject;
						info.transform.parent = infoTower.transform;
						info.transform.localScale = Vector3.one;
						info.name = towerController.name;
						
						UIAnchor anchor = info.GetComponent<UIAnchor>();
						anchor.container = InfoPanel.gameObject;
						anchor.relativeOffset = new Vector2(PlayConfig.AnchorTowerGuideInfoStartX + count * PlayConfig.AnchorTowerGuideInfoDistance, PlayConfig.AnchorTowerGuideInfoStartY);
						
						#region TOWER
						TowerShopInfoController infoController = info.GetComponent<TowerShopInfoController>();
						infoController.icon.mainTexture = Resources.Load<Texture>(PlayConfig.getTowerIcon(towerID));
						infoController.level.text = "Level " + ((int)towerID.Level).ToString();

						infoController.atk.parent.transform.GetComponentInChildren<UISprite> ().spriteName = "icon-gold";
						infoController.atk.text = towerController.passiveAttribute.Value.ToString();
						infoController.spawnShoot.text = towerController.passiveAttribute.UpdateTime.ToString();
						infoController.timeBuild.text = towerController.passiveAttribute.TimeBuild.ToString();
						
						//Bullet label
						string[] str = PlayConfig.getBulletType(towerController.attackType.ToString());
						infoController.bulletAbility.text = str[0] + " TARGET";
						infoController.bulletRegion.text = str[1];
						
						//set icon fix size
						infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
						infoController.icon.SetDimensions(infoController.icon.mainTexture.width, infoController.icon.mainTexture.height);
						infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
						
						UIStretch uiStretch = infoController.icon.GetComponent<UIStretch>();
						uiStretch.enabled = true;
						#endregion
						
						#region BULLET
						object[] bulletData = PlayConfig.getBulletShop(towerID);
						infoController.bulletIcon.mainTexture = Resources.Load<Texture>(GameConfig.PathBulletIcon + bulletData[0].ToString());
						infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
						infoController.bulletIcon.SetDimensions(infoController.bulletIcon.mainTexture.width, infoController.bulletIcon.mainTexture.height);
						infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
						
						UIStretch stretch = infoController.bulletIcon.GetComponent<UIStretch>();
						stretch.relativeSize.y = (float)bulletData[1];
						stretch.enabled = true;
						
						//Add effect
						infoController.bullet = towerController.bullet;
						#endregion
						
						count++;
						listTower.Add(info);
						
						//get Next Tower
						if (towerController.nextLevel != null)
						{
							towerID = towerController.nextLevel.GetComponent<TowerPassiveController>().ID;
							towerController = (TowerPassiveController)towerController.nextLevel;
						}
						else
						{
							maxLevel = count;
							currentLevel = 1;
							towerLevelSelected.transform.position = levels[currentLevel - 1].transform.position;
							break;
						}
					}
					break;
				}
			}
		}
	}
    public void loadTowerInfo()
    {
		;
        if (target != null)
        {
            AutoDestroy.destroyChildren(infoTower, "Level");

            //clear array
            listTower.Clear(); 

            //active tower name
            if (!infoName.gameObject.activeSelf)
                infoName.gameObject.SetActive(true);

            TowerGuideController targetController = target.GetComponentInChildren<TowerGuideController>();
            string name = targetController.ID.Type.ToString();
            infoName.text = name[0] + name.Substring(1, name.Length - 1).ToLower() + " LV " + targetController.ID.Level;

            //Set towe name color
            Color[] nameColor = PlayConfig.getColorTowerName(targetController.ID);
            infoName.color = nameColor[0];
            infoName.effectColor = nameColor[1];

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
                        GameObject info = Instantiate(PlayManager.Instance.modelPlay.TowerGuideInfo) as GameObject;
                        info.transform.parent = infoTower.transform;
                        info.transform.localScale = Vector3.one;
                        info.name = towerController.name;

                        UIAnchor anchor = info.GetComponent<UIAnchor>();
                        anchor.container = InfoPanel.gameObject;
                        anchor.relativeOffset = new Vector2(PlayConfig.AnchorTowerGuideInfoStartX + count * PlayConfig.AnchorTowerGuideInfoDistance, PlayConfig.AnchorTowerGuideInfoStartY);

                        #region TOWER
                        TowerShopInfoController infoController = info.GetComponent<TowerShopInfoController>();
                        infoController.icon.mainTexture = Resources.Load<Texture>(PlayConfig.getTowerIcon(towerID));
                        infoController.level.text = "Level " + ((int)towerID.Level).ToString();
                        
						infoController.atk.parent.transform.GetComponentInChildren<UISprite> ().spriteName = "icon-atk";
						infoController.atk.text = towerController.attribute.MinATK.ToString() + " - " + towerController.attribute.MaxATK.ToString();
                        infoController.spawnShoot.text = towerController.attribute.SpawnShoot.ToString();
                        infoController.timeBuild.text = towerController.attribute.TimeBuild.ToString();

                        //Bullet label
                        string[] str = PlayConfig.getBulletType(towerController.attackType.ToString());
                        infoController.bulletAbility.text = str[0] + " TARGET";
                        infoController.bulletRegion.text = str[1];

                        //set icon fix size
                        infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
                        infoController.icon.SetDimensions(infoController.icon.mainTexture.width, infoController.icon.mainTexture.height);
                        infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                        UIStretch uiStretch = infoController.icon.GetComponent<UIStretch>();
                        uiStretch.enabled = true;
                        #endregion

                        #region BULLET
                        object[] bulletData = PlayConfig.getBulletShop(towerID);
                        infoController.bulletIcon.mainTexture = Resources.Load<Texture>("Image/Bullet/Bullet Icon/" + bulletData[0].ToString());
                        infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
                        infoController.bulletIcon.SetDimensions(infoController.bulletIcon.mainTexture.width, infoController.bulletIcon.mainTexture.height);
                        infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                        UIStretch stretch = infoController.bulletIcon.GetComponent<UIStretch>();
                        stretch.relativeSize.y = (float)bulletData[1];
                        stretch.enabled = true;

                        //Add effect
                        infoController.bullet = towerController.bullet;
                        #endregion

                        count++;
                        listTower.Add(info);

                        //get Next Tower
                        if (towerController.nextLevel != null)
                        {
                            towerID = towerController.nextLevel.GetComponent<TowerController>().ID;
                            towerController = towerController.nextLevel;
                        }
                        else
                        {
                            maxLevel = count;
                            currentLevel = 1;
                            towerLevelSelected.transform.position = levels[currentLevel - 1].transform.position;
                            break;
                        }
                    }
                    break;
                }
            }

			towers = ObjectManager.Instance.TowersPassive;
			length = towers.Length;
			for (int i = 0; i < length; i++)
			{
				TowerPassiveController towerController = towers[i].GetComponent<TowerPassiveController>();
				if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
				{
					while (true)
					{
						GameObject info = Instantiate(PlayManager.Instance.modelPlay.TowerGuideInfo) as GameObject;
						info.transform.parent = infoTower.transform;
						info.transform.localScale = Vector3.one;
						info.name = towerController.name;
						
						UIAnchor anchor = info.GetComponent<UIAnchor>();
						anchor.container = InfoPanel.gameObject;
						anchor.relativeOffset = new Vector2(PlayConfig.AnchorTowerGuideInfoStartX + count * PlayConfig.AnchorTowerGuideInfoDistance, PlayConfig.AnchorTowerGuideInfoStartY);
						
						#region TOWER
						TowerShopInfoController infoController = info.GetComponent<TowerShopInfoController>();
						infoController.icon.mainTexture = Resources.Load<Texture>(PlayConfig.getTowerIcon(towerID));
						infoController.level.text = "Level " + ((int)towerID.Level).ToString();
						
						infoController.atk.parent.transform.GetComponentInChildren<UISprite> ().spriteName = "icon-gold";
						infoController.atk.text = towerController.passiveAttribute.Value.ToString();
						infoController.spawnShoot.text = towerController.passiveAttribute.UpdateTime.ToString();
						infoController.timeBuild.text = towerController.passiveAttribute.TimeBuild.ToString();
						
						//Bullet label
						//string[] str = PlayConfig.getBulletType(towerController.attackType.ToString());
						infoController.bulletAbility.text = towerController.passiveAttribute.Describe.ToString();
						infoController.bulletRegion.text = towerController.passiveAttribute.Type.ToString();;
						
						//set icon fix size
						infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
						infoController.icon.SetDimensions(infoController.icon.mainTexture.width, infoController.icon.mainTexture.height);
						infoController.icon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
						
						UIStretch uiStretch = infoController.icon.GetComponent<UIStretch>();
						uiStretch.enabled = true;
						#endregion
						
						#region BULLET
						object[] bulletData = PlayConfig.getBulletShop(towerID);
						infoController.bulletIcon.mainTexture = Resources.Load<Texture>("Image/Bullet/Bullet Icon/" + bulletData[0].ToString());
						infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.Free;
						infoController.bulletIcon.SetDimensions(infoController.bulletIcon.mainTexture.width, infoController.bulletIcon.mainTexture.height);
						infoController.bulletIcon.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
						
						UIStretch stretch = infoController.bulletIcon.GetComponent<UIStretch>();
						stretch.relativeSize.y = (float)bulletData[1];
						stretch.enabled = true;
						
						//Add effect

						infoController.bullet = towerController.bullet;
						#endregion
						
						count++;
						listTower.Add(info);
						
						//get Next Tower
						if (towerController.nextLevel != null)
						{
							towerID = towerController.nextLevel.GetComponent<TowerPassiveController>().ID;
							towerController = (TowerPassiveController)towerController.nextLevel;
						}
						else
						{
							maxLevel = count;
							currentLevel = 1;
							towerLevelSelected.transform.position = levels[currentLevel - 1].transform.position;
							break;
						}
					}
					break;
				}
			}

        }
    }

    public void setLevel(int level)
    {
        if (currentLevel == level)
            return;

        GameObject currentInfo = listTower[currentLevel - 1] as GameObject;
        GameObject nextInfo = listTower[level - 1] as GameObject;

        Vector3 temp = nextInfo.transform.position;
        nextInfo.transform.position = currentInfo.transform.position;
        currentInfo.transform.position = temp;

        //set name
        infoName.text = infoName.text.Split(' ')[0] + " LV " + level;

        towerLevelSelected.transform.position = levels[level - 1].transform.position;
        currentLevel = level;
    }
    #endregion

    #region ENEMIES
    void setEnemyArray()
    {
        int length = ReadDatabase.Instance.EnemyInfo.Count;
        enemies = new string[length];

        int t = 0;
        foreach (System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
        {
            enemies[t] = iterator.Key;
            t++;
        }

        int min;
        GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
        EnemyController modelController = model.GetComponent<EnemyController>();

        EnemyController enemy1, enemy2;
        for (int i = 0; i < length - 1; i++)
        {
            min = i;

            for (int j = i + 1; j < length; j++)
            {
                GameSupportor.transferEnemyData(modelController, ReadDatabase.Instance.EnemyInfo[enemies[i]]);
                enemy1 = modelController;
                GameSupportor.transferEnemyData(modelController, ReadDatabase.Instance.EnemyInfo[enemies[j]]);
                enemy2 = modelController;
                if (enemy1.level > enemy2.level)
                {
                    min = j;

                    string swap = enemies[i];
                    enemies[i] = enemies[min];
                    enemies[min] = swap;
                }
                else if (enemy1.level == enemy2.level)
                {
                    if (enemy1.attribute.HP.Max > enemy2.attribute.HP.Max)
                    {
                        min = j;

                        string swap = enemies[i];
                        enemies[i] = enemies[min];
                        enemies[min] = swap;
                    }
                }
            }
        }
    }

    public void loadEnemy()
    {
        AutoDestroy.destroyChildren(selectedRegionPanel);
        infoTower.gameObject.SetActive(false);
        infoEnemy.gameObject.SetActive(true);

        guideType = UIGuideButton.ENEMY;

        target = null;

        //set grid
        UIGrid grid = selectedRegionPanel.GetComponent<UIGrid>();
        grid.maxPerLine = PlayConfig.GridGuideEnemy.MaxPerLine;
        grid.cellWidth = PlayConfig.GridGuideEnemy.CellWidth;
        grid.cellHeight = PlayConfig.GridGuideEnemy.CellHeight;
        grid.enabled = true;
        grid.repositionNow = true;

        EnemyController enemyController = Resources.Load<GameObject>("Prefab/Enemy/Enemy").GetComponent<EnemyController>();

        int length = enemies.Length;
        foreach (System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
        {
            GameSupportor.transferEnemyData(enemyController, iterator.Value);

            GameObject enemyGuide = Instantiate(PlayManager.Instance.modelPlay.EnemyGuide) as GameObject;
            enemyGuide.transform.parent = selectedRegionPanel.transform;
            enemyGuide.name = enemyController.name;

            enemyGuide.GetComponent<UIStretch>().container = selectedRegionPanel;

            EnemyGuideController controller = enemyGuide.GetComponent<EnemyGuideController>();
            controller.ID = enemyController.ID;
            controller.visible = PlayerInfo.Instance.listEnemy[enemyController.ID];

            //set icon
            if (controller.visible)
                controller.Icon.mainTexture = Resources.Load<Texture>("Image/Enemy/00 Guide Icon/" + enemyController.ID.ToLower());

            if (target == null && controller.visible)
                target = enemyGuide;
        }
        if (target != null)
            target.GetComponent<EnemyGuideController>().setColor(true);

        loadEnemyInfo();
    }

    public void loadEnemyInfo()
    {
        if (target != null)
        {
            //AutoDestroy.destroyChildren(InfoPanel, "Name");

            //active tower name
            if (!infoName.gameObject.activeSelf)
                infoName.gameObject.SetActive(true);

            EnemyGuideController targetController = target.GetComponent<EnemyGuideController>();

            int length = ReadDatabase.Instance.EnemyInfo.Count;
            GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
            EnemyController enemyController = model.GetComponent<EnemyController>();

            GameSupportor.transferEnemyData(enemyController, ReadDatabase.Instance.EnemyInfo[targetController.ID]);
            EnemyGuideInfoController info = infoEnemy.GetComponent<EnemyGuideInfoController>();
            info.Image.mainTexture = targetController.Icon.mainTexture;
            info.labelLevel.text = enemyController.level.ToString();
            info.labelHP.text = enemyController.attribute.HP.Max.ToString();
            info.labelDEF.text = enemyController.attribute.DEF.ToString();
            info.labelCoin.text = enemyController.money.ToString();
            info.labelRegion.text = enemyController.region.ToString();
            info.labelSpeed.text = PlayConfig.getSpeedString(enemyController.speed);
            //set visible boss icon
            if (enemyController.level >= 6)
                info.boss.gameObject.SetActive(true);
            else
                info.boss.gameObject.SetActive(false);

            //set level sprite
           
                info.spriteLevel.spriteName = "play-level-" + enemyController.level;

            //set name
            infoName.text = enemyController.attribute.Name;

            //set name color
            Color[] colors = PlayConfig.getColorEnemyName(enemyController.level);
            infoName.color = colors[0];
            infoName.effectColor = colors[1];
        }
        else
        {
            EnemyGuideInfoController info = infoEnemy.GetComponent<EnemyGuideInfoController>();
            info.Image.mainTexture = Resources.Load<Texture>("Image/Enemy/00 Guide Icon/0x0");
            info.labelLevel.text = "?";
            info.labelHP.text = "???";
            info.labelDEF.text = "???";
            info.labelCoin.text = "???";
            info.labelRegion.text = "???";
            info.labelSpeed.text = "???";
            info.boss.gameObject.SetActive(false);
            info.spriteLevel.spriteName = "play-level-123";

            //set name
            infoName.text = GameConfig.GameName.ToUpper();

            //set name color
            infoName.color = Color.white;
            infoName.effectColor = Color.black;
        }
    }
    #endregion
}
