using UnityEngine;
using System.Collections;

public class IconDragonController : MonoBehaviour
{
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public int ID;
    [HideInInspector]
    public int ATK;
    [HideInInspector]
    public int DEF;
    [HideInInspector]
    public int HP;
    [HideInInspector]
    public int MP;
    [HideInInspector]
    public int Speed;
    [HideInInspector]
    public int ATKSpeed;

    void OnClick()
    {
        UIAttributeDragon attributeDragon = gameObject.transform.parent.GetComponent<UIPanelIconDragon>().parent.transform.GetChild(0).GetComponent<UIAttributeDragon>();
        attributeDragon.labelATK.text = ATK.ToString();
        attributeDragon.labelDEF.text = DEF.ToString();
    }
}