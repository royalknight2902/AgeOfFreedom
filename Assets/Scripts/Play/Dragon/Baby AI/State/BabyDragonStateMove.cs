using UnityEngine;
using System.Collections;

public class BabyDragonStateMove : FSMState<BabyDragonController>
{
    //public EDragonStateDirection attackDirection { get; set; }

    private EDragonStateDirection preStateDirection;

    public override void Enter(BabyDragonController obj)
    {
        preStateDirection = obj.StateDirection;
    }

    public override void Execute(BabyDragonController obj)
    {
        Vector3 destPosition = obj.dragonParent.stateMove.listPosition[PlayConfig.BabyDragonIndexForListStart
                                - PlayConfig.BabyDragonIndexForListDistance * obj.index];

        if (Vector3.Distance(obj.transform.position, destPosition) <= 0.01f)
        {
            obj.StateAction = EDragonStateAction.IDLE;
        }

        if (obj.transform.position != destPosition)
        {
            if (Time.timeScale != 0.0f)
            {
                if (obj.transform.position.x > destPosition.x)
                {
                    if (obj.StateDirection != EDragonStateDirection.LEFT)
                        obj.StateDirection = EDragonStateDirection.LEFT;
                }
                else
                {
                    if (obj.StateDirection != EDragonStateDirection.RIGHT)
                        obj.StateDirection = EDragonStateDirection.RIGHT;
                }
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, destPosition, obj.attribute.Speed / 1000);
            }
        }

        if (preStateDirection != obj.StateDirection)
        {
            preStateDirection = obj.StateDirection;

            if (obj.StateDirection == EDragonStateDirection.LEFT)
            {
                Vector3 scale = obj.transform.GetChild(0).localScale;
                obj.transform.GetChild(0).localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
            }
            else
            {
                Vector3 scale = obj.transform.GetChild(0).localScale;
                obj.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
    }

    public override void Exit(BabyDragonController obj)
    {
    }
}
