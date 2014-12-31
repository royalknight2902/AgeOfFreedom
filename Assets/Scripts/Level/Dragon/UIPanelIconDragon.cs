using UnityEngine;
using System.Collections;

public class UIPanelIconDragon : MonoBehaviour
{

    public GameObject parent;

    void Start()
    {
        int len = ReadDatabase.Instance.DragonInfo.DragonInfo.Count;

        for (int i = 0; i < len; i++)
        {
            Dragon data = ReadDatabase.Instance.DragonInfo.DragonInfo[i + 1];

            GameObject dragon = Instantiate(Resources.Load<GameObject>("Prefab/Level/Icon Dragon")) as GameObject;

            dragon.transform.parent = gameObject.transform;
            dragon.GetComponent<UIAnchor>().container = gameObject;
            dragon.GetComponent<UIAnchor>().relativeOffset = new Vector2(0.1f + i * 0.2f, 0);
            dragon.GetComponent<UIStretch>().container = gameObject;
            dragon.GetComponent<UIAnchor>().enabled = true;
            dragon.GetComponent<UIStretch>().enabled = true;
            dragon.name = data.Name;
            dragon.transform.GetChild(0).GetComponent<UITexture>().mainTexture = Resources.Load("Image/Dragon/Icon/dragon-" + dragon.name.ToLower()) as Texture;

            IconDragonController iconController = dragon.GetComponent<IconDragonController>();
            iconController.Name = data.Name;
            iconController.ID = data.ID;
            iconController.ATK = data.ATK;
            iconController.DEF = data.DEF;
            iconController.HP = data.HP;
            iconController.MP = data.MP;
            iconController.Speed = data.Speed;
            iconController.ATKSpeed = data.Speed;
        }
    }
}
