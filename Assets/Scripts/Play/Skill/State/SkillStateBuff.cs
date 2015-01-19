using UnityEngine;
using System.Collections;

public enum ESkillStateBuffType
{
    ROTATION,
}

public class SkillStateBuff : SkillState 
{
    public ESkillStateBuffType type;
    public float duration;
    public object value;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);

        switch(type)
        {
            case ESkillStateBuffType.ROTATION:
                TweenRotation tween = obj.skillAnimation.gameObject.AddComponent<TweenRotation>();
                tween.from = Vector3.zero;
                tween.to = new Vector3(0, 0, 360);
                tween.style = UITweener.Style.Loop;
                tween.duration = float.Parse(value.ToString());

                obj.transform.parent = obj.Owner.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                break;
        }

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
