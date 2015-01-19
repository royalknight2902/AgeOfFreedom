using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationFrames))]
public class SkillAnimation : MonoBehaviour
{
    AnimationFrames animationFrames;
    SkillController controller;

    object currentState;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<SkillController>();
        currentState = null;
    }

    public void changeResources(ESkillAction stateAction)
    {
        currentState = stateAction;
        float timeFrame = (float)getValueFromDatabase(EAnimationDataType.TIME_FRAME);
        string resourcePath = getValueFromDatabase(EAnimationDataType.RESOURCE_PATH).ToString().Trim();
        object[] specificLoop = (object[])getValueFromDatabase(EAnimationDataType.SPECIFIC_LOOP);
        EventDelegate callback = null;

        switch (stateAction)
        {
            case ESkillAction.DROP:
                animationFrames.createAnimation(ESkillAction.DROP, "Image/Skill/" + resourcePath, timeFrame, true,
                    (bool)specificLoop[0], (int[])specificLoop[1]);
                break;
            case ESkillAction.TRAP:
                animationFrames.createAnimation(ESkillAction.TRAP, "Image/Skill/" + resourcePath, timeFrame, true,
                    (bool)specificLoop[0], (int[])specificLoop[1]);
                break;
            case ESkillAction.BUFF:
                animationFrames.createAnimation(ESkillAction.BUFF, "Image/Skill/" + resourcePath, timeFrame, true,
                    (bool)specificLoop[0], (int[])specificLoop[1]);
                break;
            case ESkillAction.ARMAGGEDDON:
                animationFrames.createAnimation(ESkillAction.ARMAGGEDDON, "Image/Skill/" + resourcePath, timeFrame, true,
                    (bool)specificLoop[0], (int[])specificLoop[1]);
                break;
            case ESkillAction.END:
                animationFrames.createAnimation(ESkillAction.END, "Image/Explosion/" + resourcePath, timeFrame, true,
                    (bool)specificLoop[0], (int[])specificLoop[1]);
                callback = new EventDelegate(((SkillStateEnd)controller.listState[ESkillAction.END]).destroy);
                break;
            case ESkillAction.FADE:
                break;
        }

        //set render queue
        if (stateAction == ESkillAction.END)
        {
            SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        }
        else
        {
            GetComponent<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueSkill;
        }

        object result = getValueFromDatabase(EAnimationDataType.EVENT);
        if (result != null)
        {
            object[] events = (object[])result;
            if (events.Length != 0)
            {
                animationFrames.addEvent(events, callback, false);
            }
        }
    }

    object getValueFromDatabase(EAnimationDataType type)
    {
        object result = null;
        if (type == EAnimationDataType.TIME_FRAME)
        {
            result = ReadDatabase.Instance.SkillInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].TimeFrame;
        }
        else if (type == EAnimationDataType.EVENT)
        {
            if (ReadDatabase.Instance.SkillInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].listKeyEventFrame.Count > 0)
            {
                System.Collections.Generic.List<object> listEvent = null;
                listEvent = ReadDatabase.Instance.SkillInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].listKeyEventFrame;

                int length = listEvent.Count;
                for (int i = 0; i < length; i++)
                {
                    if (listEvent[i].Equals("end"))
                        listEvent[i] = animationFrames.keyEnd;
                }

                result = listEvent.ToArray();
            }
        }
        else if (type == EAnimationDataType.RESOURCE_PATH)
        {
            string s = ReadDatabase.Instance.SkillInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].ResourcePath;
            if ((ESkillAction)currentState == ESkillAction.END) // Explosion texture
            {
                string[] arr = s.Split(' ');
                result = ConvertSupportor.convertUpperFirstChar(arr[0]) + "/" + arr[1];
            }
            else // Skill texture
                result = s;
        }
        else if (type == EAnimationDataType.SPECIFIC_LOOP)
        {
            bool boolResult = false;
            int[] arrResult = null;

            AnimationEventState eventState = ReadDatabase.Instance.SkillInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()];
            boolResult = eventState.isSpecificLoop;

            if (boolResult)
            {
                arrResult = new int[eventState.SpecificLoopIndex.Length];
                for (int i = 0; i < eventState.SpecificLoopIndex.Length; i++)
                {
                    arrResult[i] = eventState.SpecificLoopIndex[i];
                }
            }

            result = new object[] { boolResult, arrResult };
        }
        return result;
    }
}
