using UnityEngine;
using System.Collections;

public class BabyDragonStateDie : FSMState<BabyDragonController> 
{
    const float DURATION = 2.0f;
    BabyDragonController controller;

    public override void Enter(BabyDragonController obj)
    {
        controller = obj;
        PlayDragonManager.Instance.countBaby--;
        PlayDragonManager.Instance.listBabyDragon.Remove(obj.gameObject);
        obj.StartCoroutine(obj.GetComponentInChildren<AutoDestroy>().destroyParent(DURATION));
    }

    public override void Execute(BabyDragonController obj)
    {
    }

    public override void Exit(BabyDragonController obj)
    {
    }

    public void fadeOutSprites()
    {
        //Animation
        EffectSupportor.Instance.fadeOutAndDestroy(controller.transform.GetChild(0).gameObject,
                                                    ESpriteType.SPRITE_RENDERER, DURATION);

        //HP Background
        EffectSupportor.Instance.fadeOutAndDestroy(controller.sliderHP.transform.GetChild(0).gameObject,
                                                    ESpriteType.UI_SPRITE, DURATION);

        //HP Foreground
        EffectSupportor.Instance.fadeOutAndDestroy(controller.sliderHP.transform.GetChild(1).gameObject,
                                                    ESpriteType.UI_SPRITE, DURATION);
    }
}
