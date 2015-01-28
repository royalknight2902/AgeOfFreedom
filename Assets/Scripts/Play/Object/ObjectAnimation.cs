using UnityEngine;
using System.Collections;

public class ObjectAnimation : MonoBehaviour {

    AnimationFrames animationFrames;
    ObjectController controller;

    object currentState;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<ObjectController>();
        currentState = null;
    }

    public void changeResources(EObjectState stateAction)
    {
        if(stateAction == EObjectState.DESTROY)
        {
            MonoBehaviour.Destroy(controller.gameObject);
            return;
        }

        currentState = stateAction;
        float timeFrame = (float)getValueFromDatabase(EAnimationDataType.TIME_FRAME);
        string resourcePath = getValueFromDatabase(EAnimationDataType.RESOURCE_PATH).ToString().Trim();
        EventDelegate callback = null;

        switch (stateAction)
        {
            case EObjectState.RUN:
                animationFrames.createAnimation(EObjectState.RUN, "Image/Object/" + resourcePath, timeFrame, false);
                break;
            case EObjectState.EXPLOSION:
                animationFrames.createAnimation(EObjectState.EXPLOSION, "Image/Explosion/" + resourcePath, timeFrame, false);
                callback = new EventDelegate(((ObjectStateExplosion)controller.listState[EObjectState.EXPLOSION]).destroy);
                break;
        }
    }

    public float getDuration()
    {
        return animationFrames.frameLength * animationFrames.timeFrame;
    }

    object getValueFromDatabase(EAnimationDataType type)
    {
        object result = null;
        if (type == EAnimationDataType.TIME_FRAME)
        {
            result = ReadDatabase.Instance.ObjectInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].TimeFrame;
        }
        else if (type == EAnimationDataType.EVENT)
        {
            if (ReadDatabase.Instance.ObjectInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].listKeyEventFrame.Count > 0)
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
            string s = ReadDatabase.Instance.ObjectInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()].ResourcePath;
            if ((EObjectState)currentState == EObjectState.EXPLOSION) // Explosion texture
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

            AnimationEventState eventState = ReadDatabase.Instance.ObjectInfo[controller.ID.ToUpper()].States[currentState.ToString().ToUpper()];
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
