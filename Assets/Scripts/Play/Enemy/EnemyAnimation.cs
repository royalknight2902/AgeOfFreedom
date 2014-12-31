using UnityEngine;
using System.Collections;

enum EAnimationDataType
{
    TIME_FRAME,
    EVENT,
}

[RequireComponent(typeof(AnimationFrames))]
public class EnemyAnimation : MonoBehaviour
{
    AnimationFrames animationFrames;
    EnemyController controller;

    object currentKey;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<EnemyController>();
        currentKey = null;
    }

    public void changeResources()
    {
        float timeFrame = (float)getValueFromDatabase(EAnimationDataType.TIME_FRAME);
        EventDelegate callback = null;

        switch (controller.StateAction)
        {
            case EEnemyStateAction.ATTACK:
                animationFrames.createAnimation(EEnemyStateAction.ATTACK, "Image/Enemy/" + controller.ID + "/Attack", timeFrame, true);
                if (controller.stateAttack.target.GetComponent<DragonController>() != null)
                {
                    callback = new EventDelegate(controller.stateAttack.attackDragon);
                }
                else if (controller.stateAttack.target.GetComponent<BabyDragonController>() != null)
                {
                    callback = new EventDelegate(controller.stateAttack.attackDragonBaby);
                }
                currentKey = controller.StateAction;
                break;
            case EEnemyStateAction.DIE:
                animationFrames.createAnimation(EEnemyStateAction.DIE, "Image/Enemy/" + controller.ID + "/Die", timeFrame, true);
                callback = new EventDelegate(GetComponent<AutoDestroy>().destroyParent);
                currentKey = controller.StateAction;
                break;
            case EEnemyStateAction.MOVE:
                switch (controller.StateDirection)
                {
                    case EDirection.BOTTOM:
                        animationFrames.createAnimation(EEnemyStateAction.MOVE.ToString() + "_" + EDirection.BOTTOM.ToString(), "Image/Enemy/" + controller.ID + "/Bottom", timeFrame, true);
                        break;
                    case EDirection.LEFT:
                        animationFrames.createAnimation(EEnemyStateAction.MOVE.ToString() + "_" + EDirection.LEFT.ToString(), "Image/Enemy/" + controller.ID + "/Left", timeFrame, true);
                        break;
                    case EDirection.RIGHT:
                        animationFrames.createAnimation(EEnemyStateAction.MOVE.ToString() + "_" + EDirection.RIGHT.ToString(), "Image/Enemy/" + controller.ID + "/Right", timeFrame, true);
                        break;
                    case EDirection.UP:
                        animationFrames.createAnimation(EEnemyStateAction.MOVE.ToString() + "_" + EDirection.UP.ToString(), "Image/Enemy/" + controller.ID + "/Up", timeFrame, true);
                        break;
                }
                currentKey = EEnemyStateAction.MOVE.ToString() + "_" + controller.StateDirection.ToString();
                break;
        }

        object[] events = (object[])getValueFromDatabase(EAnimationDataType.EVENT);
        if (events.Length != 0)
        {
            animationFrames.addEvent(events, callback, false);
        }
    }

    public bool checkTimeFrame()
    {
        if (animationFrames.listData[currentKey].TimeFrame == animationFrames.timeFrame)
            return true;
        return false;
    }

    public void setOriginTimeFrame()
    {
        animationFrames.timeFrame = animationFrames.listData[currentKey].TimeFrame;
    }

    public void setTimeFrame(float aspect)
    {
        animationFrames.timeFrame = animationFrames.listData[currentKey].TimeFrame * aspect;
    }

    object getValueFromDatabase(EAnimationDataType type)
    {
        object result = null;
        if (type == EAnimationDataType.TIME_FRAME)
        {
            if (controller.StateAction != EEnemyStateAction.MOVE)
                result = ReadDatabase.Instance.EnemyInfo[controller.ID].States[controller.StateAction.ToString().ToUpper()].TimeFrame;
            else // == MOVE
            {
                result = ReadDatabase.Instance.EnemyInfo[controller.ID].States[controller.StateDirection.ToString().ToUpper()].TimeFrame;
            }

        }
        else if (type == EAnimationDataType.EVENT)
        {
            System.Collections.Generic.List<object> listEvent = null;

            if (controller.StateAction != EEnemyStateAction.MOVE)
                listEvent = ReadDatabase.Instance.EnemyInfo[controller.ID].States[controller.StateAction.ToString().ToUpper()].listKeyEventFrame;
            else
                listEvent = ReadDatabase.Instance.EnemyInfo[controller.ID].States[controller.StateDirection.ToString().ToUpper()].listKeyEventFrame;

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