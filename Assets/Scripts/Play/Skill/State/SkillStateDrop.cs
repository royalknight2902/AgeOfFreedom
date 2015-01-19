using UnityEngine;
using System.Collections;

public class SkillStateDrop : SkillState
{
    public float Speed;
    public float Duration;
    public Vector3 destPosition;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);

        Speed = 1.0f;
        Duration = 2.0f;

        float positionY = destPosition.y + 0.5f;
        obj.transform.position = new Vector2(destPosition.x, positionY);
    }

    public override void Execute(SkillController obj)
    {
        obj.transform.localPosition += Vector3.down * 1.2f;
        if (Vector2.Distance(obj.transform.position, destPosition) <= 0.05f)
        {
            goNextState();
            return;
        }
    }

    public override void Exit(SkillController obj)
    {
    }
}
