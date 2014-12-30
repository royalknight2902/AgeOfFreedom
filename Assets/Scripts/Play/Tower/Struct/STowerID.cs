using UnityEngine;
using System.Collections;

public enum ETower
{
    ARCHITECT,
    ROCK,
    ICE,
    FIRE,
    GOLD,
    POISON,
    DRAGON,
}

[System.Serializable]
public class STowerID 
{
    public ETower Type;
    public int Level;

    public STowerID(ETower type, int level)
    {
        Type = type;
        Level = level;
    }
}
