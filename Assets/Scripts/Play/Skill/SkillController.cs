using UnityEngine;
using System.Collections;

public enum ESkillAction
{
    ONCE,
    TRAP,
    FADE,
    DROP,
    EXPLOSION,
    BUFF,
    ARMAGGEDDON,
    DESTROY,
}

public enum ESkillType
{
    TARGET,
    BUFF,
    GLOBAL,
}

public enum ESkillOffense
{
    SINGLE,
    AOE,
}

public enum ESkillBuff
{
    PLAYER,
    ENEMY,
}

public enum ESkillCollider
{
    SPHERE,
    BOX,
    CAPSULE,
}

public class SkillController : MonoBehaviour
{
    public string ID { get; set; }
    public GameObject Owner { get; set; }
    public ESkillType Type { get; set; }
    public object Ability { get; set; }

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

    public SkillState getCurrentState()
    {
        return listState[stateAction];
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

    public void initalize(string ID, ESkillType skillType, object ability, object[] data = null)
    {
        this.ID = ID;
        this.name = ID;
        this.Type = skillType;
        this.Ability = ability;

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
        setPropertyState(skillType, data);
        //set Property from database
        setPropertyFromDatabase();

        FSM.Configure(this, listState[enumerator.Current]);
        stateAction = enumerator.Current;

        runResources();
    }

    void setPropertyState(ESkillType skillType, object[] data) // for destination position
    {
        if (data == null)
            return;

        if (data.Length <= 0)
            return;

        if (skillType == ESkillType.TARGET)
        {
            foreach (System.Collections.Generic.KeyValuePair<ESkillAction, SkillState> iterator in listState)
            {
                switch (iterator.Key)
                {
                    case ESkillAction.ONCE:
                        SkillStateOnce stateOnce = iterator.Value as SkillStateOnce;
                        if ((ESkillOffense)Ability == ESkillOffense.SINGLE)
                            stateOnce.enemy = (GameObject)data[0];
                        else
                            stateOnce.destPosition = (Vector3)data[0];
                        break;
                    case ESkillAction.DROP:
                        SkillStateDrop stateDrop = iterator.Value as SkillStateDrop;
                        stateDrop.destPosition = (Vector3)data[0];
                        break;
                    case ESkillAction.TRAP:
                        SkillStateTrap stateTrap = iterator.Value as SkillStateTrap;
                        stateTrap.position = (Vector3)data[0];
                        break;
                }
            }
        }
    }

    static string[] ColliderName = { "SphereCollider", "BoxCollider", "CapsuleCollider" };
    void setPropertyFromDatabase()
    {
        foreach (System.Collections.Generic.KeyValuePair<ESkillAction, SkillState> state in listState)
        {
            foreach (System.Collections.Generic.KeyValuePair<string, object> iterator in
                ReadDatabase.Instance.SkillInfo[ID.ToUpper()].States[state.Key.ToString()].Values)
            {
                Debug.Log(iterator.Key.ToUpper());
                #region COMMON VALUE
                switch (iterator.Key.ToUpper())
                {
                    case "COLLISION":
                        state.Value.collisionNum = int.Parse(iterator.Value.ToString());
                        return;
                    case "EFFECT":
                        string[] s = iterator.Value.ToString().Trim().Split('/');
                        state.Value.effectType = (EBulletEffect)Extensions.GetEnum(EBulletEffect.NONE.GetType(), s[0].ToUpper());

                        System.Collections.Generic.List<string> listValue = new System.Collections.Generic.List<string>();
                        for (int i = 1; i < s.Length; i++)
                        {
                            listValue.Add(s[i]);
                        }
                        state.Value.effectValue = listValue.ToArray();
                        break;
                    case "EFFECTGO":
                        state.Value.effectObjectID = iterator.Value.ToString();
                        break;
                    case "DAMAGE":
                        if (iterator.Value.ToString().Trim().Equals("true"))
                            state.Value.hasDamage = true;
                        else
                            state.Value.hasDamage = false;
                        break;
                    case "COLLIDERMOVETO":
                        state.Value.colliderMoveTo = iterator.Value.ToString();
                        break;
                }
                #endregion

                #region COLLIDER
                bool hasCollider = false;
                ESkillCollider colliderType = ESkillCollider.BOX;
                for (int i = 0; i < ColliderName.Length; i++)
                {
                    string s = ColliderName[i];
                    if (string.Compare(iterator.Key.Trim(), s, true) == 0)
                    {
                        hasCollider = true;
                        switch (s)
                        {
                            case "SphereCollider": colliderType = ESkillCollider.SPHERE; break;
                            case "BoxCollider": colliderType = ESkillCollider.BOX; break;
                            case "CapsuleCollider": colliderType = ESkillCollider.CAPSULE; break;
                        }
                        break;
                    }
                }
                #endregion

                if (hasCollider)
                {
                    state.Value.hasCollider = true;
                    state.Value.colliderValue = iterator.Value;
                    state.Value.colliderType = colliderType;
                }
                else
                {
                    switch (state.Key)
                    {
                        #region DROP
                        case ESkillAction.DROP:
                            SkillStateDrop drop = state.Value as SkillStateDrop;

                            switch (iterator.Key.ToUpper())
                            {
                                case "SPEED":
                                    drop.Speed = float.Parse(iterator.Value.ToString());
                                    break;
                            }
                            break;
                        #endregion
                        #region TRAP
                        case ESkillAction.TRAP:
                            SkillStateTrap trap = state.Value as SkillStateTrap;

                            switch (iterator.Key.ToUpper())
                            {
                                case "DURATION":
                                    trap.duration = float.Parse(iterator.Value.ToString());
                                    break;
                            }
                            break;
                        #endregion
                        #region BUFF
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
                        #endregion
                        #region ARMAGGEDDON
                        case ESkillAction.ARMAGGEDDON:
                            SkillStateArmaggeddon armaggeddon = state.Value as SkillStateArmaggeddon;

                            switch (iterator.Key.ToUpper())
                            {
                                case "DURATION":
                                    armaggeddon.duration = float.Parse(iterator.Value.ToString());
                                    break;
                                case "TYPE":
                                    armaggeddon.type = (ESkillArmaggeddon)Extensions.GetEnum(ESkillArmaggeddon.METEOR.GetType(), 
                                        iterator.Value.ToString().ToUpper());
                                    break;
                            }
                            break;
                        #endregion
                        #region END
                        case ESkillAction.EXPLOSION:
                            SkillStateExplosion end = state.Value as SkillStateExplosion;

                            switch (iterator.Key.ToUpper())
                            {
                                case "COLLISION":
                                    end.collision = int.Parse(iterator.Value.ToString());
                                    break;
                            }
                            break;
                        #endregion
                    }
                }
            }
        }
    }

    SkillState getClassSkill(ESkillAction type)
    {
        SkillState s = null;

        switch (type)
        {
            case ESkillAction.ONCE:
                s = new SkillStateOnce();
                break;
            case ESkillAction.DROP:
                s = new SkillStateDrop();
                break;
            case ESkillAction.EXPLOSION:
                s = new SkillStateExplosion();
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
            case ESkillAction.DESTROY:
                s = new SkillStateDestroy();
                break;
        }
        return s;
    }
}
