using UnityEngine;
using System.Collections;

public enum DragonItemType
{
    Head,
    Body,
    Ring,
    Amulet,
    Wing,
    Rune,
}

public class DragonItemsManager : Singleton<DragonItemsManager>
{

    public GameObject tempListDragonItem;
    public LevelSelectDragonAnimation dragonAnimation;
    public SDragonItemsAttribute attribute;

    [HideInInspector]
    public System.Collections.Generic.Dictionary<string, GameObject> listItems = new System.Collections.Generic.Dictionary<string, GameObject>();

    #region STATE MACHINE
    FiniteStateMachine<DragonItemsManager> FSM;

    public DragonItemsStateIdle stateIdle;

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

    void changeState(FSMState<DragonItemsManager> e)
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
        stateIdle = new DragonItemsStateIdle();

        FSM = new FiniteStateMachine<DragonItemsManager>();

        StateAction = EDragonStateAction.IDLE;
    }

    void Start()
    {
        initalize();
        runResources();
    }

    void OnEnable()
    {
        FSM.Configure(this, stateIdle);
        updateAttribute(PlayerInfo.Instance.dragonInfo.id);
    }

    void initalize()
    {
        #region DRAGON ITEM
        AutoDestroy.destroyChildren(tempListDragonItem, null);
        //Debug.Log(ReadDatabase.Instance.DragonItemInfo.Count);
        int i = 0;
        GameObject DragonItemBefore = null;
        float rowItemBefore = 0f;
        #region Code cũ
        foreach (System.Collections.Generic.KeyValuePair<string, DragonItemData> iterator in ReadDatabase.Instance.DragonInfo.Item)
        {
            GameObject dragonItem = Instantiate(Resources.Load<GameObject>("Prefab/Level/Dragon Item")) as GameObject;
            dragonItem.transform.parent = tempListDragonItem.transform;
            dragonItem.transform.localScale = Vector3.one;

            //set ID
            dragonItem.GetComponent<DragonItemsController>().ID = iterator.Key;

            //lay du lieu bonus tu item
            string bonusText = "";
            //int OptionInRow = 0; // 2 se xuong dong
            float row = 0f;

            for (int j = 0; j < iterator.Value.Options.Length; j++)
            {
                if (iterator.Value.Options[j] > 0)
                {

                    bonusText += DragonItemData.nameOptions[j] + "+" + iterator.Value.Options[j].ToString();

                    bonusText += "\n";
                    row++;
                }
            }
            bonusText = bonusText.Substring(0, bonusText.Length - 1); // bo /n cuoi cung, do hon xet if trong vong lap

            //Anchor
            UIAnchor uiAnchor = dragonItem.GetComponent<UIAnchor>();

            //Stretch
            UIStretch uiStretch = dragonItem.GetComponent<UIStretch>();

            if (i == 0) // Phan tu dau neo theo cha
            {
                uiAnchor.container = tempListDragonItem;
                uiStretch.container = tempListDragonItem;
                uiStretch.relativeSize = new Vector2(uiStretch.relativeSize.x, row / 10);
                dragonItem.GetComponent<UIDragScrollView>().scrollView = tempListDragonItem.GetComponent<UIScrollView>();
            }
            else // cac phan tu sau noi duoi voi nhau
            {
                uiAnchor.container = DragonItemBefore;
                uiAnchor.relativeOffset.y = -0.52f;
                uiAnchor.side = UIAnchor.Side.Center;
                //  dragonItem.GetComponent<UIWidget>().pivot = UIWidget.Pivot.Top;


                uiStretch.container = DragonItemBefore;
                uiStretch.relativeSize = new Vector2(uiStretch.relativeSize.x, row / rowItemBefore);
                dragonItem.GetComponent<UIDragScrollView>().scrollView = DragonItemBefore.GetComponent<UIScrollView>();
            }


        #endregion

            #region code thu nghiem
            //foreach (System.Collections.Generic.KeyValuePair<int, DragonItemData> iterator in ReadDatabase.Instance.DragonItemInfo)
            //{

            //    GameObject dragonItem = Instantiate(LevelManager.Instance.Model.DragonItem) as GameObject;
            //    dragonItem.transform.parent = tempListDragonItem.transform;
            //    dragonItem.transform.localScale = Vector3.one;

            //    //lay du lieu bonus tu item
            //    string bonusText = "";
            //    //int OptionInRow = 0; // 2 se xuong dong
            //    float row = 0f;

            //    for (int j = 0; j < iterator.Value.Options.Length; j++)
            //    {
            //        if (iterator.Value.Options[j] > 0)
            //        {
            //            bonusText += DragonItemData.nameOptions[j] + "+" + iterator.Value.Options[j].ToString();

            //            bonusText += "\n";
            //            row++;
            //        }
            //    }
            //    bonusText  = bonusText.Substring(0,bonusText.Length - 2); // bo /n cuoi cung, do hon xet if trong vong lap

            //    //Stretch
            //    UIStretch uiStretch =  dragonItem.GetComponent<UIStretch>();



            //    uiStretch.container = tempListDragonItem;
            //    uiStretch.relativeSize = new Vector2(uiStretch.relativeSize.x, row / 10);
            //    dragonItem.GetComponent<UIDragScrollView>().scrollView = tempListDragonItem.GetComponent<UIScrollView>();
            //    float currentPosY = 0f;
            //    if (i == 0)
            //        currentPosY = dragonItem.GetComponentInParent<UIPanel>().height / 2 - dragonItem.GetComponent<UIWidget>().height / 2;
            //    else
            //        currentPosY = beforePositionY - dragonItem.GetComponent<UIWidget>().height / 2;

            //    dragonItem.transform.localPosition = new Vector3(0, currentPosY, 0);
            //    Debug.Log(dragonItem.transform.localPosition);
            #endregion
            switch (iterator.Value.Name.Substring(iterator.Value.Name.LastIndexOf(' ') + 1))
            {
                case "Head":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Head;
                    break;
                case "Body":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Body;
                    break;
                case "Ring":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Ring;
                    break;
                case "Amulet":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Amulet;
                    break;
                case "Wing":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Wing;
                    break;
                case "Rune":
                    dragonItem.GetComponent<UIDragonItems>().dragonItemType = DragonItemType.Rune;
                    break;
            }

            dragonItem.transform.GetChild(0).gameObject.GetComponent<UILabel>().text = iterator.Value.Name;
            dragonItem.transform.GetChild(1).gameObject.GetComponent<UILabel>().text = bonusText;
            dragonItem.transform.GetChild(2).gameObject.GetComponent<UISprite>().spriteName = iterator.Value.Icon;

            dragonItem.SetActive(true);
            DragonItemBefore = dragonItem;
            rowItemBefore = row;

            listItems.Add(iterator.Key, dragonItem);

            i++;
        }
        #endregion

        #region LOAD DATABASE
        string dataHead = PlayerInfo.Instance.dragonInfo.itemHead;
        string dataWing = PlayerInfo.Instance.dragonInfo.itemWing;
        string dataRing = PlayerInfo.Instance.dragonInfo.itemRing;
        string dataAmulet = PlayerInfo.Instance.dragonInfo.itemAmulet;
        string dataBody = PlayerInfo.Instance.dragonInfo.itemBody;
        string dataRune = PlayerInfo.Instance.dragonInfo.itemRune;

        if (!dataHead.Equals(""))
        {
            GameObject item = listItems[dataHead];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        if (!dataWing.Equals(""))
        {
            GameObject item = listItems[dataWing];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        if (!dataRing.Equals(""))
        {
            GameObject item = listItems[dataRing];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        if (!dataAmulet.Equals(""))
        {
            GameObject item = listItems[dataAmulet];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        if (!dataBody.Equals(""))
        {
            GameObject item = listItems[dataBody];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        if (!dataRune.Equals(""))
        {
            GameObject item = listItems[dataRune];
            item.GetComponent<UIDragonItems>().EquipItem();
        }
        #endregion
    }

    public void updateAttribute(string branch)
    {
        DragonPlayerData data = ReadDatabase.Instance.DragonInfo.Player[branch];
        attribute.Name.text = branch;
        attribute.HP.text = "[000000]" + data.HP.ToString() + "[-]";
        attribute.MP.text = "[000000]" + data.MP.ToString() + "[-]";
        attribute.ATK.text = "[000000]" + data.ATK.Min + " - " + data.ATK.Max + "[-]";
        attribute.DEF.text = "[000000]" + data.DEF.ToString() + "[-]";
        attribute.ATKSpeed.text = "[000000]" + data.ATKSpeed.ToString() + "[-]";
        attribute.MoveSpeed.text = "[000000]" + data.MoveSpeed.ToString() + "[-]";

        SDragonAttributeBonus bonus = new SDragonAttributeBonus();
        bonus.ATK = bonus.DEF = 0;
        bonus.HP = bonus.MP = 0;
        bonus.ATKSpeed = bonus.MoveSpeed = 0.0f;

        if (!PlayerInfo.Instance.dragonInfo.itemHead.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemHead],ref bonus);
        if (!PlayerInfo.Instance.dragonInfo.itemWing.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemWing],ref bonus);
        if (!PlayerInfo.Instance.dragonInfo.itemRing.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemRing],ref bonus);
        if (!PlayerInfo.Instance.dragonInfo.itemAmulet.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemAmulet],ref bonus);
        if (!PlayerInfo.Instance.dragonInfo.itemBody.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemBody],ref bonus);
        if (!PlayerInfo.Instance.dragonInfo.itemRune.Equals(""))
            updateAttributeBonus(ReadDatabase.Instance.DragonInfo.Item[PlayerInfo.Instance.dragonInfo.itemRune],ref bonus);

        if (bonus.ATK != 0) //ATK
            attribute.ATK.text += ((bonus.ATK > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.ATK) + "[-]";
        if (bonus.DEF != 0) //DEF
            attribute.DEF.text += ((bonus.DEF > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.DEF) + "[-]";
        if (bonus.HP != 0) //HP
            attribute.HP.text += ((bonus.HP > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.HP) + "[-]";
        if (bonus.MP != 0) //MP
            attribute.MP.text += ((bonus.MP > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.MP) + "[-]";
        if (bonus.ATKSpeed != 0) //ATKSpeed
            attribute.ATKSpeed.text += ((bonus.ATKSpeed > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.ATKSpeed) + "[-]";
        if (bonus.MoveSpeed != 0) //MoveSpeed
            attribute.MoveSpeed.text += ((bonus.MoveSpeed > 0) ? "[34a00a] + " : "[be0d0d] - ") + Mathf.Abs(bonus.MoveSpeed)+ "[-]";
    }

    public void updateAttributeBonus(DragonItemData item,ref SDragonAttributeBonus bonus)
    {
        if (item.Options[0] != 0) //ATK
            bonus.ATK += (int)item.Options[0];
        if (item.Options[1] != 0) // DEF
            bonus.DEF += (int)item.Options[1];
        if (item.Options[2] != 0) // HP
            bonus.HP += (int)item.Options[2];
        if (item.Options[3] != 0) // MP
            bonus.MP += (int)item.Options[3];
        if (item.Options[4] != 0) // ATK Speed
            bonus.ATKSpeed += item.Options[4];
        if (item.Options[5] != 0) // Move Speed
            bonus.MoveSpeed += item.Options[5];
    }

    public void updateListItem(GameObject destroyObject)
    {
        if (destroyObject.GetComponent<UIAnchor>().side == UIAnchor.Side.TopLeft) // item dau
        {
            GameObject deleteItem = tempListDragonItem.transform.GetChild(0).gameObject;
            UIAnchor uiAnchorDeleteItem = deleteItem.GetComponent<UIAnchor>();
            UIStretch uiStretchDeleteItem = deleteItem.GetComponent<UIStretch>();
            if (tempListDragonItem.transform.childCount > 1)
            {
                GameObject secondItem = tempListDragonItem.transform.GetChild(1).gameObject;
                UIAnchor uiAnchorSecondItem = secondItem.GetComponent<UIAnchor>();
                UIStretch uiStretchSecondItem = secondItem.GetComponent<UIStretch>();
                uiAnchorSecondItem.container = uiAnchorDeleteItem.container;
                uiAnchorSecondItem.side = uiAnchorDeleteItem.side;
                uiAnchorSecondItem.relativeOffset = uiAnchorDeleteItem.relativeOffset;

                uiStretchSecondItem.container = uiStretchDeleteItem.container;
                secondItem.transform.localPosition = deleteItem.transform.localPosition;
                secondItem.GetComponent<UIWidget>().pivot = UIWidget.Pivot.TopLeft;
            }
            AutoDestroy.Destroy(destroyObject);
        }
        else
        {

        }
    }
}
