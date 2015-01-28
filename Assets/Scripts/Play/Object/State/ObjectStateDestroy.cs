using UnityEngine;
using System.Collections;

public class ObjectStateDestroy : ObjectState
{
    public override void Enter(ObjectController obj)
    {
        base.Enter(obj);
    }

    public override void Execute(ObjectController obj)
    {
        base.Execute(obj);
    }

    public override void Exit(ObjectController obj)
    {
        base.Execute(obj);
    }
}
