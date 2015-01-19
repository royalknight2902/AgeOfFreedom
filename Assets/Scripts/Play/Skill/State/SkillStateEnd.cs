using UnityEngine;
using System.Collections;

public class SkillStateEnd : SkillState
{
    public override void Enter(SkillController obj)
    {
        base.Enter(obj);
    }

    public override void Execute(SkillController obj)
    {
    }

    public override void Exit(SkillController obj)
    {
    }

    public void destroy()
    {
        MonoBehaviour.Destroy(controller.gameObject);
    }
}
