using UnityEngine;
using System.Collections;

public class HouseStateBuild : FSMState<HouseController>
{
    UISlider slider;
    float valuePerFrame;

    public override void Enter(HouseController obj)
    {
        slider = obj.gameObject.GetComponentInChildren<UISlider>();

        slider.value = 0;
        valuePerFrame = Time.fixedDeltaTime / (obj.attribute.TimeBuild / PlayerInfo.Instance.userInfo.timeScale);

        //set scale
        obj.houseAnimation.transform.localScale = new Vector3(100, 100, 0);

        //set position
        obj.houseAnimation.transform.localPosition = PlayConfig.PositionTowerBuild;
    }

    public override void Execute(HouseController obj)
    {
        slider.value += valuePerFrame;
        if (slider.value >= 1.0f)
        {
            slider.value = 1.0f;
            slider.gameObject.SetActive(false);
            obj.GetComponent<HouseAction>().isActivity = true;

            obj.StateAction = EHouseStateAction.IDLE;
        }
    }

    public override void Exit(HouseController obj)
    {
    }
}
