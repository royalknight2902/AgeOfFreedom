using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationFrames))]
public class DragonAnimation : MonoBehaviour
{
    AnimationFrames animationFrames;
    DragonController controller;

    void OnEnable()
    {
        animationFrames = GetComponent<AnimationFrames>();
        controller = transform.parent.GetComponent<DragonController>();
    }

    public void changeResources(EDragonStateAction stateAction)
    {
        switch (stateAction)
        {
            case EDragonStateAction.IDLE:
                animationFrames.createAnimation(EDragonStateAction.IDLE, "Image/Dragon/Player/Idle", 0.125f, true);
                break;
            case EDragonStateAction.MOVE:
                animationFrames.createAnimation(EDragonStateAction.MOVE, "Image/Dragon/Player/Move", 0.125f, true);
                break;
            case EDragonStateAction.ATTACK:
                animationFrames.createAnimation(EDragonStateAction.ATTACK, "Image/Dragon/Player/Attack", 0.125f, true);
                animationFrames.addEvent(new object[] { 4 }, new EventDelegate(controller.stateAttack.attackEnemy), false);
                break;
            case EDragonStateAction.DIE:
                animationFrames.createAnimation(EDragonStateAction.DIE, "Image/Dragon/Player/Die", 0.3f, true);
                animationFrames.addEventLastKey(new EventDelegate(controller.stateDie.fadeOutSprites), false);
                break;
        }
    }
}