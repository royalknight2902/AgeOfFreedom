using UnityEngine;
using System.Collections;

public class MockController : MonoBehaviour {

    public int mapID;
    public string mapName;

    void Start()
    {
        ReadDatabase.Instance.MapLevelInfo[mapID].Name = mapName;
    }
}
