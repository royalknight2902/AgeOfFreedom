using UnityEngine;
using System.Collections;

public class PlayDragonInfoSkillController : MonoBehaviour
{
    public UITexture sprite;
    public UISprite cooldown;
    public UILabel mana;
    public UISprite typeSprite;
    public UILabel typeLabel;
    public GameObject selected;

    public string ID { get; set; }
    public float CooldownTime { get; set; }
    public int ManaValue { get; set; }
    public ESkillType Type { get; set; }
    public object Ability { get; set; }
    public bool isEnable { get; set; }

    public bool isTap { get; set; }

    void Start()
    {
        isEnable = false;
        isTap = false;
    }

    public void initalize()
    {
        mana.text = ManaValue.ToString();

        object[] result = PlayConfig.getColorSkillType(Type);
        typeLabel.text = result[0].ToString();
        typeSprite.color = (Color)result[1];

        StartCoroutine(runCooldown());
    }

    public IEnumerator runCooldown()
    {
        float valueEachTime = Time.fixedDeltaTime / (CooldownTime / PlayerInfo.Instance.userInfo.timeScale);

        while (true)
        {
            if (Time.timeScale != 0.0f)
            {
                cooldown.fillAmount -= valueEachTime;
                if (cooldown.fillAmount <= 0.0f)
                {
                    cooldown.gameObject.SetActive(false);
                    isEnable = true;
                    yield break;
                }
            }
            yield return 0;
        }
    }

    public IEnumerator invisibleSelectedSkill()
    {
        yield return new WaitForSeconds(selected.GetComponent<TweenAlpha>().duration);
        selected.SetActive(false);
        PlayTouchManager.Instance.skillTarget = null;
        yield return 0;
    }

    void OnClick()
    {
        if (isEnable)
        {
            if (Type == ESkillType.TARGET)
            {
                if (PlayTouchManager.Instance.skillTarget != null && PlayTouchManager.Instance.skillTarget != this.gameObject)
                {
                    PlayDragonInfoSkillController temp = PlayTouchManager.Instance.skillTarget.GetComponent<PlayDragonInfoSkillController>();
                    temp.selected.GetComponent<TweenScale>().PlayReverse();
                    temp.selected.GetComponent<TweenAlpha>().PlayReverse();

                    temp.typeSprite.GetComponent<TweenPosition>().PlayReverse();
                    temp.typeSprite.GetComponent<TweenAlpha>().PlayReverse();
                    temp.isTap = false;

                    PlayTouchManager.Instance.skillTarget = null;
                    PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.AOE);
                }

                if (isTap)
                {
                    selected.GetComponent<TweenScale>().PlayReverse();
                    selected.GetComponent<TweenAlpha>().PlayReverse();

                    typeSprite.GetComponent<TweenPosition>().PlayReverse();
                    typeSprite.GetComponent<TweenAlpha>().PlayReverse();

                    isTap = false;
                    PlayTouchManager.Instance.skillTarget = null;
                    PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.AOE);
                }
                else
                {
                    isTap = true;

                    //--show selected sprite
                    selected.SetActive(true);
                    //-Anchor
                    UIAnchor anchor = selected.GetComponent<UIAnchor>();
                    anchor.container = this.gameObject;
                    anchor.enabled = true;
                    //-Tween
                    selected.GetComponent<TweenScale>().PlayForward();
                    selected.GetComponent<TweenAlpha>().PlayForward();

                    //--show type sprite
                    typeSprite.gameObject.SetActive(true);
                    typeSprite.GetComponent<TweenPosition>().PlayForward();
                    typeSprite.GetComponent<TweenAlpha>().PlayForward();

                    PlayTouchManager.Instance.skillTarget = this.gameObject;

                    if ((ESkillOffense)Ability == ESkillOffense.SINGLE)
                        PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.SINGLE);
                    else
                        PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.AOE);
                }
            }
            else // BUFF & GLOBAL
            {
                if (PlayTouchManager.Instance.skillTarget != null)
                {
                    if (PlayTouchManager.Instance.skillTarget != this.gameObject)
                    {
                        PlayDragonInfoSkillController temp = PlayTouchManager.Instance.skillTarget.GetComponent<PlayDragonInfoSkillController>();
                        temp.selected.GetComponent<TweenScale>().PlayReverse();
                        temp.selected.GetComponent<TweenAlpha>().PlayReverse();

                        temp.typeSprite.GetComponent<TweenPosition>().PlayReverse();
                        temp.typeSprite.GetComponent<TweenAlpha>().PlayReverse();
                        temp.isTap = false;

                        PlayTouchManager.Instance.skillTarget = null;
                        PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.AOE);
                    }
                }

                if (isTap)
                {
                    selected.GetComponent<TweenScale>().PlayReverse();
                    selected.GetComponent<TweenAlpha>().PlayReverse();

                    typeSprite.GetComponent<TweenPosition>().PlayReverse();
                    typeSprite.GetComponent<TweenAlpha>().PlayReverse();

                    if (PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>().attribute.MP.Current >= ManaValue)
                    {
                        cooldown.gameObject.SetActive(true);
                        cooldown.fillAmount = 1.0f;
                        isTap = false;
                        isEnable = false;
                        StartCoroutine(runCooldown());
                    }
                    PlayDragonManager.Instance.initSkill(ID, ManaValue, Type, Ability, null);
                }
                else
                {
                    isTap = true;

                    //--show selected sprite
                    selected.SetActive(true);
                    //-Anchor
                    UIAnchor anchor = selected.GetComponent<UIAnchor>();
                    anchor.container = this.gameObject;
                    anchor.enabled = true;
                    //-Tween
                    selected.GetComponent<TweenScale>().PlayForward();
                    selected.GetComponent<TweenAlpha>().PlayForward();

                    //--show type sprite
                    typeSprite.gameObject.SetActive(true);
                    typeSprite.GetComponent<TweenPosition>().PlayForward();
                    typeSprite.GetComponent<TweenAlpha>().PlayForward();

                    PlayTouchManager.Instance.skillTarget = this.gameObject;
                    PlayTouchManager.Instance.setCurrentOffenseType(ESkillOffense.AOE);
                }
            }
        }
    }
}
