using UnityEngine;
using System.Collections;

public enum EEnemyRegion
{
    LAND,
    AIR,
}

public enum EEnemyType
{
    FIRE,
    ICE,
    THUNDER,
    BLIZZARD,
    EARTH,
    ROBOT,
    ZOMBIE,
}

public enum EEnemyStateAction
{
    MOVE,
    DIE,
    ATTACK,
}

public class EnemyController : MonoBehaviour
{
    public SEnemyAttribute attribute;
    public int level;
    public string ID;
    public int money;
    public float speed = 0;
    public int EXP;
    public EEnemyRegion region;
    public EEnemyType type;

    public int waveID { get; set; }
    public bool isEnable { get; set; }
    public bool isDie { get; set; }
    public bool isKilledByPlayerDragon { get; set; }

    public System.Collections.Generic.List<object> listEffected = new System.Collections.Generic.List<object>();

    [HideInInspector]
    public UISlider sliderHP;
    [HideInInspector]
    public EnemyAttack enemyAttack;

    EnemyAnimation enemyAnimation;

    #region STATE MACHINE
    FiniteStateMachine<EnemyController> FSM;
    public EnemyStateAttack stateAttack;
    public EnemyStateDie stateDie;
    public EnemyStateMove stateMove;

    EEnemyStateAction stateAction;
    EDirection stateDirection;

    public EEnemyStateAction StateAction
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
                    case EEnemyStateAction.ATTACK:
                        changeState(stateAttack);
                        break;
                    case EEnemyStateAction.DIE:
                        changeState(stateDie);
                        break;
                    case EEnemyStateAction.MOVE:
                        changeState(stateMove);
                        break;
                }
            }
        }
    }

    public EDirection StateDirection
    {
        get
        {
            return stateDirection;
        }
        set
        {
            if (stateDirection != value)
            {
                stateDirection = value;
                if (stateDirection != EDirection.NONE)
                    runResources();
            }
        }
    }

    void changeState(FSMState<EnemyController> e)
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
        if (StateAction == EEnemyStateAction.MOVE && StateDirection == EDirection.NONE)
            return;

        enemyAnimation.changeResources();
    }
    #endregion

    void Awake()
    {
        FSM = new FiniteStateMachine<EnemyController>();
        stateAttack = new EnemyStateAttack();
        stateDie = new EnemyStateDie();
        stateMove = new EnemyStateMove();

        enemyAnimation = this.GetComponentInChildren<EnemyAnimation>();
        enemyAttack = this.GetComponentInChildren<EnemyAttack>();

        StateAction = EEnemyStateAction.MOVE;
        StateDirection = EDirection.NONE;
    }

    void Start()
    {
        attribute.HP.Current = attribute.HP.Max;
        sliderHP = GetComponentInChildren<UISlider>();

        isEnable = true;
        isDie = false;
        isKilledByPlayerDragon = false;

        FSM.Configure(this, stateMove);

        runResources();
        setDepth();
    }

    public void updateHP()
    {
        sliderHP.value = (float)attribute.HP.Current / attribute.HP.Max;
        if (sliderHP.value <= 0)
        {
            sliderHP.value = 0.0f;
        }
    }

    void setDepth()
    {
        if (SceneState.Instance.State == ESceneState.ADVENTURE)
        {
            if (PlayTouchManager.Instance.currentOffense == ESkillOffense.SINGLE)
                GetComponent<UIWidget>().depth = 1;
        }
    }

    public void die()
    {
        int iBonus = 0;

        if (ItemManager.Instance.listItemState.Contains(EItemState.HAND_OF_MIDAS))
        {
            iBonus += (int)(ItemManager.Instance.BonusCoin * money);
        }

        PlayInfo.Instance.Money += (money + iBonus);

        GameObject temp = new GameObject();
        temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
        temp.transform.position = this.transform.position;
        temp.transform.localScale = Vector3.one;
        temp.name = "Temp Enemy Score";

        GameObject enemyScore = Instantiate(PlayManager.Instance.modelPlay.EnemyScore) as GameObject;
        enemyScore.transform.parent = temp.transform;
        enemyScore.transform.localPosition = Vector3.zero;
        enemyScore.transform.localScale = Vector3.one;
        enemyScore.GetComponent<Animator>().SetBool("isLeft", (UnityEngine.Random.Range(0, 2) == 1) ? true : false);


        UILabel label = enemyScore.GetComponent<UILabel>();
        label.text = (money + iBonus).ToString();

        //update achievement
        PlayAchievement.Instance.updateValueEnemy();
        if (region == EEnemyRegion.AIR)
        {
            PlayAchievement.Instance.updateValueEnemyAir();
        }

        //check diamond for player
        WaveController.Instance.checkDiamond(this.waveID, this.gameObject, true);

        //Enemy die
        this.isDie = true;


        if (stateDirection == EDirection.LEFT)
        {
            gameObject.transform.Rotate(new Vector3(0, 180, 0));
        }
        sliderHP.gameObject.SetActive(false);

        //If killed by dragon, give dragon EXP
        if (SceneState.Instance.State == ESceneState.ADVENTURE)
        {
            DragonController dragonController = PlayDragonManager.Instance.PlayerDragon.GetComponent<DragonController>();
            dragonController.EXP += EXP;
        }

        StateAction = EEnemyStateAction.DIE;

        //towerAction.enemyArray.Remove(_enemyController.gameObject);

        if (SceneState.Instance.State != ESceneState.ADVENTURE)
        {
            PlayManager.Instance.dailyQuestEvent_enemyDie(this.transform);
            return;
        }

        // check victory
        WaveController.Instance.enemy_current--;
        if (WaveController.Instance.enemy_current <= 0 && PlayInfo.Instance.Heart > 0)
        {
            PlayManager.Instance.showVictory();
        }
    }

    public void stop()
    {
        enemyAnimation.enableAnimation(false);
        isEnable = false;
    }

    public void resume()
    {
        enemyAnimation.enableAnimation(true);
        isEnable = true;
    }
}
