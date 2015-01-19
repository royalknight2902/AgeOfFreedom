using UnityEngine;
using System.Collections;

public class SkillStateFade : SkillState 
{
    public float Duration;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);
        Duration = 3.0f;

        EffectSupportor.Instance.fadeOutWithEvent(obj.skillAnimation.gameObject, ESpriteType.SPRITE_RENDERER,
            Duration, new EventDelegate(obj.GetComponentInChildren<AutoDestroy>().destroyParent));
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
