using UnityEngine;
using System.Collections;

public class SkillData
{
    public string Name { get; set; }
    public float Cooldown { get; set; }
    public int Mana { get; set; }
    public ESkillType Type { get; set; }
    public object Ability { get; set; }
    public System.Collections.Generic.Dictionary<string, AnimationEventState> States = new System.Collections.Generic.Dictionary<string, AnimationEventState>();
}
