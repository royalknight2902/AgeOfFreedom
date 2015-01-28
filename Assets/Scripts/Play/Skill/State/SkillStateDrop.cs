using UnityEngine;
using System.Collections;

public class SkillStateDrop : SkillState
{
    public float Speed;
    public Vector3 destPosition;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);

        float positionY = 1.0f + PlayManager.Instance.tempInit.cameraRender.transform.position.y;
        obj.transform.position = new Vector2(destPosition.x, positionY);
    }

    public override void Execute(SkillController obj)
    {
        obj.transform.localPosition += Vector3.down * (Speed / 2);
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
