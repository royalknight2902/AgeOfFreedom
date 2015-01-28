using UnityEngine;
using System.Collections;

public class ObjectStateExplosion : ObjectState
{
    public override void Enter(ObjectController obj)
    {
        base.Enter(obj);

        obj.transform.parent = PlayManager.Instance.Temp.Explosion.transform;
    }

    public override void Execute(ObjectController obj)
    {
        base.Execute(obj);
    }

    public override void Exit(ObjectController obj)
    {
        base.Exit(obj);
    }

    public void destroy()
    {
        MonoBehaviour.Destroy(controller.gameObject);
    }
}
