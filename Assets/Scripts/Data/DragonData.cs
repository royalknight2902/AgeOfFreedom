using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonData
{
    public Dictionary<string, DragonPlayerData> Player = new Dictionary<string, DragonPlayerData>();
    public Dictionary<int, DragonHouseData> House = new Dictionary<int, DragonHouseData>();
    public Dictionary<int, DragonItemData> Item = new Dictionary<int, DragonItemData>();
}
