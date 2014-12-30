using UnityEngine;
using System.Collections;

public class EnemyData
{
    public string Name { get; set; }
    public string Branch { get; set; }
    public int HP { get; set; }
    public float Speed { get; set; }
    public int DEF { get; set; }
    public string Region { get; set; }
    public int Coin { get; set; }
    public int Level { get; set; }
    public int Scale { get; set; }
    public System.Collections.Generic.Dictionary<string, AnimationEventState> States = new System.Collections.Generic.Dictionary<string, AnimationEventState>();
}
