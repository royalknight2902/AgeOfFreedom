using UnityEngine;
using System.Collections;

public class BabyDragonController : MonoBehaviour
{
    public SBabyDragonAttribute attribute;

    public DragonController dragonParent { get; set; }
    public BabyDragonAnimation babyAnimation { get; set; }
    public BabyDragonAttack babyAttack { get; set; }
    public int index { get; set; }

    [HideInInspector]
    public UISlider sliderHP;

    #region STATE MACHINE
    FiniteStateMachine<BabyDragonController> FSM;

    public BabyDragonStateAttack stateAttack;
    public BabyDragonStateDie stateDie;
    public BabyDragonStateIdle stateIdle;
    public BabyDragonStateMove stateMove;

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
    #endregion

    void Awake()
    {
        stateAttack = new BabyDragonStateAttack();
        stateDie = new BabyDragonStateDie();
        stateIdle = new BabyDragonStateIdle();
        stateMove = new BabyDragonStateMove();

        FSM = new FiniteStateMachine<BabyDragonController>();

        babyAnimation = this.transform.GetChild(0).GetComponent<BabyDragonAnimation>();
        babyAttack = this.GetComponentInChildren<BabyDragonAttack>();

        StateAction = EDragonStateAction.IDLE;
        StateDirection = EDragonStateDirection.LEFT;

        attribute.HP.Current = attribute.HP.Max;
    }

    void Start()
    {
        sliderHP = transform.GetChild(1).GetComponent<UISlider>();

        FSM.Configure(this, stateIdle);

        runResources();
    }

    void changeState(FSMState<BabyDragonController> e)
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
        babyAnimation.changeResources(StateAction);
    }

    public void updateTextHP()
    {
        PlayDragonInfoController.Instance.labelHP.text = attribute.HP.Current + " / " + attribute.HP.Max;
    }
}
