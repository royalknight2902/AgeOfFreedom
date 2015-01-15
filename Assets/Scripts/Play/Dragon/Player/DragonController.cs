using UnityEngine;
using System.Collections;

public enum EDragonBranch
{
    FIRE,
    ICE,
    IRON,
    PLANT,
    EARTH,
}

public enum EDragonStateAction
{
    IDLE,
    MOVE,
    ATTACK,
    DIE,
}

public enum EDragonStateDirection
{
    NONE,
    LEFT,
    RIGHT,
}

public enum EDragonStateOffense
{
    NONE,
    ATTACK,
    SKILL,
}

public class DragonController : MonoBehaviour
{
    public SDragonAttribute attribute;
    public GameObject selected;

    [HideInInspector]
    public DragonAnimation dragonAnimation;
    [HideInInspector]
    public DragonAttack dragonAttack;
    [HideInInspector]
    public UISlider sliderHP;

    public bool isSelected { get; set; }
    public bool isCopulate { get; set; }

    public int HP
    {
        set
        {
            attribute.HP.Current = value;

            if (attribute.HP.Current < 0)
                attribute.HP.Current = 0;

            sliderHP.value = attribute.HP.Current / (float)attribute.HP.Max;

            PlayDragonInfoController.Instance.labelHP.text = attribute.HP.Current + " / " + attribute.HP.Max;
            PlayDragonInfoController.Instance.sliderHP.value = attribute.HP.Current / (float)attribute.HP.Max;
        }
        get
        {
            return attribute.HP.Current;
        }
    }

    public int MP
    {
        set
        {
            attribute.MP.Current = value;

            if (attribute.MP.Current < 0)
                attribute.MP.Current = 0;

            PlayDragonInfoController.Instance.labelMP.text = attribute.MP.Current + " / " + attribute.MP.Max;
            PlayDragonInfoController.Instance.sliderMP.value = attribute.MP.Current / (float)attribute.MP.Max;
        }
        get
        {
            return attribute.MP.Current;
        }
    }

    #region STATE MACHINE
    FiniteStateMachine<DragonController> FSM;

    public DragonStateAttack stateAttack;
    public DragonStateDie stateDie;
    public DragonStateIdle stateIdle;
    public DragonStateMove stateMove;

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
                    case EDragonStateAction.MOVE:
                        changeState(stateMove);
                        break;
                    case EDragonStateAction.ATTACK:
                        changeState(stateAttack);
                        break;
                    case EDragonStateAction.DIE:
                        changeState(stateDie);
                        break;
                }
            }
        }
    }

    public EDragonStateDirection StateDirection { get; set; }
    public EDragonStateOffense StateOffense { get; set; }

    void changeState(FSMState<DragonController> e)
    {
        FSM.Change(e);
        runResources();
    }

    void Update()
    {
        FSM.Update();
    }

    void runResources()
    {
        dragonAnimation.changeResources(StateAction);
    }
    #endregion

    void Awake()
    {
        stateAttack = new DragonStateAttack();
        stateDie = new DragonStateDie();
        stateIdle = new DragonStateIdle();
        stateMove = new DragonStateMove();

        FSM = new FiniteStateMachine<DragonController>();

        dragonAnimation = this.transform.GetChild(0).GetComponent<DragonAnimation>();
        dragonAttack = this.GetComponentInChildren<DragonAttack>();

        StateAction = EDragonStateAction.IDLE;
        StateDirection = EDragonStateDirection.NONE;
        StateOffense = EDragonStateOffense.NONE;

        initalize();
    }

    void Start()
    {
        sliderHP = transform.GetChild(1).GetComponent<UISlider>();
        isSelected = false;
        isCopulate = false;

        HP = attribute.HP.Max;
        MP = attribute.MP.Max;

        FSM.Configure(this, stateIdle);

        runResources();
    }

    void initalize()
    {
        string branch = PlayerInfo.Instance.dragonInfo.id;
        DragonPlayerData data = ReadDatabase.Instance.DragonInfo.Player[branch];
        attribute.Name = data.Name;
        attribute.HP.Max = attribute.HP.Current = data.HP;
        attribute.MP.Max = attribute.MP.Current = data.MP;
        attribute.ATK.Min = data.ATK.Min;
        attribute.ATK.Max = data.ATK.Max;
        attribute.DEF = data.DEF;
        attribute.Speed = data.MoveSpeed;
    }

    public void updateTextHP()
    {
        PlayDragonInfoController.Instance.labelHP.text = attribute.HP.Current + " / " + attribute.HP.Max;
    }

    public void updateTextMP()
    {
        PlayDragonInfoController.Instance.labelMP.text = attribute.MP.Current + " / " + attribute.MP.Max;
    }
}
