using UnityEngine;
using System.Collections;

public enum EHouseStateAction
{
    IDLE,
    BUILD,
    OPEN,
    CLOSE,
    DESTROY,
}

public class HouseController : MonoBehaviour
{
    public STowerID ID;
    public SHouseAttribute attribute;
    public int totalMoney { get; set; }

    [HideInInspector]
    public HouseAnimation houseAnimation;

    #region STATE MACHINE
    FiniteStateMachine<HouseController> FSM;

    public HouseStateBuild stateBuild;
    public HouseStateIdle stateIdle;
    public HouseStateOpen stateOpen;
    public HouseStateClose stateClose;
    public HouseStateDestroy stateDestroy;

    EHouseStateAction stateAction;
    public EHouseStateAction StateAction
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
                    case EHouseStateAction.BUILD:
                        changeState(stateBuild);
                        break;
                    case EHouseStateAction.IDLE:
                        changeState(stateIdle);
                        break;
                    case EHouseStateAction.OPEN:
                        changeState(stateOpen);
                        break;
                    case EHouseStateAction.CLOSE:
                        changeState(stateClose);
                        break;
                    case EHouseStateAction.DESTROY:
                        changeState(stateDestroy);
                        break;
                }
            }
        }
    }
    #endregion

    void Awake()
    {
        stateBuild = new HouseStateBuild();
        stateIdle = new HouseStateIdle();
        stateOpen = new HouseStateOpen();
        stateClose = new HouseStateClose();
        stateDestroy = new HouseStateDestroy();

        FSM = new FiniteStateMachine<HouseController>();
        houseAnimation = this.GetComponentInChildren<HouseAnimation>();
    }

    void Start()
    {
        FSM.Configure(this, stateBuild);
        StateAction = EHouseStateAction.BUILD;

        runResources();
    }

    void changeState(FSMState<HouseController> e)
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
        houseAnimation.changeResources(StateAction);
    }

    public void updateTotalMoney(int money)
    {
        totalMoney += money;
    }
}
