using UnityEngine;
using System.Collections;

public class DragonStateDie : FSMState<DragonController>
{
	const float DURATION = 2.0f;
	DragonController controller;

	public override void Enter (DragonController obj)
	{
		controller = obj;
		//obj.StartCoroutine(obj.GetComponentInChildren<AutoDestroy>().destroyParent(DURATION));
	}

	public override void Execute (DragonController obj)
	{

	}

	public override void Exit (DragonController obj)
	{

	}

	public void fadeOutSprites()
	{
		//Animation
		EffectSupportor.Instance.fadeOutAndDestroy (controller.transform.GetChild (0).gameObject, 
		                                            ESpriteType.SPRITE_RENDERER, DURATION);
		
		//HP Background
		EffectSupportor.Instance.fadeOutAndDestroy (controller.sliderHP.transform.GetChild (0).gameObject,
		                                            ESpriteType.UI_SPRITE, DURATION);
		
		//HP Foreground
		EffectSupportor.Instance.fadeOutAndDestroy (controller.sliderHP.transform.GetChild (1).gameObject,
		                                            ESpriteType.UI_SPRITE, DURATION);
	}
}

