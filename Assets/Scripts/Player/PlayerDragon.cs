using UnityEngine;
using System.Collections;

public class PlayerDragon : ObjectData
{
    public string id;
    public int rank;
    public string itemHead;
    public string itemWing;
    public string itemRing;
    public string itemAmulet;
    public string itemBody;
    public string itemRune;

    public PlayerDragon()
    {
        id = "";
        rank = 1;
        itemHead = itemWing = itemRing = itemAmulet = itemBody = itemRune = "";
    }

    public PlayerDragon(string idBranch)
    {
        id = idBranch;
    }
}
