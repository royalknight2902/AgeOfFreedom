using UnityEngine;
using System.Collections;

public class ObjectState : FSMState<ObjectController> 
{
    protected ObjectController controller;

    public override void Enter(ObjectController obj)
    {
        if (controller == null)
            controller = obj;
    }

    public override void Execute(ObjectController obj)
    {
    }

    public override void Exit(ObjectController obj)
    {
    }

    public void goNextState()
    {
        var enumerator = controller.listState.Keys.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (controller.StateAction == enumerator.Current)
            {
                enumerator.MoveNext();
                controller.StateAction = enumerator.Current;
                break;
            }
        }
    }
}
