using UnityEngine;
using System.Collections;

public class BabyDragonStateIdle : FSMState<BabyDragonController> 
{
    public override void Enter(BabyDragonController obj)
    {
        if (obj.StateDirection == EDragonStateDirection.RIGHT)
        {
            Transform childAnimation = obj.transform.GetChild(0);
            childAnimation.localScale = new Vector3(Mathf.Abs(childAnimation.localScale.x) * -1, childAnimation.localScale.y, childAnimation.localScale.z);
        }
    }

    public override void Execute(BabyDragonController obj)
    {
    }

    public override void Exit(BabyDragonController obj)
    {
    }
}
