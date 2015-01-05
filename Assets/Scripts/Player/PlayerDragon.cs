using UnityEngine;
using System.Collections;

public class PlayerDragon : ObjectData
{
    public string id;
    public int rank;

    public PlayerDragon()
    {
        id = "";
        rank = 1;
    }

    public PlayerDragon(string idBranch)
    {
        id = idBranch;
    }
}
