using UnityEngine;
using System.Collections;

public class HouseStateOpen : FSMState<HouseController> 
{
    public override void Enter(HouseController obj)
    {
        //set scale
        obj.houseAnimation.transform.localScale = new Vector3(85, 85, 0);

        //set position
        obj.houseAnimation.transform.localPosition = PlayConfig.PositionDragonTower;
    }

    public override void Execute(HouseController obj)
    {

    }

    public override void Exit(HouseController obj)
    {
    }
}
