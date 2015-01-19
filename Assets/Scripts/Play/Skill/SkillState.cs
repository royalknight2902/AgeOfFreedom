using UnityEngine;
using System.Collections;

public class SkillState : FSMState<SkillController>
{
    protected SkillController controller;

    public override void Enter(SkillController obj)
    {
        if (controller == null)
            controller = obj;
    }

    public override void Execute(SkillController obj)
    {
    }

    public override void Exit(SkillController obj)
    {
    }

    public void goNextState()
    {
        var enumerator = controller.listState.Keys.GetEnumerator();
        while(enumerator.MoveNext())
        {
            if(controller.StateAction == enumerator.Current)
            {
                enumerator.MoveNext();
                controller.StateAction = enumerator.Current;
                break;
            }
        }
    }

    public IEnumerator runNextState(float time)
    {
        yield return new WaitForSeconds(time);
        goNextState();
        yield return 0;
    }

    public IEnumerator runDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        controller.GetComponentInChildren<AutoDestroy>().destroyParent();
        yield return 0;
    }
}
