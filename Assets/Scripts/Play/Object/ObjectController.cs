using UnityEngine;
using System.Collections;

public enum EobjectID
{
    BLIZZARD,
}

public enum EObjectState
{
    RUN,
    DESTROY,
    EXPLOSION,
}

public class ObjectController : MonoBehaviour 
{
    public string ID { get; set; }

    [HideInInspector]
    public ObjectAnimation objectAnimation;

    #region STATE MACHINE
    FiniteStateMachine<ObjectController> FSM;

    [HideInInspector]
    private EObjectState stateAction;
    public EObjectState StateAction
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
                changeState(listState[stateAction]);
            }
        }
    }

    [HideInInspector]
    public System.Collections.Generic.Dictionary<EObjectState, ObjectState> listState = new System.Collections.Generic.Dictionary<EObjectState, ObjectState>();

    void changeState(FSMState<ObjectController> e)
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
        objectAnimation.changeResources(StateAction);
    }
    #endregion

    void Awake()
    {
        FSM = new FiniteStateMachine<ObjectController>();

        objectAnimation = this.transform.GetChild(0).GetComponent<ObjectAnimation>();
    }

    public void initalize(string id)
    {
        this.ID = id;

        ObjectGameData skillData = ReadDatabase.Instance.ObjectInfo[ID.ToUpper()];
        foreach (string key in skillData.States.Keys)
        {
            EObjectState state = (EObjectState)Extensions.GetEnum(EObjectState.RUN.GetType(), key.ToUpper());
            listState.Add(state, getClassObject(state));
        }

        //first element
        var enumerator = listState.Keys.GetEnumerator();
        enumerator.MoveNext();

        FSM.Configure(this, listState[enumerator.Current]);
        stateAction = enumerator.Current;

        runResources();
    }

    ObjectState getClassObject(EObjectState type)
    {
        ObjectState s = null;

        switch (type)
        {
            case EObjectState.RUN:
                s = new ObjectStateRun();
                break;
            case EObjectState.DESTROY:
                s = new ObjectStateDestroy();
                break;
            case EObjectState.EXPLOSION:
                s = new ObjectStateExplosion();
                break;
        }
        return s;
    }
}
