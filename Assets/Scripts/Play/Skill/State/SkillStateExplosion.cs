using UnityEngine;
using System.Collections;

public class SkillStateExplosion : SkillState
{
    public int collision;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);
    }

    public override void Execute(SkillController obj)
    {
        base.Execute(obj);
    }

    public override void Exit(SkillController obj)
    {
        base.Exit(obj);
    }

    public void destroy()
    {
        MonoBehaviour.Destroy(controller.gameObject);
    }
}
