using UnityEngine;
using System.Collections;

public class DragonPlayerData 
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public SMinMax ATK { get; set; }
    public int DEF { get; set; }
    public float ATKSpeed { get; set; }
    public float MoveSpeed { get; set; }

    public System.Collections.Generic.List<DragonPlayerSkillData> Skills = new System.Collections.Generic.List<DragonPlayerSkillData>();
    public System.Collections.Generic.Dictionary<string, AnimationEventState> States = new System.Collections.Generic.Dictionary<string, AnimationEventState>();
}
