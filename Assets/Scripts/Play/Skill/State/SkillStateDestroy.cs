using UnityEngine;
using System.Collections;

public class SkillStateDestroy : SkillState {

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);
        MonoBehaviour.Destroy(obj.gameObject);
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
