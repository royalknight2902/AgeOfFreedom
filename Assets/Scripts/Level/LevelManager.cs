using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager>
{
	public GameObject mockRoutine;
	public MapInfoController mapInfoController;
	public LevelModel Model;
    public LevelObject Objects;
    public GameObject[] buttonHiden;

	private GameObject root;
	private GameObject[] m_objMocks;
	[HideInInspector]
    public GameObject tutorial;

    void Awake()
    {
        if (SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            for (int i = 0; i < buttonHiden.Length; i++)
            {
                buttonHiden[i].SetActive(false);
            }
        }
    }

	void Start ()
	{
        initGameObject();
		root = GameObject.FindGameObjectWithTag("Root");

		foreach(Transform child in mockRoutine.transform)
		{
			child.GetComponent<UIAnchor>().enabled = true;
		}

		getMocks();

        if (PlayerInfo.Instance.userInfo.new_player == 0)
        {
            LevelPanel.Instance.WelcomeGift.SetActive(true);
        }
        else
        {
            if (SceneState.Instance.State == ESceneState.ADVENTURE)
            {
                if (PlayerInfo.Instance.userInfo.checkTutorialLevel == 0)
                {
                    initTutorial();
                    // da hien thi trong lan choi nay
                    PlayerInfo.Instance.userInfo.checkTutorialLevel = 1;
                    PlayerInfo.Instance.userInfo.Save();
                }
            }
            else if (SceneState.Instance.State == ESceneState.BLUETOOTH)
            {
                if (BluetoothManager.Instance.desiredMode == BluetoothMultiplayerMode.Server)
                {

                }
                else if (BluetoothManager.Instance.desiredMode == BluetoothMultiplayerMode.Client)
                {
                    GameObject wait_server = Instantiate(Resources.Load<GameObject>("Prefab/Bluetooth/Wait Bluetooth")) as GameObject;
                    wait_server.GetComponent<UIStretch>().container = mockRoutine.transform.parent.gameObject;
                    wait_server.GetComponent<UIAnchor>().container = mockRoutine.transform.parent.gameObject;
                    wait_server.transform.parent = mockRoutine.transform.root;
                    wait_server.transform.localScale = Vector3.one;
                    wait_server.transform.localPosition = Vector3.zero;
                }
                else
                {
                    DeviceService.Instance.openToast("Connect unsuccess!");
                    BluetoothManager.Instance.disconnectNetWork();
                    BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.None;
                    BluetoothMultiplayerAndroid.Disconnect();
                    Application.LoadLevel("Menu");
                }
            }
        }
	}

    void initGameObject()
    {
        Model.StarSucceed = Resources.Load<GameObject>("Prefab/Level/StarSuccess");
        Model.StarUnsucceed = Resources.Load<GameObject>("Prefab/Level/StarUnsuccess");
        Model.Unlock = Resources.Load<GameObject>("Prefab/Level/Unlock");
        Model.Quest = Resources.Load<GameObject>("Prefab/Level/Daily Quest/Quest");
        Model.Achievement = Resources.Load<GameObject>("Prefab/Level/Achievement");
        Model.DragonItem = Resources.Load<GameObject>("Prefab/Dragon/Dragon Items/tempItem");
    }   

	public void getMocks()
	{
        int lenghtMock = mockRoutine.transform.childCount;
        m_objMocks = new GameObject[lenghtMock];

        for (int i = 0; i < lenghtMock; i++)
        {
            m_objMocks[i] = mockRoutine.transform.GetChild(i).gameObject;
        }

        #region BLUETOOTH

        // khi choi o che do bluetooth, mo 5 map cho nguoi choi
        if (SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject objUnLock = Instantiate(Model.Unlock) as GameObject;
                objUnLock.transform.parent = m_objMocks[i].transform;
                objUnLock.transform.localPosition = Vector3.one;
                objUnLock.transform.localScale = Vector3.one;

                m_objMocks[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            return;
        }

        #endregion

        #region ADVENTURE

        Dictionary<int, PlayerMap> playerMaps = PlayerInfo.Instance.listMap;

        UISprite uiStar = Model.StarSucceed.GetComponent<UISprite>();
        float starWidth = uiStar.width * 0.8f;
        //float starHeight = uiStar.height;
        UISprite uiUnlock = Model.Unlock.GetComponent<UISprite>();
        //float unlockWidth = uiUnlock.width;
        float unlockHeight = uiUnlock.height;

        for (int j = 0; j < lenghtMock; j++)
        {
            int mapID = m_objMocks[j].GetComponent<MockController>().mapID;

            if (playerMaps.ContainsKey(mapID))
            {
                //m_objMocks[j].GetComponent<MockController>().mapName = playerMaps[mapID].
                // show unlock
                GameObject objUnLock = Instantiate(Model.Unlock) as GameObject;
                objUnLock.transform.parent = m_objMocks[j].transform;
                objUnLock.transform.localPosition = Vector3.one;
                objUnLock.transform.localScale = Vector3.one;
                objUnLock.GetComponent<UIStretch>().container = m_objMocks[j].transform.GetChild(0).gameObject;
                objUnLock.GetComponent<UIUnlock>().starSuccess = playerMaps[mapID].starSuccess;

                // hide lock
                m_objMocks[j].transform.GetChild(0).gameObject.SetActive(false);

                // show star
                Vector3 temp = new Vector3(-(float)(playerMaps[mapID].starTotal - 1) / 2 * starWidth, unlockHeight / 2, 0);
                for (int k = 0; k < playerMaps[mapID].starSuccess; k++)
                {
                    Vector3 starPosition = temp + new Vector3(starWidth * k, 0, 0);
                    GameObject objStarSuccess = Instantiate(Model.StarSucceed) as GameObject;
                    objStarSuccess.transform.parent = objUnLock.transform;
                    objStarSuccess.transform.localPosition = starPosition;
                    objStarSuccess.transform.localScale = Vector3.zero;
                    tweenStarScale(objStarSuccess, Vector3.one);
                }

                for (int k = playerMaps[mapID].starSuccess; k < playerMaps[mapID].starTotal; k++)
                {
                    Vector3 starPosition = temp + new Vector3(starWidth * k, 0, 0);
                    GameObject objStarSuccess = Instantiate(Model.StarUnsucceed) as GameObject;
                    objStarSuccess.transform.parent = objUnLock.transform;
                    objStarSuccess.transform.localPosition = starPosition;
                    objStarSuccess.transform.localScale = Vector3.zero;
                    tweenStarScale(objStarSuccess, Vector3.one);
                }
            }
        }
        #endregion
	}

    // add tutorial detail into level
	public void initTutorial()
	{
		tutorial = Instantiate(Resources.Load<GameObject>("Prefab/Tutorial/Tutorial Level")) as GameObject;
		tutorial.transform.parent = root.transform;
		tutorial.transform.localPosition = Vector3.zero;
		tutorial.transform.localScale = Vector3.one;
	}

	void tweenStarScale(GameObject star, Vector3 scaleTo)
	{
		iTween.ScaleTo(star, iTween.Hash(
			iT.ScaleTo.time, 0.8f,
			iT.ScaleTo.scale, scaleTo,
			iT.ScaleTo.easetype, iTween.EaseType.easeOutBack
			));
	}

    #region ACTION - DRAGON HOUSE
    public void openDragonHosuse()
    {
        StartCoroutine(waitToOpenDragonHouse(0.1f));
    }

    public void closeDragonHouse()
    {
        StartCoroutine(waitToCloseDragonHouse(0.1f));
    }

    IEnumerator waitToOpenDragonHouse(float time)
    {
        GameObject dragonButton = LevelManager.Instance.Objects.btnDragon;
        GameObject bagButton = LevelManager.Instance.Objects.btnBag;
        GameObject dragonHouse = LevelManager.Instance.Objects.btnDragonHouse;

        //Clone toa do hien tai cua cac button
        Vector3 currPositionDragonButton = new Vector3(dragonButton.transform.localPosition.x, dragonButton.transform.localPosition.y, dragonButton.transform.localPosition.z);
        Vector3 currPositionBagButton = new Vector3(bagButton.transform.localPosition.x, bagButton.transform.localPosition.y, bagButton.transform.localPosition.z);
        //Vector3 currPositionDragonHouse = new Vector3(dragonHouse.transform.localPosition.x,dragonHouse.transform.localPosition.y,dragonHouse.transform.localPosition.z);
        dragonButton.transform.localPosition = bagButton.transform.localPosition = Vector3.one;
        float stepDragonButton = -currPositionDragonButton.y / 10;
        float stepBagButton = -currPositionBagButton.y / 10;

        dragonButton.SetActive(true);
        bagButton.SetActive(true);
        while (true)
        {
            if (dragonButton.transform.localPosition.y <= currPositionDragonButton.y)
            {
                //dragonHouse.renderer.enabled = false;
                yield break;
            }
            //Debug.Log(2);
            if (dragonButton.transform.localPosition.y - stepDragonButton < currPositionDragonButton.y)
                dragonButton.transform.localPosition = currPositionDragonButton;
            else
                dragonButton.transform.localPosition = new Vector3(dragonButton.transform.localPosition.x, dragonButton.transform.localPosition.y - stepDragonButton, dragonButton.transform.localPosition.z);
            if (bagButton.transform.localPosition.y - stepBagButton < currPositionBagButton.y)
                bagButton.transform.localPosition = currPositionBagButton;
            else
                bagButton.transform.localPosition = new Vector3(bagButton.transform.localPosition.x, bagButton.transform.localPosition.y - stepBagButton, bagButton.transform.localPosition.z);
            yield return new WaitForSeconds(time / 10);
        }

    }

    IEnumerator waitToCloseDragonHouse(float time)
    {

        GameObject dragonButton = LevelManager.Instance.Objects.btnDragon;
        GameObject bagButton = LevelManager.Instance.Objects.btnBag;
        GameObject dragonHouse = LevelManager.Instance.Objects.btnDragonHouse;

        //Clone toa do cua cac nut
        Vector3 currPositionDragonButton = new Vector3(dragonButton.transform.localPosition.x, dragonButton.transform.localPosition.y, dragonButton.transform.localPosition.z);
        Vector3 currPositionBagButton = new Vector3(bagButton.transform.localPosition.x, bagButton.transform.localPosition.y, bagButton.transform.localPosition.z);
        //Vector3 currPositionDragonHouse = new Vector3(dragonHouse.transform.localPosition.x, dragonHouse.transform.localPosition.y, dragonHouse.transform.localPosition.z);
        // dragonButton.transform.localPosition = bagButton.transform.localPosition = Vector3.one;

        //Tinh 1 buoc dem cua tung nut
        float stepDragonButton = -currPositionDragonButton.y / 10;
        float stepBagButton = -currPositionBagButton.y / 10;


        while (true)
        {
            if (dragonButton.transform.localPosition.y + stepDragonButton >= 0)
            {
                dragonButton.SetActive(false);
                bagButton.SetActive(false);
                dragonButton.transform.localPosition = currPositionDragonButton;
                bagButton.transform.localPosition = currPositionBagButton;
                yield break;
            }
            //Debug.Log(2);
            if (dragonButton.transform.localPosition.y + stepDragonButton > 0)
                dragonButton.transform.localPosition = Vector3.one;
            else
                dragonButton.transform.localPosition = new Vector3(dragonButton.transform.localPosition.x, dragonButton.transform.localPosition.y + stepDragonButton, dragonButton.transform.localPosition.z);
            if (bagButton.transform.localPosition.y + stepBagButton > 0)
                bagButton.transform.localPosition = Vector3.one;
            else
                bagButton.transform.localPosition = new Vector3(bagButton.transform.localPosition.x, bagButton.transform.localPosition.y + stepBagButton, bagButton.transform.localPosition.z);
            yield return new WaitForSeconds(time / 10);
        }

    }
    #endregion

    #region DEVICE - BACK, HOME (UPDATE)
    void Update()
	{
		//BACK
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(mapInfoController.gameObject.activeSelf)
				mapInfoController.gameObject.SetActive(false);
			else
				SceneManager.Instance.Load(SceneHashIDs.MENU);
		}
	}
	#endregion
}
