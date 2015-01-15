using UnityEngine;
using System.Collections;

public class BabyDragonAnimation : MonoBehaviour 
{
    AnimationFrames animationFrames;
    BabyDragonController controller;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<BabyDragonController>();
    }

    public void changeResources(EDragonStateAction stateAction)
    {
        string branch = ConvertSupportor.convertUpperFirstChar(PlayerInfo.Instance.dragonInfo.id);
        float timeFrame = (float)getValueFromDatabase(EAnimationDataType.TIME_FRAME);
        EventDelegate callback = null;

        switch (stateAction)
        {
            case EDragonStateAction.IDLE:
                animationFrames.createAnimation(EDragonStateAction.IDLE, "Image/Dragon/Player/" + branch + "/Idle", timeFrame, true);
                break;
            case EDragonStateAction.MOVE:
                animationFrames.createAnimation(EDragonStateAction.MOVE, "Image/Dragon/Player/" + branch + "/Move", timeFrame, true);
                break;
            case EDragonStateAction.ATTACK:
                animationFrames.createAnimation(EDragonStateAction.ATTACK, "Image/Dragon/Player/" + branch + "/Attack", timeFrame, true);
                callback = new EventDelegate(controller.stateAttack.attackEnemy);
                break;
            case EDragonStateAction.DIE:
                animationFrames.createAnimation(EDragonStateAction.DIE, "Image/Dragon/Player/" + branch + "/Die", timeFrame, true);
                callback = new EventDelegate(controller.stateDie.fadeOutSprites);
                //animationFrames.addEventLastKey(new EventDelegate(controller.stateDie.fadeOutSprites), false);
                break;
        }

        object[] events = (object[])getValueFromDatabase(EAnimationDataType.EVENT);
        if (events.Length != 0)
        {
            animationFrames.addEvent(events, callback, false);
        }
    }

    object getValueFromDatabase(EAnimationDataType type)
    {
        object result = null;
        if (type == EAnimationDataType.TIME_FRAME)
        {
            result = ReadDatabase.Instance.DragonInfo.Player[PlayerInfo.Instance.dragonInfo.id].States[controller.StateAction.ToString().ToUpper()].TimeFrame;
        }
        else if (type == EAnimationDataType.EVENT)
        {
            System.Collections.Generic.List<object> listEvent = null;
            listEvent = ReadDatabase.Instance.DragonInfo.Player[PlayerInfo.Instance.dragonInfo.id].States[controller.StateAction.ToString().ToUpper()].listKeyEventFrame;

            int length = listEvent.Count;
            for (int i = 0; i < length; i++)
            {
                if (listEvent[i].Equals("end"))
                    listEvent[i] = animationFrames.keyEnd;
            }

            result = listEvent.ToArray();
        }
        return result;
    }
}
