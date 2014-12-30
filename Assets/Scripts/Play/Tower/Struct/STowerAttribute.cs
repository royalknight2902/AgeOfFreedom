using UnityEngine;
using System.Collections;

[System.Serializable]
public class STowerAttribute
{
    public string Name;
    public int Cost;
    public int Range;
    public int MinATK;
    public int MaxATK;
    public float SpawnShoot;
    public float TimeBuild;
    public string Type;
}

[System.Serializable]
public class STowerPassiveAttribute
{
    public string Name;
    public int Cost;
    public int Value;
    public float UpdateTime;
    public float TimeBuild;
    public string Type;
}