using UnityEngine;
using System.Collections;

public enum DragonItemState
{ 
    Equipping,
    UnEquipping,
}

public class UIDragonItems : MonoBehaviour {

    public DragonItemType dragonItemType;
    public DragonItemState dragonItemState;

    bool isClicked;
    DragonItemsController dragonItemController;

    void Awake()
    {
        dragonItemController = GetComponent<DragonItemsController>();
    }

    void OnDoubleClick()
    {
        isClicked = true;
        if (isClicked)
        {
            switch (dragonItemState)
            {
                case DragonItemState.Equipping:
                    UnEquipItem();
                    saveData(false);
                    break;
                case DragonItemState.UnEquipping:
                    EquipItem();
                    saveData(true);
                    break;

            }
            isClicked = false;
            DragonItemsManager.Instance.updateAttribute(PlayerInfo.Instance.dragonInfo.id);
        }
    }

    public void UnEquipItem()
    {
        GameObject go = GameObject.FindWithTag(dragonItemType.ToString());
        string itemName = go.GetComponent<UITexture>().mainTexture.name;
        go.GetComponent<UITexture>().mainTexture = null;
        dragonItemState = DragonItemState.UnEquipping;

        dragonItemController.UnEquipItem();
    }

    public void EquipItem()
    {
        GameObject go = GameObject.FindWithTag(dragonItemType.ToString());   
         
        string itemName = transform.GetChild(0).gameObject.GetComponent<UILabel>().text;
        string path = "Image/Item/Dragon Items/" + transform.GetChild(2).gameObject.GetComponent<UISprite>().spriteName; // child(2) = Icon

        //keep resolution
        UITexture texture = go.GetComponent<UITexture>();
        texture.mainTexture = Resources.Load<Texture>(path);
        texture.keepAspectRatio = UIWidget.AspectRatioSource.Free;

        Vector2 localSize = new Vector2(texture.mainTexture.width, texture.mainTexture.height);
        texture.SetDimensions((int)localSize.x, (int)localSize.y);
        texture.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
 
        //stretch
        go.GetComponent<UIStretch>().enabled = true;
       
        //add fix ui stretch
        go.AddComponent<FixUIStretch>();

        dragonItemState = DragonItemState.Equipping;
   
        dragonItemController.EquipItem();
    }

    void saveData(bool newData)
    {
        switch (dragonItemType)
        {
            case DragonItemType.Head:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemHead = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemHead = "";
                break;
            case DragonItemType.Body:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemBody = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemBody = "";
                break;
            case DragonItemType.Wing:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemWing = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemWing = "";
                break;
            case DragonItemType.Amulet:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemAmulet = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemAmulet = "";
                break;
            case DragonItemType.Ring:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemRing = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemRing = "";
                break;
            case DragonItemType.Rune:
                if (newData)
                    PlayerInfo.Instance.dragonInfo.itemRune = GetComponent<DragonItemsController>().ID;
                else
                    PlayerInfo.Instance.dragonInfo.itemRune = "";
                break;
        }

        PlayerInfo.Instance.dragonInfo.Save();
    }
}


