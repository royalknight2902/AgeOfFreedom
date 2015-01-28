using UnityEngine;
using System.Collections;

public enum ESkillArmaggeddon
{
    METEOR,
    TORNADO,
}

public class SkillStateArmaggeddon : SkillState 
{
    public float duration;
    public ESkillArmaggeddon type;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);

        if (duration > 0.0f)
            obj.StartCoroutine(runNextState(duration));
    }

    public override void Execute(SkillController obj)
    {
        base.Execute(obj);
    }

    public override void Exit(SkillController obj)
    {
        base.Exit(obj);
    }
}
