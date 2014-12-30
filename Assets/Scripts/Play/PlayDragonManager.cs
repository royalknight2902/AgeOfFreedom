using UnityEngine;
using System.Collections;

public class PlayDragonManager : Singleton<PlayDragonManager>
{
    public GameObject PlayerDragon { get; set; }
    public HouseController currentHouse { get; set; }
    public int countBaby { get; set; }

    DragonController dragonController;
    System.Collections.Generic.List<GameObject> listBabyDragon = new System.Collections.Generic.List<GameObject>();

    void Awake()
    {
        countBaby = 0;
    }

    void OnEnable()
    {
        initDragon();
    }

    #region DRAGON PLAYER
    void initDragon()
    {
        PlayerDragon = Instantiate(Resources.Load<GameObject>("Prefab/Dragon/Player/Dragon")) as GameObject;
        PlayerDragon.transform.parent = PlayManager.Instance.Temp.Dragon.transform;
        PlayerDragon.transform.localPosition = Vector3.zero;
        PlayerDragon.transform.localScale = Vector3.one;

        //Stretch HP
        PlayerDragon.transform.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("Root");

        dragonController = PlayerDragon.GetComponent<DragonController>();
    }

    public void moveToHouse()
    {
        //Dragon state move to house
        dragonController.StateAction = EDragonStateAction.MOVE;
        dragonController.stateMove.destPosition = PlayManager.Instance.objectUpgrade.Tower.transform.position;
        dragonController.stateMove.Movement = EDragonMovement.MOVE_TO_HOUSE;

        if (dragonController.stateMove.destFrag != null)
            dragonController.stateMove.destFrag = null;

        if (dragonController.stateMove.destPosition.x < dragonController.transform.position.x && dragonController.StateDirection == EDragonStateDirection.RIGHT)
        {
            dragonController.StateDirection = EDragonStateDirection.LEFT;
        }
        else if (dragonController.stateMove.destPosition.x > dragonController.transform.position.x && dragonController.StateDirection == EDragonStateDirection.LEFT)
        {
            dragonController.StateDirection = EDragonStateDirection.RIGHT;
        }

        //House state open to wait dragon come in
        currentHouse = PlayManager.Instance.objectUpgrade.Tower.GetComponent<HouseController>();
        currentHouse.StateAction = EHouseStateAction.OPEN;
    }

    public void copulateIn()
    {
        PlayerDragon.SetActive(false);

        currentHouse.StateAction = EHouseStateAction.CLOSE;
        dragonController.isCopulate = true;
        HouseAction action = currentHouse.GetComponent<HouseAction>();

        action.countdown.SetActive(true);
        action.countdownForeground.fillAmount = 1;
        EffectSupportor.Instance.fadeInAndDestroy(action.countdown, ESpriteType.UI_SPRITE, 0.8f);

        StartCoroutine(copulateChild(action, 5));
    }

    public void copulateOut()
    {
        currentHouse.StateAction = EHouseStateAction.OPEN;
        EffectSupportor.Instance.fadeOutAndDestroy(currentHouse.GetComponent<HouseAction>().countdown, ESpriteType.UI_SPRITE, 0.8f);

        currentHouse.houseAnimation.addEventLastKeyForCurrentState(new EventDelegate(dragonComeOut), true);
        currentHouse.houseAnimation.addEventLastKeyForCurrentState(new EventDelegate(initBabyDragon), true);
        currentHouse.houseAnimation.addEventLastKeyForCurrentState(new EventDelegate(currentHouse.houseAnimation.changeStateClose), true);
    }

    void dragonComeOut()
    {
        PlayerDragon.SetActive(true);
        dragonController.isCopulate = false;
    }

    IEnumerator copulateChild(HouseAction houseAction, float time)
    {
        houseAction.countdownLabel.text = time.ToString();

        float valueEachFrame = Time.fixedDeltaTime / (time / PlayerInfo.Instance.userInfo.timeScale);
        float elapsedTime = 0.0f;
        float currentTime = time;

        while (true)
        {
            elapsedTime += Time.fixedDeltaTime;
            houseAction.countdownForeground.fillAmount -= valueEachFrame;

            if (elapsedTime >= 1.0f)
            {
                currentTime--;
                elapsedTime = 0.0f;

                houseAction.countdownLabel.text = currentTime.ToString();
            }

            if (houseAction.countdownForeground.fillAmount <= 0.0f && currentTime <= 0)
            {
                copulateOut();
                yield break;
            }

            yield return 0;
        }
    }
    #endregion

    #region BABY DRAGON
    void initBabyDragon()
    {
        GameObject babyDragon = Instantiate(Resources.Load<GameObject>("Prefab/Dragon/Baby/Baby Dragon")) as GameObject;
        babyDragon.transform.parent = PlayManager.Instance.Temp.Dragon.transform;
        babyDragon.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        BabyDragonController babyController = babyDragon.GetComponent<BabyDragonController>();
        babyController.index = countBaby;

        if (dragonController.StateDirection == EDragonStateDirection.LEFT)
        {
            babyDragon.transform.position = new Vector3(PlayerDragon.transform.position.x + 0.25f, PlayerDragon.transform.position.y,
                PlayerDragon.transform.position.z);
            babyController.StateDirection = EDragonStateDirection.LEFT;
        }
        else
        {
            babyDragon.transform.position = new Vector3(PlayerDragon.transform.position.x - 0.25f, PlayerDragon.transform.position.y,
                PlayerDragon.transform.position.z);
            babyController.StateDirection = EDragonStateDirection.RIGHT;
        }

        babyController.dragonParent = dragonController;

        //Stretch HP
        babyDragon.transform.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("Root");

        listBabyDragon.Add(babyDragon);
        countBaby = listBabyDragon.Count;

    }
    #endregion

    void Update()
    {
        if (dragonController.StateAction == EDragonStateAction.MOVE)
        {
            foreach (GameObject baby in listBabyDragon)
            {
                try
                {
                    BabyDragonController babyDragonController = baby.GetComponent<BabyDragonController>();
                    Vector3 handleException = dragonController.stateMove.listPosition[PlayConfig.BabyDragonIndexForListStart 
                        + PlayConfig.BabyDragonIndexForListDistance * babyDragonController.index]; // for test exception
                    babyDragonController.StateAction = EDragonStateAction.MOVE;
                }
                catch
                {
                    //baby.GetComponent<BabyDragonController>().StateAction = EDragonStateAction.IDLE;
                }
            }
        }
    }
}

