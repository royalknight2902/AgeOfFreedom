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
public enum DragonItemState
{ 
    Equipping,
    UnEquipping,
}




public class UIDragonItems : MonoBehaviour {

    public DragonItemType dragonItemType;

    public DragonItemState dragonItemState;


    public GameObject PanelMainAttribute;

    private bool isClicked;

    DragonItemAction dragonItemAction;
    DragonItemsController dragonItemController;
    void Start()
    {

        dragonItemAction = GetComponent<DragonItemAction>();
        dragonItemController = GetComponent<DragonItemsController>();
        PanelMainAttribute = GameObject.FindWithTag("MainAttributeDragon");
    }

    void OnDoubleClick()
    {
        isClicked = true;
        if (isClicked)
        {
            switch (dragonItemState)
            {
                case DragonItemState.Equipping:
                    this.UnEquipItem();
                    break;
                case DragonItemState.UnEquipping:
                    this.EquipItem();
                    break;

            }
            isClicked = false;
        }
        //DragonItemsManager.Instance.UpdateListItem(this.gameObject);
    }
    void UnEquipItem()
    {
        GameObject go = GameObject.FindWithTag(dragonItemType.ToString());
        string itemName = go.GetComponent<UITexture>().mainTexture.name;
        go.GetComponent<UITexture>().mainTexture = null;
        dragonItemState = DragonItemState.UnEquipping;
       
     

        dragonItemController.UnEquipItem();
    }


    void EquipItem()
    {
        GameObject go = GameObject.FindWithTag(dragonItemType.ToString());   
         
        string itemName = transform.GetChild(0).gameObject.GetComponent<UILabel>().text;
        string path = "Image/Item/Dragon Items/" + transform.GetChild(2).gameObject.GetComponent<UISprite>().spriteName; // child(2) = Icon
          
        go.GetComponent<UITexture>().mainTexture = Resources.Load<Texture>(path);
        dragonItemState = DragonItemState.Equipping;
   
        dragonItemController.EquipItem();
       // dragonItemAction.bonusAttribute(itemName);
    }
    void InitToLabel()
    {
        GameObject lblAttack = PanelMainAttribute.transform.GetChild(0).gameObject;
        GameObject lblDefense = PanelMainAttribute.transform.GetChild(0).gameObject;
        GameObject lblAttackSpeed = PanelMainAttribute.transform.GetChild(0).gameObject;
        GameObject lblHP = PanelMainAttribute.transform.GetChild(0).gameObject;
        GameObject lblMP = PanelMainAttribute.transform.GetChild(0).gameObject;
        GameObject MS = PanelMainAttribute.transform.GetChild(0).gameObject;
    }
}


