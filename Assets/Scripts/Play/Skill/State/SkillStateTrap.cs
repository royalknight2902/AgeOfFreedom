using UnityEngine;
using System.Collections;

public class SkillStateTrap : SkillState 
{
    public Vector3 position;
    public float duration;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);
        obj.transform.position = position;

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
