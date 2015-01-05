using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationFrames))]
public class LevelSelectDragonAnimation : MonoBehaviour 
{
    AnimationFrames animationFrames;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
    }

    public void changeResources(EDragonStateAction stateAction)
    {
        string dataBranch = PlayerInfo.Instance.dragonInfo.id;
        string branch = char.ToUpper(dataBranch[0]) + dataBranch.Substring(1, dataBranch.Length - 1).ToLower();

        switch (stateAction)
        {
            case EDragonStateAction.IDLE:
                animationFrames.createAnimation("Image/Dragon/Player/" + branch + "/Idle", 0.125f, true);
                break;
            case EDragonStateAction.MOVE:
                animationFrames.createAnimation("Image/Dragon/Player/" + branch + "/Move", 0.125f, true);
                break;
            case EDragonStateAction.ATTACK:
                animationFrames.createAnimation("Image/Dragon/Player/" + branch + "/Attack", 0.125f, true);
                break;
            case EDragonStateAction.DIE:
                animationFrames.createAnimation("Image/Dragon/Player/" + branch + "/Die", 0.3f, true);
                break;
        }
    }
}
