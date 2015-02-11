using UnityEngine;
using System.Collections;

public class HouseStateDestroy : FSMState<HouseController>
{
    HouseController houseController;
    float elapsedTime;

    public override void Enter(HouseController obj)
    {
        houseController = obj;
        houseController.GetComponentInChildren<AutoTower>().Tower = houseController.gameObject;
        elapsedTime = 0.0f;

        //set scale
        obj.houseAnimation.transform.localScale = new Vector3(100, 100, 0);

        //set position
        obj.houseAnimation.transform.localPosition = PlayConfig.PositionTowerSell;
    }

    public override void Execute(HouseController obj)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1.5f)
        {
            houseController.GetComponentInChildren<AutoTower>().showTarget();
        }
    }

    public override void Exit(HouseController obj)
    {
    }

    public void fadeOutSprites()
    {
        foreach (Transform child in houseController.transform)
        {
            if (child.name.Equals("Animation"))
            {
                //Animation
                EffectSupportor.Instance.fadeOutAndDestroy(child.gameObject, ESpriteType.SPRITE_RENDERER, 1.0f);
                break;
            }
        }
    }
}
