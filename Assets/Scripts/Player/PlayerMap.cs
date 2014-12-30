using UnityEngine;
using System.Collections;

public class PlayerMap : ObjectData
{
    public int id;
    public int starSuccess;
    public int starTotal;

    public PlayerMap()
    {
        this.id = 1;
        this.starSuccess = 0;
        this.starTotal = 3;
    }

    public PlayerMap(int idMap, int starSuccess, int starTotal)
    {
        this.id = idMap;
        this.starSuccess = starSuccess;
        this.starTotal = starTotal;
    }
}
