using UnityEngine;
using System.Collections;

public class UIPanelIconDragon : MonoBehaviour
{
    void Start()
    {
        int len = ReadDatabase.Instance.DragonInfo.DragonInfo.Count;

        for (int i = 0; i < len; i++)
        {
            GameObject dragon = Instantiate(Resources.Load<GameObject>("Prefab/Level/Icon Dragon")) as GameObject;

            dragon.transform.parent = gameObject.transform;
            dragon.GetComponent<UIAnchor>().container = gameObject;
            dragon.GetComponent<UIAnchor>().relativeOffset = new Vector2(0.1f + i * 0.2f, 0);
            dragon.GetComponent<UIStretch>().container = gameObject;
            dragon.GetComponent<UIAnchor>().enabled = true;
            dragon.GetComponent<UIStretch>().enabled = true;
            dragon.name = ReadDatabase.Instance.DragonInfo.DragonInfo[i + 1].Name;
            //Debug.Log(dragon.transform.GetChild(0).GetComponent<UITexture>().mainTexture.name);
            dragon.transform.GetChild(0).GetComponent<UITexture>().mainTexture = Resources.Load("Image/Dragon/Icon/dragon-" + dragon.name.ToLower()) as Texture;
        }
    }
}
