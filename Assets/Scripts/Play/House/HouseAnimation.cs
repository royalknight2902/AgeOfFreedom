using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationFrames))]
public class HouseAnimation : MonoBehaviour
{
    HouseController controller;
    AnimationFrames animationFrames;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<HouseController>();
    }

    public void changeResources(EHouseStateAction stateAction)
    {
        switch (stateAction)
        {
            case EHouseStateAction.BUILD:
                animationFrames.createAnimation(EHouseStateAction.BUILD, "Image/House/Build", 1.0f, false);
                break;
            case EHouseStateAction.IDLE:
                animationFrames.createAnimation(EHouseStateAction.IDLE, "Image/House/LV " + controller.ID.Level + "/Idle", 1.0f, true);
                break;
            case EHouseStateAction.OPEN:
                animationFrames.createAnimation(EHouseStateAction.OPEN, "Image/House/LV " + controller.ID.Level + "/Open", 0.5f, false);
                break;
            case EHouseStateAction.CLOSE:
                animationFrames.createAnimation(EHouseStateAction.CLOSE, "Image/House/LV " + controller.ID.Level + "/Close", 0.5f, false);
                animationFrames.addEventLastKey(new EventDelegate(changeStateIdle), false);
                break;
            case EHouseStateAction.DESTROY:
                animationFrames.createAnimation(EHouseStateAction.DESTROY, "Image/House/Destroy", 7.0f / 60, false);
                animationFrames.addEventLastKey(new EventDelegate(controller.stateDestroy.fadeOutSprites), false);
                break;
        }
    }

    public void changeStateIdle()
    {
        controller.StateAction = EHouseStateAction.IDLE;
    }

    public void changeStateClose()
    {
        controller.StateAction = EHouseStateAction.CLOSE;
    }

    public void addEventForCurrentState(object[] key, EventDelegate callback, bool runOnlyOnce)
    {
        animationFrames.addEvent(key, callback, runOnlyOnce);
    }

    public void addEventLastKeyForCurrentState(EventDelegate callback, bool runOnlyOnce)
    {
        animationFrames.addEventLastKey(callback, runOnlyOnce);
    }
}
