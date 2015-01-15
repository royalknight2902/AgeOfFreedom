using UnityEngine;
using System.Collections;

public class SelectDragonController : Singleton<SelectDragonController> 
{
    public UISprite selected;
    public SpriteRenderer renderUlti;
    public UIWidget tempSkill;
    public LevelSelectDragonAnimation dragonAnimation;
    public SSelectDragonAttribute attribute;
    public SSelectDragonContainer[] dragons;

    #region STATE MACHINE
    FiniteStateMachine<SelectDragonController> FSM;

    public SelectDragonStateIdle stateIdle;

    EDragonStateAction stateAction;
    public EDragonStateAction StateAction
    {
        get
        {
            return stateAction;
        }
        set
        {
            if (stateAction != value)
            {
                stateAction = value;
                switch (stateAction)
                {
                    case EDragonStateAction.IDLE:
                        changeState(stateIdle);
                        break;
                }
            }
        }
    }

    void changeState(FSMState<SelectDragonController> e)
    {
        FSM.Change(e);
        runResources();
    }

    void Update()
    {
        FSM.Update();
    }

    public void runResources()
    {
        dragonAnimation.changeResources(StateAction);
    }
    #endregion

    void Awake()
    {
        stateIdle = new SelectDragonStateIdle();

        FSM = new FiniteStateMachine<SelectDragonController>();

        StateAction = EDragonStateAction.IDLE;
    }

    void Start()
    {
        runResources();
    }

    void OnEnable()
    {
        FSM.Configure(this, stateIdle);
        initalize();
    }

    void initalize()
    {
        int i = 0;
        int index = -1;
        foreach (System.Collections.Generic.KeyValuePair<string, DragonPlayerData> iterator in ReadDatabase.Instance.DragonInfo.Player)
        {
            SSelectDragonContainer container = dragons[i];
            container.IDBranch = iterator.Key;
            container.Branch.spriteName = "icon-branch-" + iterator.Key.ToLower();
            container.Icon.mainTexture = Resources.Load<Texture>("Image/Dragon/Icon/dragon-" + iterator.Key.ToLower());

            if (PlayerInfo.Instance.dragonInfo.id.Equals(iterator.Key))
                index = i;

            i++;
        }

        //Stretch skill selected
        float ratio = GameSupportor.getRatioAspect(tempSkill.gameObject, renderUlti) * 100;
        renderUlti.transform.localScale = new Vector3(ratio, ratio, ratio);

        selected.transform.position = dragons[index].transform.position;
        updateAttribute(PlayerInfo.Instance.dragonInfo.id);
        updateSkill(PlayerInfo.Instance.dragonInfo.id);
    }

    public void updateAttribute(string branch)
    {
        DragonPlayerData data = ReadDatabase.Instance.DragonInfo.Player[branch];
        attribute.Name.text = data.Name;
        attribute.Branch.spriteName = "icon-branch-" + branch.ToString().ToLower();
        attribute.HP.text = data.HP.ToString();
        attribute.MP.text = data.MP.ToString();
        attribute.ATK.text = data.ATK.Min + " - " + data.ATK.Max;
        attribute.DEF.text = data.DEF.ToString();
        attribute.ATKSpeed.text = data.ATKSpeed.ToString();
        attribute.MoveSpeed.text = data.MoveSpeed.ToString();
    }

    public void updateSkill(string branch)
    {
        int count = 0;
        bool hasUlti = false;
        int length = attribute.Skills.Length;

        if (ReadDatabase.Instance.DragonInfo.Player[branch].Skills.Count > 0)
        {
            for (int i = 0; i < length; i++)
            {
                attribute.Skills[i].gameObject.SetActive(true);
            }

            foreach (DragonPlayerSkillData skillData in ReadDatabase.Instance.DragonInfo.Player[branch].Skills)
            {
                UITexture texture = attribute.Skills[count].GetComponent<UITexture>();
                string path = "Image/Dragon/Player/" + ConvertSupportor.convertUpperFirstChar(branch) + "/Skill/" + skillData.ID;
                texture.mainTexture = Resources.Load<Texture>(path);
                count++;

                if (skillData.Ulti)
                {
                    renderUlti.gameObject.SetActive(true);
                    UIAnchor anchor = renderUlti.GetComponent<UIAnchor>();
                    anchor.container = texture.gameObject;
                    anchor.enabled = true;
                    hasUlti = true;
                }
            }
        }


        if (hasUlti == false && renderUlti.gameObject.activeSelf)
            renderUlti.gameObject.SetActive(false);

        if (count < length)
        {
            for (int i = count; i < length; i++)
            {
                attribute.Skills[i].gameObject.SetActive(false);
            }
        }
    }
}
