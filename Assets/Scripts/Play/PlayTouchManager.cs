using UnityEngine;
using System.Collections;

public class PlayTouchManager : Singleton<PlayTouchManager>
{
    [HideInInspector]
    public System.Collections.Generic.List<GameObject> frags = new System.Collections.Generic.List<GameObject>();
    [HideInInspector]
    public GameObject skillTarget;

    public ESkillOffense currentOffense { get; set; }

    DragonController dragonController;
    Camera cameraRender;

    void Start()
    {
        currentOffense = ESkillOffense.AOE;
        dragonController = PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>();
        cameraRender = PlayManager.Instance.tempInit.cameraRender.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 &&
                                           Input.touches[0].phase == TouchPhase.Began))
        {
            if (dragonController.HP <= 0 || dragonController.isCopulate)
                return;

            if (dragonController.isSelected)
            {
                if (!UICamera.hoveredObject.name.Equals("Drag Camera"))
                    return;

                if (PlayDragonManager.Instance.currentHouse != null)
                {
                    PlayDragonManager.Instance.currentHouse.GetComponent<HouseController>().StateAction = EHouseStateAction.CLOSE;
                    PlayDragonManager.Instance.currentHouse = null;
                }

                Vector3 touchPos = cameraRender.ScreenToWorldPoint(Input.mousePosition);

                GameObject f = Instantiate(PlayManager.Instance.modelPlay.Flag) as GameObject;
                f.transform.parent = PlayManager.Instance.Temp.Flag.transform;
                f.transform.localScale = Vector3.one;
                f.transform.position = touchPos;

                //set render queue
                f.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueFlag;
                //stretch
                f.transform.GetChild(0).GetComponent<UIStretch>().container = PlayManager.Instance.tempInit.uiRoot;

                dragonController.StateAction = EDragonStateAction.MOVE;
                dragonController.stateMove.destPosition = touchPos;
                dragonController.stateMove.destFrag = f;
                dragonController.stateMove.Movement = EDragonMovement.MOVE_TOUCH;
                dragonController.StateOffense = EDragonStateOffense.NONE;
                dragonController.isSelected = false;
                EffectSupportor.Instance.fadeOutWithEvent(dragonController.selected.transform.GetChild(0).gameObject, ESpriteType.UI_SPRITE, 0.1f, new EventDelegate(unenableSelectedSprite));

                if (dragonController.stateMove.preFrag == null)
                    dragonController.stateMove.preFrag = f;

                //add to list
                frags.Add(f);

                if (touchPos.x < dragonController.transform.position.x)
                {
                    dragonController.StateDirection = EDragonStateDirection.LEFT;
                }
                else if (touchPos.x > dragonController.transform.position.x)
                {
                    dragonController.StateDirection = EDragonStateDirection.RIGHT;
                }
            }
            else
            {
                if (skillTarget != null)
                {
                    PlayDragonInfoSkillController infoController = skillTarget.GetComponent<PlayDragonInfoSkillController>();

                    if (infoController.Type == ESkillType.TARGET)
                    {
                        if ((ESkillOffense)infoController.Ability == ESkillOffense.SINGLE)
                        {
                            if (UICamera.hoveredObject.tag.Equals(TagHashIDs.Enemy))
                            {
                                if (PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>().attribute.MP.Current >= infoController.ManaValue)
                                {
                                    infoController.cooldown.gameObject.SetActive(true);
                                    infoController.cooldown.fillAmount = 1.0f;
                                    infoController.isEnable = false;
                                    infoController.StartCoroutine(infoController.runCooldown());
                                }

                                //invi selected
                                infoController.selected.GetComponent<TweenScale>().PlayReverse();
                                infoController.selected.GetComponent<TweenAlpha>().PlayReverse();
                                infoController.StartCoroutine(infoController.invisibleSelectedSkill());

                                infoController.typeSprite.GetComponent<TweenPosition>().PlayReverse();
                                infoController.typeSprite.GetComponent<TweenAlpha>().PlayReverse();

                                Vector3 touchPos = cameraRender.ScreenToWorldPoint(Input.mousePosition);
                                PlayDragonManager.Instance.initSkill(infoController.ID, infoController.ManaValue,
                                    infoController.Type, ESkillOffense.SINGLE, new object[] { UICamera.hoveredObject.gameObject });
                                setCurrentOffenseType(ESkillOffense.AOE);
                            }
                        }
                        else
                        {
                            if (PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>().attribute.MP.Current >= infoController.ManaValue)
                            {
                                infoController.cooldown.gameObject.SetActive(true);
                                infoController.cooldown.fillAmount = 1.0f;
                                infoController.isEnable = false;
                                infoController.StartCoroutine(infoController.runCooldown());
                            }

                            //invi selected
                            infoController.selected.GetComponent<TweenScale>().PlayReverse();
                            infoController.selected.GetComponent<TweenAlpha>().PlayReverse();
                            infoController.StartCoroutine(infoController.invisibleSelectedSkill());

                            infoController.typeSprite.GetComponent<TweenPosition>().PlayReverse();
                            infoController.typeSprite.GetComponent<TweenAlpha>().PlayReverse();

                            Vector3 touchPos = cameraRender.ScreenToWorldPoint(Input.mousePosition);
                            PlayDragonManager.Instance.initSkill(infoController.ID, infoController.ManaValue,
                                infoController.Type, ESkillOffense.AOE, new object[] { touchPos });
                            setCurrentOffenseType(ESkillOffense.AOE);
                        }
                    }
                }
            }
        }
    }

    public void checkFrag(GameObject f, bool isFinish)
    {
        if (isFinish)
        {
            for (int i = 0; i < frags.Count; i++)
            {
                GameObject frag = frags[i];
                if (frag == f)
                {
                    StartCoroutine(fadeToDestroy(f));
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < frags.Count; i++)
            {
                GameObject frag = frags[i];
                if (frag != f)
                {
                    StartCoroutine(fadeToDestroy(frag));
                }
            }
        }
    }

    IEnumerator fadeToDestroy(GameObject f)
    {
        UISprite sprite = f.transform.GetChild(0).GetComponent<UISprite>();
        SpriteRenderer render = sprite.transform.GetChild(0).GetComponent<SpriteRenderer>();

        float ValueEachFrame = (float)Time.deltaTime / 0.4f;
        while (true)
        {
            if (f == null)
                yield break;

            //Animation render
            Color color = render.color;

            if (color.a <= 0)
            {
                frags.Remove(f);
                Destroy(f);
                yield break;
            }
            else
                render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

            //Frag sprite
            color = sprite.color;

            if (color.a <= 0)
            {
                frags.Remove(f);
                Destroy(f);
                yield break;
            }
            else
                sprite.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

            yield return 0;
        }
    }

    void unenableSelectedSprite()
    {
        UISprite sprite = dragonController.selected.transform.GetChild(0).GetComponent<UISprite>();
        Color color = sprite.color;
        sprite.color = new Color(color.r, color.g, color.b, 1);
        dragonController.selected.SetActive(false);
    }

    public void setCurrentOffenseType(ESkillOffense offense)
    {
        if (offense == ESkillOffense.SINGLE)
        {
            if (currentOffense != ESkillOffense.SINGLE)
            {
                currentOffense = ESkillOffense.SINGLE;
                setDepthEnemy();
            }
        }
        else //AOE
        {
            if (currentOffense != ESkillOffense.AOE)
            {
                currentOffense = ESkillOffense.AOE;
                setDepthEnemy();
            }
        }
    }

    void setDepthEnemy()
    {
        for (int i = 0; i < WaveController.Instance.enemyStartPos.Length; i++)
        {
            foreach (Transform child in WaveController.Instance.enemyStartPos[i].transform)
            {
                if (currentOffense == ESkillOffense.SINGLE)
                    child.GetComponent<UIWidget>().depth = 1;
                else
                    child.GetComponent<UIWidget>().depth = -1;
            }
        }
    }
}

