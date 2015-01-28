using UnityEngine;
using System.Collections;

public class PlayDragonManager : Singleton<PlayDragonManager>
{
    public GameObject PlayerDragon { get; set; }
    public HouseController currentHouse { get; set; }
    public int maxBaby { get; set; }
    public int countBaby { get; set; }
    [HideInInspector]
    public System.Collections.Generic.List<GameObject> listBabyDragon = new System.Collections.Generic.List<GameObject>();

    DragonController dragonController;

    void Awake()
    {
        maxBaby = 0;
        countBaby = 0;
    }

    void OnEnable()
    {
        initDragon();
    }

    void Start()
    {
        initSkill();
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

        //Stretch selected arrow
        dragonController.selected.transform.GetChild(0).GetComponent<UIStretch>().container = PlayManager.Instance.tempInit.cameraRender;
    }

    void initSkill()
    {
        //Stretch skill selected
        float ratio = GameSupportor.getRatioAspect(PlayDragonInfoController.Instance.tempSkill, PlayDragonInfoController.Instance.renderUlti) * 100;
        PlayDragonInfoController.Instance.renderUlti.transform.localScale = new Vector3(ratio, ratio, ratio);

        bool hasUlti = false;
        string branch = PlayerInfo.Instance.dragonInfo.id;
        int length = PlayDragonInfoController.Instance.Skills.Length;
        int count = ReadDatabase.Instance.DragonInfo.Player[branch].Skills.Count - 1;

        if (ReadDatabase.Instance.DragonInfo.Player[branch].Skills.Count > 0)
        {
            for (int i = 0; i < length; i++)
            {
                PlayDragonInfoController.Instance.Skills[i].gameObject.SetActive(true);
            }

            foreach (DragonPlayerSkillData skillData in ReadDatabase.Instance.DragonInfo.Player[branch].Skills)
            {
                PlayDragonInfoSkillController skill = PlayDragonInfoController.Instance.Skills[count].GetComponent<PlayDragonInfoSkillController>();
                SkillData data = ReadDatabase.Instance.SkillInfo[skillData.ID.ToUpper()];
                skill.ID = skillData.ID;
                skill.CooldownTime = data.Cooldown;
                skill.Type = data.Type;
                skill.Ability = data.Ability;
                skill.ManaValue = data.Mana;

                skill.initalize();

                string path = "Image/Dragon/Player/" + ConvertSupportor.convertUpperFirstChar(branch) + "/Skill/" + skillData.ID;
                skill.sprite.mainTexture = Resources.Load<Texture>(path);
                count--;

                if (skillData.Ulti)
                {
                    PlayDragonInfoController.Instance.renderUlti.gameObject.SetActive(true);
                    UIAnchor anchor = PlayDragonInfoController.Instance.renderUlti.GetComponent<UIAnchor>();
                    anchor.container = skill.gameObject;
                    anchor.enabled = true;
                    hasUlti = true;
                }
            }
        }

        if (hasUlti == false && PlayDragonInfoController.Instance.renderUlti.gameObject.activeSelf)
            PlayDragonInfoController.Instance.renderUlti.gameObject.SetActive(false);

        count = ReadDatabase.Instance.DragonInfo.Player[branch].Skills.Count;
        if (count < length)
        {
            for (int i = count; i < length; i++)
            {
                PlayDragonInfoController.Instance.Skills[i].gameObject.SetActive(false);
            }
        }
    }

    public void moveToHouse()
    {
        if (countBaby >= maxBaby && maxBaby != 0)
        {
            DeviceService.Instance.openToast("You have reached maximum of baby dragon!");
            PlayManager.Instance.resetUpgrade();
            return;
        }

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

        StartCoroutine(copulateChild(action, 1));
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
        BabyDragonController babyController = babyDragon.GetComponent<BabyDragonController>();
        babyController.index = countBaby;

        #region Scale base on parent scale
        babyDragon.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        BoxCollider boxCollider = babyDragon.GetComponent<BoxCollider>();
        boxCollider.size = new Vector2(boxCollider.size.x / 2, boxCollider.size.y / 2);

        //foreach (Transform child in babyDragon.transform)
        //{
        //    if(child.name.Equals("Collider For Enemy ATK"))
        //    {
        //        SphereCollider collider = child.GetComponent<SphereCollider>();
        //        collider.radius = collider.radius * 0.5f;
        //    }
        //    if(child.name.Equals("ATK Range"))
        //    {
        //        SphereCollider collider = child.GetComponent<SphereCollider>();
        //        collider.radius = collider.radius * 0.5f;
        //    }
        //}

        #endregion

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

        if (babyController.StateDirection == EDragonStateDirection.LEFT)
        {
            Vector3 scale = babyDragon.transform.GetChild(0).localScale;
            babyDragon.transform.GetChild(0).localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
        }
        else
        {
            Vector3 scale = babyDragon.transform.GetChild(0).localScale;
            babyDragon.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }

        //Stretch HP
        babyDragon.transform.GetChild(1).GetComponent<UIStretch>().container = GameObject.FindWithTag("Root");

        babyController.dragonParent = dragonController;
        listBabyDragon.Add(babyDragon);
        countBaby = listBabyDragon.Count;

    }
    #endregion

    #region SKILL
    public void initSkill(string id, int mana, ESkillType type, object ability, object[] data)
    {
        if (dragonController.attribute.MP.Current < mana)
        {
            DeviceService.Instance.openToast("Not enough mana!");
            return;
        }
        else
        {
            dragonController.attribute.MP.Current -= mana;

            if (dragonController.attribute.MP.Current < 0)
                dragonController.attribute.MP.Current = 0;

            float valueTo = dragonController.attribute.MP.Current / (float)dragonController.attribute.MP.Max;
            dragonController.updateTextMP();
            EffectSupportor.Instance.runSliderValue(PlayDragonInfoController.Instance.sliderMP, valueTo, EffectSupportor.TimeValueRunMP);
        }

        GameObject skill = Instantiate(Resources.Load<GameObject>("Prefab/Skill/Skill")) as GameObject;

        if (type == ESkillType.GLOBAL)
        {
            skill.transform.parent = Camera.main.transform;
            skill.transform.GetChild(0).gameObject.layer = 5;
            skill.transform.position = Vector3.zero;
        }
        else //target
        {
            skill.transform.parent = PlayManager.Instance.Temp.Skill.transform;
        }

        skill.transform.localScale = Vector3.one;

        SkillController skillController = skill.GetComponent<SkillController>();
        skillController.Owner = PlayerDragon;
        skillController.initalize(id, type, ability, data);
    }
    #endregion

    void Update()
    {
        if (dragonController != null)
        {
            if (dragonController.gameObject.activeSelf)
            {
                if (dragonController.StateAction == EDragonStateAction.MOVE)
                {
                    for (int i = 0; i < listBabyDragon.Count; i++)
                    {
                        BabyDragonController babyDragonController = listBabyDragon[i].GetComponent<BabyDragonController>();
                        if (babyDragonController.StateAction != EDragonStateAction.MOVE)
                        {
                            try
                            {
                                Vector3 handleException = dragonController.stateMove.listPosition[PlayConfig.BabyDragonIndexForListStart
                                    - PlayConfig.BabyDragonIndexForListDistance * babyDragonController.index]; // for test exception
                                babyDragonController.stateAttack.target = null;
                                babyDragonController.StateAction = EDragonStateAction.MOVE;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }
    }

    public void showDragonAttackCollision(Vector3 enemyPosition)
    {
        GameObject collision = Instantiate(Resources.Load<GameObject>("Prefab/Collision/Collision 6")) as GameObject;
        collision.transform.parent = PlayManager.Instance.Temp.Collision.transform;
        collision.transform.localScale = Vector3.one;
        collision.transform.position = enemyPosition;
        collision.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueCollision;
    }

    public void showEnemyAttackCollision(GameObject dragon)
    {
        GameObject collision = Instantiate(Resources.Load<GameObject>("Prefab/Collision/Collision 6")) as GameObject;
        collision.transform.parent = PlayManager.Instance.Temp.Collision.transform;
        collision.transform.localScale = Vector3.one;
        collision.transform.position = dragon.transform.position;
        collision.GetComponentInChildren<SpriteRenderer>().material.renderQueue = dragon.GetComponentInChildren<SpriteRenderer>().material.renderQueue + 1;
    }


}

