using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayManager : Singleton<PlayManager>
{
    public GameObject footerBar;
    public GameObject selectedTowerBuild;
    public GameObject rangeTowerBonus;
    public GameObject heartEffectPosition;
	public List<GameObject> listTowerPlayerBuilt;
    // option music and sound game
    public UISlider SliderSound;
    public UISlider SliderMusic;

    public SPlayModel modelPlay;
    public STutorialModel modelTutorial;
    public STempInstance Temp;

    [HideInInspector]
    public SPlayInit tempInit = new SPlayInit();
    [HideInInspector]
    public ArrayList ShowTowers = new ArrayList();
    [HideInInspector]
    public SObjectBuild objectBuild;
    [HideInInspector]
    public SObjectUpgrade objectUpgrade;
    [HideInInspector]
    public TowerInfoController towerInfoController;
	[HideInInspector]
	public TowerPassiveInfoController towerPassiveInfoController;
    [HideInInspector]
    public GameObject itemBuffTemp;
    [HideInInspector]
    public GameObject rangeTower;
    [HideInInspector]
    public GameObject timeSpeed;
    [HideInInspector]
    public GameObject chooseTarget;
    [HideInInspector]
    public GameObject checkOK;
    [HideInInspector]
    public GameObject panelTowerBuild;
    [HideInInspector]
    public GameObject panelTowerUpgrade;
    [HideInInspector]
    public GameObject headerBar;
    [HideInInspector]
    public UITexture uiTextureMap;
    [HideInInspector]
    public UIWidget uiWidgetCamera;
    [HideInInspector]
    public GameObject startBallte;
    [HideInInspector]
    public GameObject achievement;
    [HideInInspector]
    public bool isZoom;
    [HideInInspector]
    public GameObject tutorial;
    [HideInInspector]
    public GameObject infoBluetooth;
	[HideInInspector]
	public GameObject enemyBluetoothShop;
	[HideInInspector]
	public GameObject towerBluetoothShop;

    [HideInInspector]
    public GameObject bluetoothManager;

    [HideInInspector]
    public ArrayList listTowerBuild = new ArrayList();
    [HideInInspector]
    public int number_tower_build = 0;

    public bool isOnShop { get; set; } // use for panel effect in shop
    public bool isOnGuide { get; set; } //use for panel effect in guide

    void Awake()
    {
        PlayerInfo.Instance.userInfo.timeScale = 1.0f;
        PlayerInfo.Instance.userInfo.Save();

        PlayPanel.Instance.hideAllPanel();
        isOnShop = false;
        isOnGuide = false;

        towerInfoController = footerBar.GetComponentInChildren<TowerInfoController>();
		towerPassiveInfoController = footerBar.GetComponentInChildren<TowerPassiveInfoController> ();
        rangeTowerBonus.transform.GetChild(0).renderer.material.renderQueue = GameConfig.RenderQueueRange;

        if (SceneState.Instance.State == ESceneState.ADVENTURE || SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            WaveController.Instance.getInfoMap();

            isZoom = true;
            uiTextureMap = GameObject.FindGameObjectWithTag("Map").GetComponent<UITexture>();
            uiWidgetCamera = GameObject.FindGameObjectWithTag("DragCamera").GetComponent<UIWidget>();
        }


        initGameObject();
        initTowerShop();
        Camera.main.GetComponent<AudioSource>().volume = (float)PlayerInfo.Instance.userInfo.volumeMusic / 100;
        selectedTowerBuild.SetActive(false);
		listTowerPlayerBuilt = new List<GameObject> ();	
    }

    void Start()
    {
        if (SceneState.Instance.State == ESceneState.ADVENTURE)
        {
            //Adding PlayDragonManager script
            GameObject go = new GameObject();
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.AddComponent<PlayDragonManager>();
            go.name = "_PlayDragonManager";

            //Adding PlayTouchManager script
            go = new GameObject();
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.AddComponent<PlayTouchManager>();
            go.name = "_PlayTouchManager";

            tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();
        }
    }

    #region UPDATE SOUND-MUSIC
    public void updateValueMusic()
    {
        PlayerInfo.Instance.userInfo.volumeMusic = (int)(SliderMusic.value * 100);
        PlayerInfo.Instance.userInfo.Save();

        Camera.main.GetComponent<AudioSource>().volume = SliderMusic.value;
    }

    public void updateValueSound()
    {
        PlayerInfo.Instance.userInfo.volumeSound = (int)(SliderSound.value * 100);
        PlayerInfo.Instance.userInfo.Save();
    }

    public void updateSliderAudio()
    {
        SliderSound.value = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
        SliderMusic.value = (float)PlayerInfo.Instance.userInfo.volumeMusic / 100;
    }
    #endregion

    #region INIT - TOWER BUILD, HEADER BAR, ITEM BUFF TEMP, RANGE TOWER, TIME SPEED
    void initGameObject()
    {
        //Panel tower build
        panelTowerBuild = footerBar.transform.GetChild(0).gameObject;
        //panel tower upgrade
        panelTowerUpgrade = footerBar.transform.GetChild(1).gameObject;

        tempInit.initalize();
        initHeaderBar();
        initRangeTower();
        initChooseTarget();
        initCheckOK();
        initAchievement();
		initPrefab ();
        if (SceneState.Instance.State == ESceneState.ADVENTURE)
        {
            tempInit.initalizeAdventureMode();
            initItemBuffTemp();
            initPanelDragonInfo();
            editDragCameraTween();
            initFlagTemp();
            initDragonTemp();
            initSkillTemp();
			//initBluetooth();

            AutoDestroy.destroyChildren(itemBuffTemp);
        }
        else if (SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            initStartBattle();
            initTimeSpeed();
            initBluetooth();
        }
    }
	void initPrefab()
	{
		modelPlay.GoldBonus = Resources.Load ("Prefab/Effect/Gold Bonus") as GameObject;	
	}
    void initHeaderBar()
    {
        foreach (Transform child_1 in Camera.main.transform)
        {
            if (child_1.name == "Header Bar")
            {
                headerBar = child_1.gameObject;
                break;
            }
        }
    }

    void initItemBuffTemp()
    {
        itemBuffTemp = new GameObject();
        itemBuffTemp.transform.parent = headerBar.transform.GetChild(0).transform;
        itemBuffTemp.transform.localScale = Vector3.one;
        itemBuffTemp.transform.localPosition = Vector3.zero;
        itemBuffTemp.name = "Item Buff";

        foreach (Transform child in headerBar.transform.GetChild(0).transform)
        {
            if (child.name == "Wave")
            {
                ItemManager.Instance.waveTemp = child.gameObject;
                break;
            }
        }
    }

    void initMusicSound()
    {
        Camera.main.GetComponent<AudioSource>().volume = (float)PlayerInfo.Instance.userInfo.volumeMusic / 100;
    }

    void initRangeTower()
    {
        rangeTower = Instantiate(Resources.Load<GameObject>("Prefab/Tower/Tower Range")) as GameObject;
        rangeTower.layer = rangeTowerBonus.transform.parent.gameObject.layer;
        rangeTower.transform.parent = rangeTowerBonus.transform.parent.transform;
        rangeTower.transform.localPosition = Vector3.zero;
        rangeTower.transform.localScale = Vector3.one;
        rangeTower.transform.GetChild(0).renderer.material.renderQueue = GameConfig.RenderQueueRange + 1;

        rangeTower.SetActive(false);
    }

    void initTowerShop()
    {
        AutoDestroy.destroyChildren(panelTowerBuild, "Selected Tower Build", "Check OK");

        int length = ObjectManager.Instance.Towers.Length;

        for (int i = 0; i < length; i++)
        {
            TowerController controller = ObjectManager.Instance.Towers[i].GetComponent<TowerController>();

            GameObject _towerShop = Instantiate(modelPlay.TowerBuild) as GameObject;
            _towerShop.transform.parent = panelTowerBuild.transform;
            _towerShop.transform.localScale = Vector3.one;

            UIAnchor anchor = _towerShop.GetComponent<UIAnchor>();
            anchor.container = panelTowerBuild.gameObject;
            anchor.relativeOffset = new Vector2(PlayConfig.TowerBuildAnchor.PanelAnchorStart + i * PlayConfig.TowerBuildAnchor.PanelAnchorDistance, 0);

            TowerBuildController towerBuildController = _towerShop.GetComponent<TowerBuildController>();
            towerBuildController.ID = controller.ID;
            towerBuildController.Money = controller.attribute.Cost;
            towerBuildController.Range = controller.attribute.Range;

            UITowerBuild uiTowerBuild = _towerShop.GetComponentInChildren<UITowerBuild>();

            string s = GameConfig.PathTowerIcon + controller.ID.Type.ToString() + "-" + controller.ID.Level;

            uiTowerBuild.texture.mainTexture = Resources.Load<Texture>(s);
            uiTowerBuild.money.text = towerBuildController.Money.ToString();
            uiTowerBuild.range = towerBuildController.Range;


            //set hinh anh stretch lai cho dep
            foreach (Transform child in _towerShop.transform)
            {
                if (child.name == PlayNameHashIDs.Icon)
                {
                    UITexture texture = child.GetComponent<UITexture>();
                    texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;

                    Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
                    texture.SetDimensions((int)localSize.x, (int)localSize.y);
                    texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                    UIStretch uiStretch = child.GetComponent<UIStretch>();
                    uiStretch.container = panelTowerBuild.gameObject;

                    object[] values = PlayConfig.getTowerBuildReso(towerBuildController.ID.Type);
                    //					uiStretch.relativeSize = new Vector2(1, (float)values[0]);

                    //					UIAnchor uiAnchor = child.GetComponent<UIAnchor>();
                    //					uiAnchor.relativeOffset = (Vector2)values[1];
                    anchor.relativeOffset = new Vector2(anchor.relativeOffset.x + ((Vector2)values[1]).x, anchor.relativeOffset.y);

                    break;
                }
            }

            //Set hinh anh va label tower build chua duoc. mua trong shop
            setEnableTower(controller, uiTowerBuild);

            listTowerBuild.Add(_towerShop);

        }
        initTowerPassiveShop();
    }

    void initTowerPassiveShop()
    {
        // AutoDestroy.destroyChildren(panelTowerBuild, "Selected Tower Build", "Check OK");

        int length = ObjectManager.Instance.TowersPassive.Length;

        for (int i = 0; i < length; i++)
        {
            TowerPassiveController controller = ObjectManager.Instance.TowersPassive[i].GetComponent<TowerPassiveController>();

            GameObject _towerShop = Instantiate(modelPlay.TowerBuild) as GameObject;
            _towerShop.transform.parent = panelTowerBuild.transform;
            _towerShop.transform.localScale = Vector3.one;

            UIAnchor anchor = _towerShop.GetComponent<UIAnchor>();
            anchor.container = panelTowerBuild.gameObject;
            anchor.relativeOffset = new Vector2(PlayConfig.TowerBuildAnchor.PanelAnchorStart + (PlayConfig.TowerBuildAnchor.PanelAnchorDistance * listTowerBuild.Count) + (i * PlayConfig.TowerBuildAnchor.PanelAnchorDistance), 0);

            TowerBuildController towerBuildController = _towerShop.GetComponent<TowerBuildController>();
            towerBuildController.ID = controller.ID;
            towerBuildController.Money = controller.attribute.Cost;
            towerBuildController.Range = 0;

            UITowerBuild uiTowerBuild = _towerShop.GetComponentInChildren<UITowerBuild>();
            string s = GameConfig.PathTowerIcon + controller.ID.Type.ToString() + "-" + controller.ID.Level;

            uiTowerBuild.texture.mainTexture = Resources.Load<Texture>(s);
            uiTowerBuild.money.text = towerBuildController.Money.ToString();
            uiTowerBuild.range = towerBuildController.Range;


            //set hinh anh stretch lai cho dep
            foreach (Transform child in _towerShop.transform)
            {
                if (child.name == PlayNameHashIDs.Icon)
                {
                    UITexture texture = child.GetComponent<UITexture>();
                    texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;

                    Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
                    texture.SetDimensions((int)localSize.x, (int)localSize.y);
                    texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                    UIStretch uiStretch = child.GetComponent<UIStretch>();
                    uiStretch.container = panelTowerBuild.gameObject;

                    object[] values = PlayConfig.getTowerBuildReso(towerBuildController.ID.Type);
                    //					uiStretch.relativeSize = new Vector2(1, (float)values[0]);

                    //					UIAnchor uiAnchor = child.GetComponent<UIAnchor>();
                    //					uiAnchor.relativeOffset = (Vector2)values[1];
                    anchor.relativeOffset = new Vector2(anchor.relativeOffset.x + ((Vector2)values[1]).x, anchor.relativeOffset.y);

                    break;
                }
            }

            //Set hinh anh va label tower build chua duoc. mua trong shop
            setEnableTower(controller, uiTowerBuild);

            listTowerBuild.Add(_towerShop);
        }

        if (SceneState.Instance.State == ESceneState.ADVENTURE)
        {
            initHouse();
        }
    }

    void initHouse()
    {
        GameObject _towerShop = Instantiate(modelPlay.TowerBuild) as GameObject;
        _towerShop.transform.parent = panelTowerBuild.transform;
        _towerShop.transform.localScale = Vector3.one;

        UIAnchor anchor = _towerShop.GetComponent<UIAnchor>();
        anchor.container = panelTowerBuild.gameObject;
        anchor.relativeOffset = new Vector2(PlayConfig.TowerBuildAnchor.PanelAnchorStart + (PlayConfig.TowerBuildAnchor.PanelAnchorDistance * (listTowerBuild.Count - 1)) + PlayConfig.TowerBuildAnchor.PanelAnchorDistance, 0);

        STowerID houseID = new STowerID(ETower.DRAGON, 1);

        TowerBuildController towerBuildController = _towerShop.GetComponent<TowerBuildController>();
        towerBuildController.ID = houseID;
        towerBuildController.Money = ReadDatabase.Instance.DragonInfo.House[1].Cost;
        towerBuildController.Range = 0;

        UITowerBuild uiTowerBuild = _towerShop.GetComponentInChildren<UITowerBuild>();
        uiTowerBuild.isEnable = true;

        uiTowerBuild.texture.mainTexture = Resources.Load<Texture>("Image/Button/button-dragon-select");
        uiTowerBuild.money.text = towerBuildController.Money.ToString();
        uiTowerBuild.range = towerBuildController.Range;

        //set hinh anh stretch lai cho dep
        foreach (Transform child in _towerShop.transform)
        {
            if (child.name == PlayNameHashIDs.Icon)
            {
                UITexture texture = child.GetComponent<UITexture>();
                texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;

                Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
                texture.SetDimensions((int)localSize.x, (int)localSize.y);
                texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;

                UIStretch uiStretch = child.GetComponent<UIStretch>();
                uiStretch.relativeSize = new Vector2(1, 0.8f);
                uiStretch.container = panelTowerBuild.gameObject;

                object[] values = PlayConfig.getTowerBuildReso(towerBuildController.ID.Type);
                anchor.relativeOffset = new Vector2(anchor.relativeOffset.x + ((Vector2)values[1]).x, anchor.relativeOffset.y);

                break;
            }
        }

        //set Label
        UILabel label = _towerShop.GetComponentInChildren<UILabel>();
        label.color = PlayConfig.ColorTowerBuildDragonLabelCostForeground;
        label.effectColor = PlayConfig.ColorTowerBuildDragonLabelCostOutline;

        listTowerBuild.Add(_towerShop);
    }

    void setEnableTower(TowerController controller, UITowerBuild uiTowerBuild)
    {
        if (SceneState.Instance.State == ESceneState.ADVENTURE || SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            if (!WaveController.Instance.infoMap.TowerUsed.ToUpper().Contains(controller.ID.Type.ToString()))
            {
                uiTowerBuild.texture.color = Color.black;
                uiTowerBuild.money.text = "?";
                uiTowerBuild.money.color = PlayConfig.TowerBuildConfig.UnableLabelCost;
                uiTowerBuild.money.fontSize = PlayConfig.TowerBuildConfig.UnableFontSize;
                uiTowerBuild.money.GetComponent<UIAnchor>().relativeOffset = PlayConfig.TowerBuildConfig.UnableAnchorOffset;
                uiTowerBuild.isEnable = false;
            }
            else
                uiTowerBuild.isEnable = true;
        }
        else if (SceneState.Instance.State == ESceneState.DAILY_QUEST_3MINS)
        {
            uiTowerBuild.isEnable = true;
        }
    }

    public void initTimeSpeed()
    {
        timeSpeed = Instantiate(Resources.Load<GameObject>("Prefab/Play/TimeSpeed")) as GameObject;
        timeSpeed.GetComponent<UIAnchor>().container = headerBar.gameObject;
        timeSpeed.GetComponent<UIStretch>().container = headerBar.gameObject;
        timeSpeed.transform.parent = headerBar.transform;

        timeSpeed.GetComponent<UITimeSpeed>().sliderTimeSpeed.onChange.Add(new EventDelegate(changeTimeSpeed));
    }

    void initCheckOK()
    {
        checkOK = Instantiate(Resources.Load<GameObject>("Prefab/Play/Check OK")) as GameObject;
        checkOK.transform.parent = panelTowerBuild.transform;
        checkOK.transform.localScale = Vector3.one;
        checkOK.transform.localPosition = Vector3.zero;
        checkOK.name = "Check OK";
        checkOK.GetComponent<UIStretch>().container = footerBar;
        checkOK.SetActive(false);
    }

    void initChooseTarget()
    {
        chooseTarget = Instantiate(Resources.Load<GameObject>("Prefab/Build/Choose Target")) as GameObject;
        chooseTarget.transform.parent = rangeTowerBonus.transform.parent.transform;
        chooseTarget.transform.localPosition = Vector3.zero;
        chooseTarget.transform.localScale = Vector3.one;
        chooseTarget.transform.GetChild(0).renderer.material.renderQueue = GameConfig.RenderQueueDefault;
        chooseTarget.SetActive(false);
    }

    public void initStartBattle()
    {
        startBallte = Instantiate(Resources.Load<GameObject>("Prefab/Play/StartBattle")) as GameObject;
        startBallte.GetComponent<UIAnchor>().container = headerBar.gameObject;
        startBallte.GetComponent<UIStretch>().container = headerBar.gameObject;
        startBallte.transform.localPosition = Vector3.zero;
        startBallte.transform.localScale = Vector3.one;
        startBallte.transform.parent = headerBar.transform;
    }

    void initAchievement()
    {
        achievement = Instantiate(Resources.Load<GameObject>("Prefab/Play/Achievement")) as GameObject;
        achievement.transform.parent = headerBar.transform;
        achievement.transform.localScale = Vector3.one;

        //Anchor
        achievement.GetComponent<UIAnchor>().container = headerBar;

        //Stretch
        achievement.GetComponent<UIStretch>().container = headerBar;

        achievement.SetActive(false);
    }

    // add tutorial detail into level
    public void initTutorial()
    {
        tutorial = Instantiate(Resources.Load<GameObject>("Prefab/Tutorial/Tutorial Play")) as GameObject;
        tutorial.transform.parent = footerBar.transform.root;
        tutorial.transform.localPosition = Vector3.zero;
        tutorial.transform.localScale = Vector3.one;
    }

    void initPanelDragonInfo()
    {
        tempInit.panelDragonInfo = Instantiate(Resources.Load<GameObject>("Prefab/Play/Panel Dragon Info")) as GameObject;
        tempInit.panelDragonInfo.transform.parent = footerBar.transform;

        //Stretch
        tempInit.panelDragonInfo.GetComponent<UIStretch>().container = footerBar;

        foreach (Transform child in tempInit.towerDown.transform)
        {
            UIPlayTween tween = child.GetChild(0).gameObject.AddComponent<UIPlayTween>();
            tween.tweenTarget = tempInit.panelDragonInfo;
            tween.includeChildren = false;
            tween.playDirection = AnimationOrTween.Direction.Reverse;
        }

        foreach (Transform child in tempInit.towerUp.transform)
        {
            UIPlayTween tween = child.GetChild(0).gameObject.AddComponent<UIPlayTween>();
            tween.tweenTarget = tempInit.panelDragonInfo;
            tween.includeChildren = false;
            tween.playDirection = AnimationOrTween.Direction.Reverse;
        }
    }

    void editDragCameraTween()
    {
        GameObject cameraRender = GameObject.FindWithTag("DragCamera");

        UIPlayTween tween = cameraRender.GetComponent<UIPlayTween>();
        tween.tweenTarget = panelTowerBuild;
        tween.includeChildren = false;
        tween.playDirection = AnimationOrTween.Direction.Reverse;

        tween = cameraRender.AddComponent<UIPlayTween>();
        tween.tweenTarget = footerBar.transform.GetChild(1).gameObject;
        tween.includeChildren = false;
        tween.playDirection = AnimationOrTween.Direction.Reverse;

        tween = cameraRender.AddComponent<UIPlayTween>();
        tween.tweenTarget = tempInit.panelDragonInfo;
        tween.includeChildren = false;
        tween.playDirection = AnimationOrTween.Direction.Forward;
    }

    void initFlagTemp()
    {
        modelPlay.Flag = Resources.Load<GameObject>("Prefab/Play/Frag");

        GameObject tempFlag = new GameObject();
        foreach (Transform child in GameObject.FindWithTag("Root").transform)
        {
            if (child.name == "UI Render")
            {
                tempFlag.transform.parent = child;
                break;
            }
        }
        tempFlag.transform.localPosition = Vector3.zero;
        tempFlag.transform.localScale = Vector3.one;
        tempFlag.name = "Temp Flag";
        tempFlag.layer = GameObject.FindWithTag("CameraRender").layer;

        //add uipanel
        UIPanel panel = tempFlag.AddComponent<UIPanel>();
        panel.renderQueue = UIPanel.RenderQueue.StartAt;
        panel.startingRenderQueue = GameConfig.RenderQueueFlag;

        Temp.Flag = tempFlag;
    }

    void initDragonTemp()
    {
        GameObject tempDragon = new GameObject();
        foreach (Transform child in GameObject.FindWithTag("Root").transform)
        {
            if (child.name == "UI Render")
            {
                tempDragon.transform.parent = child;
                break;
            }
        }
        tempDragon.transform.localPosition = Vector3.zero;
        tempDragon.transform.localScale = Vector3.one;
        tempDragon.name = "Temp Dragon";
        tempDragon.layer = GameObject.FindWithTag("CameraRender").layer;

        //add uipanel
        UIPanel panel = tempDragon.AddComponent<UIPanel>();
        panel.depth = 1;
        panel.renderQueue = UIPanel.RenderQueue.StartAt;
        panel.startingRenderQueue = GameConfig.RenderQueueDragon;

        Temp.Dragon = tempDragon;
    }

    void initSkillTemp()
    {
        GameObject tempSkill = new GameObject();
        foreach (Transform child in GameObject.FindWithTag("Root").transform)
        {
            if (child.name == "UI Render")
            {
                tempSkill.transform.parent = child;
                break;
            }
        }
        tempSkill.transform.localPosition = Vector3.zero;
        tempSkill.transform.localScale = Vector3.one;
        tempSkill.name = "Temp Skill";
        tempSkill.layer = GameObject.FindWithTag("CameraRender").layer;

        //add uipanel
        UIPanel panel = tempSkill.AddComponent<UIPanel>();
        panel.depth = 1;
        panel.renderQueue = UIPanel.RenderQueue.StartAt;
        panel.startingRenderQueue = GameConfig.RenderQueueSkill;

        Temp.Skill = tempSkill;
    }

    void initBluetooth()
    {
		initInfoBlueTooth ();
		initBluetoothEnemyShop ();
		//initBluetoothTowerShop ();
    }

	void initInfoBlueTooth()
	{
		infoBluetooth = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Bluetooth/Info Bluetooth")) as GameObject;
		infoBluetooth.GetComponent<UIAnchor>().container = headerBar;
		infoBluetooth.GetComponent<UIStretch>().container = headerBar;
		infoBluetooth.transform.parent = headerBar.transform.parent;
		infoBluetooth.transform.localScale = Vector3.one;
		infoBluetooth.transform.localPosition = Vector3.zero;
	}

	void initBluetoothEnemyShop()
	{
		enemyBluetoothShop = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Bluetooth/Enemy Shop Bluetooth")) as GameObject;
		enemyBluetoothShop.GetComponent<UIAnchor>().container = headerBar;
		enemyBluetoothShop.GetComponent<UIStretch>().container = headerBar;
		enemyBluetoothShop.transform.parent = headerBar.transform.parent;
		enemyBluetoothShop.transform.localScale = Vector3.one;
		enemyBluetoothShop.transform.localPosition = Vector3.zero;
	}

	void initBluetoothTowerShop()
	{
		towerBluetoothShop = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Bluetooth/Tower Shop Bluetooth")) as GameObject;
		towerBluetoothShop.GetComponent<UIAnchor>().container = headerBar;
		towerBluetoothShop.GetComponent<UIStretch>().container = headerBar;
		towerBluetoothShop.transform.parent = headerBar.transform.parent;
		towerBluetoothShop.transform.localScale = Vector3.one;
		towerBluetoothShop.transform.localPosition = Vector3.zero;

	}
    #endregion

    #region RESET - BUILDING, UPGRADE, RANGE TOWER
    public void resetBuilding()
    {
        if (objectBuild.Target != null && objectBuild.Target.activeInHierarchy)
        {

            objectBuild.Target.GetComponentInChildren<UITarget>().reset();
        }

        if (objectBuild.Tower != null)
        {
            objectBuild.Tower.GetComponentInChildren<UITowerBuild>().reset();
        }
        if (checkOK.activeSelf)
            checkOK.SetActive(false);

        objectBuild.Target = null;
        objectBuild.Tower = null;

        // hide choose target
        chooseTarget.SetActive(false);
    }

    public void resetUpgrade()
    {
        if (objectUpgrade.Tower != null)
        {
            if (objectUpgrade.Tower.GetComponentInChildren<UITower>() != null)
                objectUpgrade.Tower.GetComponentInChildren<UITower>().reset();
            else
                objectUpgrade.Tower.GetComponentInChildren<UIHouse>().reset();
        }

        objectUpgrade.Tower = null;
        objectUpgrade.type = EObjectUpgradeType.NONE;
        towerInfoController.setSelected(ETowerInfoType.RESET);
        towerInfoController.hasClickUpgrade = false;

        // hide choose target
        chooseTarget.SetActive(false);
    }

    public void resetRangeTower()
    {
        rangeTower.transform.localScale = Vector3.one;
        rangeTower.SetActive(false);

        rangeTowerBonus.transform.localScale = Vector3.one;
        rangeTowerBonus.SetActive(false);
    }
    #endregion

    #region TOWER - SET RANGE, BUILD, SELL, UPGRADE, BUY ON SHOP
    public void setRangeTower(int iRange, GameObject target)
    {
        if (target != null)
        {
            if (iRange == 0)
            {
                if (rangeTower.activeSelf)
                    rangeTower.SetActive(false);
            }
            else
            {
                rangeTower.transform.position = target.transform.position;
                float scale = (float)iRange / 100f;
                rangeTower.transform.localScale = new Vector3(scale, scale, 0);
                rangeTower.SetActive(true);
            }
        }
    }

    public void buildTower()
    {
        // if target not null and tower not null

        if (objectBuild.Target != null && objectBuild.Tower != null)
        {



            PlayManager.Instance.number_tower_build++;

            STowerID towerID = objectBuild.Tower.GetComponent<TowerBuildController>().ID;

            #region Tower
            if (towerID.Type != ETower.GOLD && towerID.Type != ETower.DRAGON)
            {
                int length = ObjectManager.Instance.Towers.Length;
                for (int i = 0; i < length; i++)
                {
                    TowerController towerController = ObjectManager.Instance.Towers[i].GetComponent<TowerController>();

                    if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
                    {
                        // if not enough money for building tower
                        if (PlayInfo.Instance.Money < towerController.attribute.Cost)
                        {
                            DeviceService.Instance.openToast("Not enought gold :T");
                            return;
                        }

                        // reset range tower
                        resetRangeTower();

                        PlayInfo.Instance.Money -= towerController.attribute.Cost;

                        GameObject building = Instantiate(ObjectManager.Instance.Towers[i]) as GameObject;
                        building.transform.parent = objectBuild.Target.transform;
                        building.transform.localScale = Vector3.one;
                        building.transform.localPosition = Vector3.zero;
                        building.GetComponent<TowerController>().updateTotalMoney(towerController.attribute.Cost);

                        TowerController buildingController = building.GetComponent<TowerController>();

                        setTowerBonus(buildingController);
                        ShowTowers.Add(building);

                        foreach (Transform child in footerBar.transform)
                        {
                            if (child.name == PlayNameHashIDs.PanelTowerBuild)
                            {
                                building.GetComponentInChildren<UIPlayTween>().tweenTarget = child.gameObject;
                                break;
                            }
                        }

                        objectBuild.Target.GetComponentInChildren<UITarget>().reset();
                        objectBuild.Tower.GetComponentInChildren<UITowerBuild>().reset();

                        foreach (Transform child in objectBuild.Target.transform)
                        {
                            if (child.name == PlayNameHashIDs.Target)
                            {
                                //set render queue target -> tower
                                building.GetComponentInChildren<SpriteRenderer>().material.renderQueue = child.GetComponentInChildren<SpriteRenderer>().material.renderQueue;
                                child.gameObject.SetActive(false);
                                break;
                            }
                        }

                        foreach (var tween in footerBar.GetComponentsInChildren<TweenPosition>())
                        {
                            tween.PlayReverse();
                        }


                    if(SceneState.Instance.State == ESceneState.ADVENTURE)
                        tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();

                        foreach (Transform child in building.transform)
                        {
                            if (child.name == "Health")
                            {
                                child.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("CameraRender");
                            }

                            if (child.name == "Collider")
                            {
                                UIPlayTween tween = child.gameObject.AddComponent<UIPlayTween>();
                                tween.tweenTarget = tempInit.panelDragonInfo;
                                tween.includeChildren = false;
                                tween.playDirection = AnimationOrTween.Direction.Reverse;
                            }
                        }

                        NGUITools.SetActive(selectedTowerBuild, false);
						listTowerPlayerBuilt.Add(objectBuild.Target);
                        objectBuild.Target = null;
                        objectBuild.Tower = null;

                        return;
                    }
                }
            }
            #endregion

            #region Tower Passive
            if (towerID.Type == ETower.GOLD)
            {
                int length = ObjectManager.Instance.TowersPassive.Length;
                for (int i = 0; i < length; i++)
                {
                    TowerPassiveController towerController = ObjectManager.Instance.TowersPassive[i].GetComponent<TowerPassiveController>();

                    if (towerController.ID.Type == towerID.Type && towerController.ID.Level == towerID.Level)
                    {
                        // if not enough money for building tower

                        if (PlayInfo.Instance.Money < towerController.attribute.Cost)
                        {
                            DeviceService.Instance.openToast("Not enought gold :T");
                            return;
                        }

                        // reset range tower
                        resetRangeTower();

                        PlayInfo.Instance.Money -= towerController.attribute.Cost;

                        GameObject building = Instantiate(ObjectManager.Instance.TowersPassive[i]) as GameObject;
                        building.transform.parent = objectBuild.Target.transform;
                        building.transform.localScale = Vector3.one;
                        building.transform.localPosition = Vector3.zero;
                        building.GetComponent<TowerPassiveController>().updateTotalMoney(towerController.attribute.Cost);

                        TowerPassiveController buildingController = building.GetComponent<TowerPassiveController>();

                        setTowerBonus(buildingController);
                        ShowTowers.Add(building);

                        foreach (Transform child in footerBar.transform)
                        {
                            if (child.name == PlayNameHashIDs.PanelTowerBuild)
                            {
                                building.GetComponentInChildren<UIPlayTween>().tweenTarget = child.gameObject;
                                break;
                            }
                        }

                        objectBuild.Target.GetComponentInChildren<UITarget>().reset();
                        objectBuild.Tower.GetComponentInChildren<UITowerBuild>().reset();

                        foreach (Transform child in objectBuild.Target.transform)
                        {
                            if (child.name == PlayNameHashIDs.Target)
                            {
                                //set render queue target -> tower
                                building.GetComponentInChildren<SpriteRenderer>().material.renderQueue = child.GetComponentInChildren<SpriteRenderer>().material.renderQueue;
                                child.gameObject.SetActive(false);
                                break;
                            }
                        }

                        foreach (Transform child in building.transform)
                        {
                            if (child.name == "Health")
                            {
                                child.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("CameraRender");
                                break;
                            }
                        }

                        foreach (var tween in footerBar.GetComponentsInChildren<TweenPosition>())
                        {
                            tween.PlayReverse();
                        }

                        NGUITools.SetActive(selectedTowerBuild, false);
						listTowerPlayerBuilt.Add(objectBuild.Target);
//						Debug.Log(listTowerPlayerBuilt.Count);
						objectBuild.Target = null;
                        objectBuild.Tower = null;

                        return;
                    }
                }
            }
            #endregion

            #region Dragon House
            if (towerID.Type == ETower.DRAGON)
            {
                HouseController houseController = ObjectManager.Instance.DragonHouse.GetComponent<HouseController>();
                // if not enough money for building tower
                if (PlayInfo.Instance.Money < houseController.attribute.Cost)
                {
                    DeviceService.Instance.openToast("Not enought gold :T");
                    return;
                }

                // reset range tower
                resetRangeTower();

                PlayInfo.Instance.Money -= houseController.attribute.Cost;

                GameObject building = Instantiate(ObjectManager.Instance.DragonHouse) as GameObject;
                building.transform.parent = objectBuild.Target.transform;
                building.transform.localScale = Vector3.one;
                building.transform.localPosition = Vector3.zero;

                //set panel
                building.GetComponent<UIPanel>().startingRenderQueue = objectBuild.Target.GetComponentInChildren<SpriteRenderer>().material.renderQueue + 50;

                HouseController buildingController = building.GetComponent<HouseController>();
                buildingController.updateTotalMoney(buildingController.attribute.Cost);
                building.GetComponent<HouseAction>().countdown.GetComponent<UIStretch>().container = tempInit.cameraRender;

                ShowTowers.Add(building);

                foreach (Transform child in footerBar.transform)
                {
                    if (child.name == PlayNameHashIDs.PanelTowerBuild)
                    {
                        building.GetComponentInChildren<UIPlayTween>().tweenTarget = child.gameObject;
                        break;
                    }
                }

                objectBuild.Target.GetComponentInChildren<UITarget>().reset();
                objectBuild.Tower.GetComponentInChildren<UITowerBuild>().reset();

                foreach (Transform child in objectBuild.Target.transform)
                {
                    if (child.name == PlayNameHashIDs.Target)
                    {
                        //set render queue target -> tower
                        int renderQueue = child.GetComponentInChildren<SpriteRenderer>().material.renderQueue;
                        building.GetComponentInChildren<SpriteRenderer>().material.renderQueue = renderQueue;
                        child.gameObject.SetActive(false);
                        break;
                    }
                }

                foreach (Transform child in building.transform)
                {
                    if (child.name == "Health")
                    {
                        child.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("CameraRender");
                        break;
                    }
                }



                foreach (var tween in footerBar.GetComponentsInChildren<TweenPosition>())
                {
                    tween.PlayReverse();
                }

                NGUITools.SetActive(selectedTowerBuild, false);

                objectBuild.Target = null;
                objectBuild.Tower = null;

                //set Max baby
                PlayDragonManager.Instance.maxBaby = buildingController.attribute.LimitChild;
            }
            #endregion
        }
    }

    public void sellTower()
    {
        if (objectUpgrade.Tower != null)
        {
            //update Money
            if (towerInfoController.isOnTowerInfo)
            {
                PlayInfo.Instance.Money += objectUpgrade.Tower.GetComponent<TowerController>().totalMoney / 2;
                objectUpgrade.Tower.GetComponentInChildren<TowerAnimation>().sell(objectUpgrade.Tower);
            }
            else
            {
                HouseController controller = objectUpgrade.Tower.GetComponent<HouseController>();
                PlayInfo.Instance.Money += controller.totalMoney / 2;
                controller.StateAction = EHouseStateAction.DESTROY;

            }

            foreach (var tween in PlayManager.Instance.footerBar.GetComponentsInChildren<TweenPosition>())
            {
                tween.PlayReverse();
            }

			listTowerPlayerBuilt.Remove(objectUpgrade.Tower.transform.parent.transform.gameObject);

            ShowTowers.Remove(objectUpgrade.Tower);
        }
    }
	public void DestroyRandomTower()
	{
		if (listTowerPlayerBuilt.Count == 0)
						return;

		int index = Random.Range(0, listTowerPlayerBuilt.Count);
		GameObject target = listTowerPlayerBuilt [index];
		GameObject tower = target.transform.GetChild(1).transform.gameObject;
		
		tower.GetComponentInChildren<TowerAnimation>().sell(tower);

		ShowTowers.Remove(tower);
		listTowerPlayerBuilt.RemoveAt(index);
		
		objectBuild.Target = null;
		objectBuild.Tower = null;
	}
	public void DecreaseLevelRandomTower()
	{
		if (listTowerPlayerBuilt.Count == 0)
			return;
		
		int index = Random.Range(0, listTowerPlayerBuilt.Count);
		GameObject target = listTowerPlayerBuilt [index];
		GameObject tower = target.transform.GetChild(1).transform.gameObject;
		TowerController towerController = tower.GetComponent<TowerController> ();
		string name = towerController.attribute.Name;
		int towerLevel = int.Parse (name.Substring (name.LastIndexOf (" ") + 1));
		if (towerLevel > 1) { // cap 2,3

			//tao tru moi
			int newTowerLevel = towerLevel-1;
			string towerType = name.Substring(0,name.IndexOf(" "));
	
			GameObject newTower = Instantiate(Resources.Load<GameObject>("Prefab/Tower/" + towerType 
			                                                             + "/Tower " + towerType + " " + newTowerLevel)) as GameObject;
		
			//TowerController newTowerController = newTower.GetComponent<TowerController>();
			//setTowerBonus(newTowerController);
			newTower.transform.parent = tower.transform.parent;
			newTower.transform.localPosition = Vector3.zero;
			newTower.transform.localScale = Vector3.one;
			
			//set render queue tower cu~ -> tower moi'
			newTower.GetComponentInChildren<SpriteRenderer>().material.renderQueue = tower.GetComponentInChildren<SpriteRenderer>().material.renderQueue;
			
			foreach (Transform child in newTower.transform)
			{
				if (child.name == "Health")
				{
					child.GetChild(1).GetComponent<UIStretch>().container = tempInit.cameraRender;
					break;
				}
			}
			
			
			ShowTowers.Remove(tower);
			ShowTowers.Add(newTower);
			
			//disable footer bar
			foreach (var tween in footerBar.GetComponentsInChildren<TweenPosition>())
			{
				tween.PlayReverse();
			}
			//delete current tower
			Destroy(tower);
			
			resetBuilding();
			resetUpgrade();
			resetRangeTower();

		} 
		else { // cap 1
			foreach (Transform child in tower.transform)
			{
				if (child.name == "Health")
				{
					child.transform.gameObject.SetActive(false);
					break;
				}
			}
			tower.GetComponentInChildren<TowerAnimation>().sell(tower);

			ShowTowers.Remove(tower);
			listTowerPlayerBuilt.RemoveAt(index);
			
			objectBuild.Target = null;
			objectBuild.Tower = null;	
		}
			
	}

    public void upgradeTower()
    {
        if (objectUpgrade.Tower != null)
        {
            int cost = 0;
            if (towerInfoController.isOnTowerInfo)
            {
                TowerController towerController = objectUpgrade.Tower.GetComponent<TowerController>();
                if (PlayInfo.Instance.Money < towerController.nextLevel.attribute.Cost)
                    return;

                cost = towerController.nextLevel.attribute.Cost;
            }
            else
            {
                HouseController houseController = objectUpgrade.Tower.GetComponent<HouseController>();
                cost = ReadDatabase.Instance.DragonInfo.House[houseController.ID.Level + 1].Cost;

                if (PlayInfo.Instance.Money < cost)
                    return;
            }

            //trừ tiền
            PlayInfo.Instance.Money -= cost;

            GameObject newTower = null;

            //add next tower
            if (towerInfoController.isOnTowerInfo)
            {
                TowerController preTower = objectUpgrade.Tower.GetComponent<TowerController>();

                newTower = Instantiate(preTower.nextLevel.gameObject) as GameObject;

                TowerController newTowerController = newTower.GetComponent<TowerController>();
                newTowerController.updateTotalMoney(newTowerController.attribute.Cost + preTower.totalMoney);

                setTowerBonus(newTowerController);
            }
            else
            {
                newTower = Instantiate(ObjectManager.Instance.DragonHouse) as GameObject;

                HouseController preHouse = objectUpgrade.Tower.GetComponent<HouseController>();
                HouseController newHouseController = newTower.GetComponent<HouseController>();
                GameSupportor.transferHouseDragonData(newHouseController, preHouse.ID.Level + 1);

                //set panel
                newHouseController.GetComponent<UIPanel>().startingRenderQueue = preHouse.GetComponent<UIPanel>().startingRenderQueue;

                //count total money
                newHouseController.updateTotalMoney(newHouseController.attribute.Cost + preHouse.totalMoney);

                //set Max baby
                PlayDragonManager.Instance.maxBaby = newHouseController.attribute.LimitChild;
            }

            newTower.transform.parent = objectUpgrade.Tower.transform.parent;
            newTower.transform.localPosition = Vector3.zero;
            newTower.transform.localScale = Vector3.one;

            //set render queue tower cu~ -> tower moi'
            newTower.GetComponentInChildren<SpriteRenderer>().material.renderQueue = objectUpgrade.Tower.GetComponentInChildren<SpriteRenderer>().material.renderQueue;

            foreach (Transform child in newTower.transform)
            {
                if (child.name == "Health")
                {
                    child.GetChild(1).GetComponent<UIStretch>().container = tempInit.cameraRender;
                    break;
                }
            }


            ShowTowers.Remove(objectUpgrade.Tower);
            ShowTowers.Add(newTower);

            //disable footer bar
            foreach (var tween in footerBar.GetComponentsInChildren<TweenPosition>())
            {
                tween.PlayReverse();
            }
            //delete current tower
            Destroy(objectUpgrade.Tower.gameObject);

            resetBuilding();
            resetUpgrade();
            resetRangeTower();
        }
    }

    public void buyTower(TowerShopController towerShopController, bool isPaymentMoney)
    {
        if (PlayInfo.Instance.Money < towerShopController.Money && isPaymentMoney)
        {
            DeviceService.Instance.openToast("Not enought money!");
            return;
        }
        else if (PlayerInfo.Instance.userInfo.diamond < towerShopController.Diamond && !isPaymentMoney)
        {
            DeviceService.Instance.openToast("Not enought diamond!");
            return;
        }

        //buy tower
        towerShopController.GetComponentInChildren<UITowerShop>().type = ETowerShopState.TOWER_BUY_UNABLE;
        ShopController.Instance.towerShopType[towerShopController.Index] = ETowerShopState.TOWER_BUY_UNABLE;

        //tru tien shop va play scene
        if (isPaymentMoney)
            PlayInfo.Instance.Money -= towerShopController.Money;
        else
        {
            PlayerInfo.Instance.userInfo.diamond -= towerShopController.Diamond;
            PlayerInfo.Instance.userInfo.Save();
        }

        foreach (GameObject towerBuild in listTowerBuild)
        {
            TowerBuildController towerBuildController = towerBuild.GetComponent<TowerBuildController>();
            if (towerBuildController.ID.Type == towerShopController.ID.Type)
            {
                UITowerBuild uiTowerBuild = towerBuild.GetComponentInChildren<UITowerBuild>();
                uiTowerBuild.isEnable = true;
                uiTowerBuild.texture.color = Color.white;
                uiTowerBuild.money.text = towerBuildController.Money.ToString();
                uiTowerBuild.money.color = PlayConfig.TowerBuildConfig.EnableLabelCost;
                uiTowerBuild.money.fontSize = PlayConfig.TowerBuildConfig.EnableFontSize;

                UIAnchor anchor = uiTowerBuild.money.GetComponent<UIAnchor>();
                anchor.enabled = true;
                anchor.relativeOffset = PlayConfig.TowerBuildConfig.EnableAnchorOffset;
                break;
            }
        }

        //close shop panel and payment panel
        Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
        isOnShop = false;
        ShopController.Instance.paymentTowerPanel.SetActive(false);
        PlayPanel.Instance.Shop.SetActive(false);
    }
    #endregion

    #region ITEM TOWER - ATK, SPAWN SHOOT, RANGE
    public void setTowerBonus(TowerController towerController)
    {
        if (ItemManager.Instance.listItemState.Contains(EItemState.ATK))
        {
            towerController.Bonus.ATK = (int)((towerController.attribute.MinATK + towerController.attribute.MaxATK) * ItemManager.Instance.BonusATK / 2);
        }
        if (ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
        {
            towerController.Bonus.SpawnShoot = towerController.attribute.SpawnShoot * ItemManager.Instance.BonusSpawnShoot;
        }
        if (ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
        {
            towerController.Bonus.Range = (int)(towerController.attribute.Range * ItemManager.Instance.BonusRange);
            ((SphereCollider)towerController.collider).radius = towerController.attribute.Range + towerController.Bonus.Range;
        }
    }

    public void setTowerBonus()
    {
        foreach (var i in ShowTowers)
        {
            TowerController towerController = (i as GameObject).GetComponent<TowerController>();

            if (ItemManager.Instance.listItemState.Contains(EItemState.ATK))
            {
                towerController.Bonus.ATK = (int)((towerController.attribute.MinATK + towerController.attribute.MaxATK) * ItemManager.Instance.BonusATK / 2);
            }
            if (ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
            {
                towerController.Bonus.SpawnShoot = towerController.attribute.SpawnShoot * ItemManager.Instance.BonusSpawnShoot;
            }
            if (ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
            {
                towerController.Bonus.Range = (int)(towerController.attribute.Range * ItemManager.Instance.BonusRange);
                ((SphereCollider)towerController.collider).radius = towerController.attribute.Range + towerController.Bonus.Range;
            }
        }
    }

    public void resetTowerBonus(TowerController towerController)
    {
        if (!ItemManager.Instance.listItemState.Contains(EItemState.ATK))
        {
            towerController.Bonus.ATK = 0;
        }
        if (!ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
        {
            towerController.Bonus.SpawnShoot = 0.0f;
        }
        if (!ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
        {
            ((SphereCollider)towerController.collider).radius = towerController.attribute.Range - towerController.Bonus.Range;
            towerController.Bonus.Range = 0;
        }
    }

    public void resetTowerBonus()
    {
        foreach (var i in ShowTowers)
        {
            TowerController towerController = (i as GameObject).GetComponent<TowerController>();

            if (!ItemManager.Instance.listItemState.Contains(EItemState.ATK))
            {
                towerController.Bonus.ATK = 0;
            }
            if (!ItemManager.Instance.listItemState.Contains(EItemState.SPAWN_SHOOT))
            {
                towerController.Bonus.SpawnShoot = 0.0f;
            }
            if (!ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
            {
                ((SphereCollider)towerController.collider).radius = towerController.attribute.Range - towerController.Bonus.Range;
                towerController.Bonus.Range = 0;
            }
        }
    }
    #endregion

    #region GAME PANEL - GAMEOVER, VICTORY, TUTORIAL
    public void showGameOver()
    {
        StartCoroutine(waitForGameOver());
    }

    IEnumerator waitForGameOver()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0.0f;
        PlayPanel.Instance.GameOver.SetActive(true);
    }

    public void showVictory()
    {
        // set star success
        float temp = (float)WaveController.Instance.infoMap.Heart / 4;
        int starSuccess = 0;

        if (PlayInfo.Instance.Heart >= (int)(temp * 3)) // star = 3
        {
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap, 3, 3));
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap + 1, 0, 3));
            starSuccess = 3;
        }
        else if (PlayInfo.Instance.Heart >= (int)(temp * 2)) //star = 2
        {
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap, 2, 3));
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap + 1, 0, 3));
            starSuccess = 2;
        }
        else //star = 1
        {
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap, 1, 3));
            PlayerInfo.Instance.addMap(new PlayerMap(WaveController.Instance.currentMap + 1, 0, 3));
            starSuccess = 1;
        }

        int Length = PlayPanel.Instance.Stars.Length;
        for (int i = 0; i < Length; i++)
        {
            PlayPanel.Instance.Stars[i].transform.localScale = Vector3.zero;

            if (i < starSuccess)
                PlayPanel.Instance.Stars[i].GetComponent<UISprite>().spriteName = "icon_star2-on";
            else
                PlayPanel.Instance.Stars[i].GetComponent<UISprite>().spriteName = "icon_star2-off";
        }

        StartCoroutine(waitForVictory(Length));
    }

    IEnumerator waitForVictory(int length)
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0.0f;
        PlayPanel.Instance.Victory.SetActive(true);

        for (int i = 0; i < length; i++)
        {
            PlayPanel.Instance.Stars[i].GetComponent<TweenScale>().PlayForward();
        }
    }

    #region TUTORIAL
    public void setInstructionEnable()
    {
        PlayerInfo.Instance.setUserInstruction(PlayPanel.Instance.Tutorial.GetComponentInChildren<UIToggle>().value);
    }

    public void setInstructionEnableFromOption()
    {
        PlayerInfo.Instance.setUserInstruction(PlayPanel.Instance.Option.GetComponentInChildren<UIToggle>().value);
    }

    public void showMission(string mapName, int heart, int gold, int waves)
    {
        Time.timeScale = 0.0f;
        isZoom = false;
        PlayPanel.Instance.Tutorial.SetActive(true);

        GameObject mission = Instantiate(modelTutorial.Mission) as GameObject;
        mission.transform.parent = PlayPanel.Instance.Tutorial.transform;
        mission.transform.localScale = Vector3.one;

        //set text
        MissionController controller = mission.GetComponent<MissionController>();
        controller.initMission(mapName, heart, gold, waves);

        //set resolution
        mission.GetComponent<UIAnchor>().container = PlayPanel.Instance.Tutorial;
        mission.GetComponent<UIStretch>().container = PlayPanel.Instance.Tutorial;

        //set Tween
        TweenScale tween = mission.GetComponent<TweenScale>();
        tween.AddOnFinished(PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().setEnable);

        //set close mode tutorial panel
        PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().type = ETutorialButton.CLOSE_MISSION_AND_CHECK_INSTRUCTION;
    }

    public void showMission(string s)
    {
        Time.timeScale = 0.0f;
        PlayPanel.Instance.Tutorial.SetActive(true);

        GameObject mission = Instantiate(modelTutorial.Mission) as GameObject;
        mission.transform.parent = PlayPanel.Instance.Tutorial.transform;
        mission.transform.localScale = Vector3.one;

        //set text
        MissionController controller = mission.GetComponent<MissionController>();
        controller.initMission(s);

        //set resolution
        mission.GetComponent<UIAnchor>().container = PlayPanel.Instance.Tutorial;
        mission.GetComponent<UIStretch>().container = PlayPanel.Instance.Tutorial;

        //set Tween
        TweenScale tween = mission.GetComponent<TweenScale>();
        tween.AddOnFinished(PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().setEnable);

        //set close mode tutorial panel
        PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().type = ETutorialButton.CLOSE_MISSION_AND_CHECK_INSTRUCTION;
    }

    public void showInstruction()
    {
        if (PlayerInfo.Instance.userInfo.instruction == 0)
        {
            return;
        }

        Time.timeScale = 0.0f;
        PlayPanel.Instance.Tutorial.SetActive(true);
        PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().setEnable();

        GameObject instruction = Instantiate(modelTutorial.Instruction) as GameObject;
        instruction.transform.parent = PlayPanel.Instance.Tutorial.transform;
        instruction.transform.localScale = Vector3.one;

        //set resolution
        instruction.GetComponent<UIAnchor>().container = PlayPanel.Instance.Tutorial;
        instruction.GetComponent<UIStretch>().container = PlayPanel.Instance.Tutorial;

        //set text
        InstructionController controller = instruction.GetComponent<InstructionController>();
        controller.currentPage = 1;
        controller.setPage();
        controller.setText();

        //set mode tutorial panel
        if (controller.currentPage < PlayConfig.PagesInstruction)
        {
            PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().type = ETutorialButton.NEXT_PAGE_INSTRUCTION;
            controller.ToggleStartup.SetActive(false);
        }
        else
        {
            PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().type = ETutorialButton.CLOSE_MISSION;
            controller.ToggleStartup.SetActive(true);
            controller.GetComponent<UIToggle>().onChange.Add(new EventDelegate(setInstructionEnable));
        }
    }

    public void showTutorialNewEnemy(EnemyController enemy)
    {
        Time.timeScale = 0.0f;
        PlayPanel.Instance.Tutorial.SetActive(true);

        GameObject newEnemy = Instantiate(modelTutorial.NewEnemy) as GameObject;
        newEnemy.transform.parent = PlayPanel.Instance.Tutorial.transform;
        newEnemy.transform.localScale = Vector3.one;

        //Set resolution
        newEnemy.GetComponent<UIAnchor>().container = PlayPanel.Instance.Tutorial;
        newEnemy.GetComponent<UIStretch>().container = PlayPanel.Instance.Tutorial;

        //set Tween
        TweenScale tween = newEnemy.GetComponent<TweenScale>();
        tween.AddOnFinished(PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().setEnable);

        //set close mode tutorial panel
        PlayPanel.Instance.Tutorial.GetComponent<UITutorial>().type = ETutorialButton.CLOSE_TUTORIAL;

        NewEnemyController controller = newEnemy.GetComponent<NewEnemyController>();
        controller.Image.mainTexture = Resources.Load<Texture>("Image/Enemy/00 Guide Icon/" + enemy.ID.ToLower());
        controller.labelLevel.text = enemy.level.ToString();
        controller.labelHP.text = enemy.attribute.HP.Max.ToString();
        controller.labelDEF.text = enemy.attribute.DEF.ToString();
        controller.labelCoin.text = enemy.money.ToString();
        controller.labelRegion.text = enemy.region.ToString();
        controller.labelSpeed.text = PlayConfig.getSpeedString(enemy.speed);

        //set visible boss icon
        if (enemy.level >= 6)
            controller.Banner.spriteName = "play-newboss";
        else
            controller.Banner.spriteName = "play-newenemy";

        //set level sprite

        controller.spriteLevel.spriteName = "play-level-" + enemy.level;

        //set name
        controller.Name.text = enemy.attribute.Name;

        //set name color
        Color[] colors = PlayConfig.getColorEnemyName(enemy.level);
        controller.Name.color = colors[0];
        controller.Name.effectColor = colors[1];
    }

    public void WaitInstruction()
    {
        StartCoroutine(waitForInstruction());
    }

    IEnumerator waitForInstruction()
    {
        yield return new WaitForSeconds(PlayConfig.TimeWaitInstructionAfterMission);
        showInstruction();
    }
    #endregion
    #endregion

    #region CALCULATE DAMAGE
    public void pushDamagePhysics(EnemyController enemyController, TowerController towerController)
    {
        enemyController.attribute.HP.Current -= Random.Range(towerController.attribute.MinATK + towerController.Bonus.ATK, towerController.attribute.MaxATK
            + towerController.Bonus.ATK) - enemyController.attribute.DEF;
        if (enemyController.attribute.HP.Current <= 0)
            enemyController.attribute.HP.Current = 0;
        enemyController.updateHP();
    }

    public void pushDamagePhysics(EnemyController enemyController, int TowerATK)
    {
        enemyController.attribute.HP.Current -= (TowerATK - enemyController.attribute.DEF);
        if (enemyController.attribute.HP.Current <= 0)
            enemyController.attribute.HP.Current = 0;
        enemyController.updateHP();
    }

    public int pushDamagePhysics(int minATK, int maxATK, int DEF)
    {
        return Random.Range(minATK, maxATK) - DEF;
    }

    public void pushDamageEffect(EnemyController enemyController, int damage)
    {
        enemyController.attribute.HP.Current -= damage;
        if (enemyController.attribute.HP.Current <= 0)
            enemyController.attribute.HP.Current = 0;
        enemyController.updateHP();
    }
    #endregion

    #region EVENT - OPEN SHOP WITH TOWER SELECTED, EVENT CHANGE TIME SPEED
    public void openShopWithTowerSelected(STowerID ID)
    {
        //Open shop panel
        Time.timeScale = 0.0f;
        PlayPanel.Instance.Shop.SetActive(true);

        ShopController.Instance.checkInitArrShopType();

        //Click tower button
        PlayPanel.Instance.Shop.GetComponent<ShopController>().loadTower();

        //Click tower shop selected - show tower selected info
        PlayPanel.Instance.Shop.GetComponent<ShopController>().loadInfoTower(ID);
    }

    private void changeTimeSpeed()
    {
        UITimeSpeed uiTimeSpeed = timeSpeed.GetComponent<UITimeSpeed>();
        PlayerInfo.Instance.userInfo.timeScale = 1 + uiTimeSpeed.sliderTimeSpeed.value * (uiTimeSpeed.sliderTimeSpeed.numberOfSteps - 1) / 4;
        PlayerInfo.Instance.userInfo.Save();
        Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
        uiTimeSpeed.labelText.text = "x " + PlayerInfo.Instance.userInfo.timeScale.ToString();
    }

    #endregion

    #region DAILY QUEST - 3 MINS CHALLENGE
    public void dailyQuestEvent_enemyDie(Transform position)
    {
        switch (SceneState.Instance.State)
        {
            case ESceneState.DAILY_QUEST_3MINS:
                DQ3MinManager.Instance.checkEnemyDie(position);
                break;
        }
    }
    #endregion

    #region DEVICE - BACK, HOME (UPDATE)
    void Update()
    {
        //BACK
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PlayPanel.Instance.Option.activeSelf)
            {
                if (Time.timeScale == 0.0f)
                    return;

                Time.timeScale = 0.0f;

                PlayPanel.Instance.Option.SetActive(true);
            }
        }
        //HOME
        if (Input.GetKeyDown(KeyCode.Home))
        {
            if (!PlayPanel.Instance.Pause.activeSelf)
            {
                if (Time.timeScale == 0.0f)
                    return;

                Time.timeScale = 0.0f;
                PlayPanel.Instance.Pause.SetActive(true);
            }
        }
    }
    #endregion
}
