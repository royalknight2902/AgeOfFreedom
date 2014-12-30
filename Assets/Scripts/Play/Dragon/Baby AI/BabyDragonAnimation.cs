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
        switch (stateAction)
        {
            case EDragonStateAction.IDLE:
                animationFrames.createAnimation(EDragonStateAction.IDLE, "Image/Dragon/Baby/Idle", 0.125f, true);
                break;
            case EDragonStateAction.MOVE:
                animationFrames.createAnimation(EDragonStateAction.MOVE, "Image/Dragon/Baby/Move", 0.125f, true);
                break;
            case EDragonStateAction.ATTACK:
                animationFrames.createAnimation(EDragonStateAction.ATTACK, "Image/Dragon/Baby/Attack", 0.3f, true);
                break;
            case EDragonStateAction.DIE:
                animationFrames.createAnimation(EDragonStateAction.DIE, "Image/Dragon/Baby/Die", 0.3f, true);
                animationFrames.addEventLastKey(new EventDelegate(controller.stateDie.fadeOutSprites), false);
                break;
        }
    }
}
