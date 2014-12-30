using UnityEngine;
using System.Collections;

public class MapInfoController : MonoBehaviour
{
    public UILabel labelName;
    public UITexture map;
    public StarController starController;

    int mapID;
    public int MapID
    {
        get
        {
            return mapID;
        }
    }

    public void initalize(string _mapName, int _mapID, int starSuccess)
    {
        mapID = _mapID;
        string s = GameConfig.PathMap + _mapID;

        labelName.text = _mapName;
        map.mainTexture = Resources.Load<Texture>(s);

        int t = 1;
        foreach (GameObject obj in starController.stars)
        {
            if (t <= starSuccess)
                obj.GetComponent<UISprite>().spriteName = "icon-star";
            else
                obj.GetComponent<UISprite>().spriteName = "icon-star-off";

            t++;
        }

    }
}
