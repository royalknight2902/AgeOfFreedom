using UnityEngine;
using System.Collections;

public class SelectDragonController : Singleton<SelectDragonController> 
{
    public UISprite selected;
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

        selected.transform.position = dragons[index].transform.position;
        updateAttribute(PlayerInfo.Instance.dragonInfo.id);
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
}
