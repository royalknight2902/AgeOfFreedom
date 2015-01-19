using UnityEngine;
using System.Collections;

public enum ESkillAction
{
    TRAP,
    FADE,
    DROP,
    END,
    BUFF,
    ARMAGGEDDON,
}

public enum ESkillType
{
    TARGET,
    BUFF,
    GLOBAL,
}

public enum ESkillOffense
{
    TARGET,
    AOE,
}

public enum ESkillBuff
{
    PLAYER,
    ENEMY,
}

public class SkillController : MonoBehaviour
{
    public string ID { get; set; }
    public GameObject Owner { get; set; }

    [HideInInspector]
    public SkillAnimation skillAnimation;

    #region STATE MACHINE
    FiniteStateMachine<SkillController> FSM;

    [HideInInspector]
    public System.Collections.Generic.Dictionary<ESkillAction, SkillState> listState = new System.Collections.Generic.Dictionary<ESkillAction, SkillState>();

    private ESkillAction stateAction;
    public ESkillAction StateAction
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

    void changeState(FSMState<SkillController> e)
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
        skillAnimation.changeResources(StateAction);
    }
    #endregion

    void Awake()
    {
        FSM = new FiniteStateMachine<SkillController>();
        skillAnimation = this.transform.GetChild(0).GetComponent<SkillAnimation>();
    }

    public void initalize(string ID, params object[] data)
    {
        this.ID = ID;
        this.name = ID;

        SkillData skillData = ReadDatabase.Instance.SkillInfo[ID.ToUpper()];
        foreach (string key in skillData.States.Keys)
        {
            ESkillAction state = (ESkillAction)Extensions.GetEnum(ESkillAction.DROP.GetType(), key.ToUpper());
            listState.Add(state, getClassSkill(state));
        }

        //first element
        var enumerator = listState.Keys.GetEnumerator();
        enumerator.MoveNext();

        //set Property for state
        setPropertyState(data);
        //set Property from database
        setPropertyFromDatabase();

        FSM.Configure(this, listState[enumerator.Current]);
        stateAction = enumerator.Current;

        runResources();
    }

    void setPropertyState(params object[] data) // for destination position
    {
        if (data.Length <= 0)
            return;

        if (data[0] is ESkillType)
        {
            if ((ESkillType)data[0] == ESkillType.TARGET)
            {
                foreach (System.Collections.Generic.KeyValuePair<ESkillAction, SkillState> iterator in listState)
                {
                    switch (iterator.Key)
                    {
                        case ESkillAction.DROP:
                            SkillStateDrop stateDrop = iterator.Value as SkillStateDrop;
                            stateDrop.destPosition = (Vector3)data[1];
                            break;
                        case ESkillAction.TRAP:
                            SkillStateTrap stateTrap = iterator.Value as SkillStateTrap;
                            stateTrap.position = (Vector3)data[1];
                            break;
                    }
                }
            }
        }
    }

    void setPropertyFromDatabase()
    {
        foreach (System.Collections.Generic.KeyValuePair<ESkillAction, SkillState> state in listState)
        {
            foreach (System.Collections.Generic.KeyValuePair<string, object> iterator in
                ReadDatabase.Instance.SkillInfo[ID.ToUpper()].States[state.Key.ToString()].Values)
            {
                switch (state.Key)
                {
                    case ESkillAction.TRAP:
                        SkillStateTrap trap = state.Value as SkillStateTrap;

                        switch (iterator.Key.ToUpper())
                        {
                            case "DURATION":
                                trap.duration = float.Parse(iterator.Value.ToString());
                                break;
                        }
                        break;
                    case ESkillAction.BUFF:
                        SkillStateBuff buff = state.Value as SkillStateBuff;

                        switch (iterator.Key.ToUpper())
                        {
                            case "DURATION":
                                buff.duration = float.Parse(iterator.Value.ToString());
                                break;
                            case "TYPE":
                                buff.type = (ESkillStateBuffType)Extensions.GetEnum(ESkillStateBuffType.ROTATION.GetType(), iterator.Value.ToString().ToUpper());
                                break;
                            case "VALUE":
                                buff.value = iterator.Value;
                                break;
                        }
                        break;
                    case ESkillAction.ARMAGGEDDON:
                        SkillStateArmaggeddon armaggeddon = state.Value as SkillStateArmaggeddon;

                        switch(iterator.Key.ToUpper())
                        {
                            case "DURATION":
                                armaggeddon.duration = float.Parse(iterator.Value.ToString());
                                break;
                        }
                        break;
                }
            }
        }
    }

    SkillState getClassSkill(ESkillAction type)
    {
        SkillState s = null;

        switch (type)
        {
            case ESkillAction.DROP:
                s = new SkillStateDrop();
                break;
            case ESkillAction.END:
                s = new SkillStateEnd();
                break;
            case ESkillAction.TRAP:
                s = new SkillStateTrap();
                break;
            case ESkillAction.FADE:
                s = new SkillStateFade();
                break;
            case ESkillAction.BUFF:
                s = new SkillStateBuff();
                break;
            case ESkillAction.ARMAGGEDDON:
                s = new SkillStateArmaggeddon();
                break;
        }
        return s;
    }
}
