using UnityEngine;
using System.Collections;

public class DragonItemsController : MonoBehaviour {

    public UISprite spriteIcon;
    public UILabel labelName;
    public UILabel labelInfoBonus;
    public GameObject spriteEquipment;
    

    public int ID { get; set; }

    public void EquipItem()
    {
        spriteEquipment.SetActive(true);
    }
    public void UnEquipItem()
    {
        spriteEquipment.SetActive(false);
    }
}
