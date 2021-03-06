using UnityEngine;
using System.Collections;

public class EnemyStateAttack : FSMState<EnemyController>
{
	public EnemyController controller;
	public GameObject target;
	public EDragonStateDirection direction;

    Vector3 preTargetPosition;
	EDragonStateDirection preDirection;

	public override void Enter (EnemyController obj)
	{
		controller = obj;

		if(target.transform.position.x >= controller.transform.position.x)
			controller.stateAttack.direction = EDragonStateDirection.RIGHT;
		else
			controller.stateAttack.direction = EDragonStateDirection.LEFT;

		preDirection = direction;
        preTargetPosition = target.transform.position;
		setDirection ();
	}

	public override void Execute (EnemyController obj)
	{
        if (!obj.isEnable)
            return;

        if (target == null)
        {
            controller.stateMove.State = EEnemyMovement.MOVE_ON_PATHS;
            controller.StateAction = EEnemyStateAction.MOVE;
            return;
        }
        else
        {
            if (target.GetComponent<DragonController>() != null)
            {
                DragonController dragonController = target.GetComponent<DragonController>();
                if (dragonController.attribute.HP.Current <= 0)
                {
                    dragonController.StateAction = EDragonStateAction.DIE;
                    dragonController.StateOffense = EDragonStateOffense.NONE;
                    controller.StateAction = EEnemyStateAction.MOVE;
                    target = null;
                    return;
                }
            }
            else
            {
                BabyDragonController babyController = target.GetComponent<BabyDragonController>();
                if (babyController.attribute.HP.Current <= 0)
                {
                    babyController.StateAction = EDragonStateAction.DIE;

                    controller.enemyAttack.listDragon.Remove(target);
                    target = null;
                    controller.enemyAttack.chooseDragonToAttack();
                    return;
                }
            }
        }

		if (target.transform.position.x >= controller.transform.position.x && direction == EDragonStateDirection.LEFT)
		{
			direction = EDragonStateDirection.RIGHT;

			if(preDirection != direction)
				setDirection();
		}
		else if(target.transform.position.x < controller.transform.position.x && direction == EDragonStateDirection.RIGHT)
		{
			direction = EDragonStateDirection.LEFT;

			if(preDirection != direction)
				setDirection();
		}

        if(preTargetPosition != target.transform.position)
        {
            if (target.transform.position.x < controller.transform.position.x)
                controller.stateMove.Direction = EDragonStateDirection.RIGHT;
            else
                controller.stateMove.Direction = EDragonStateDirection.LEFT;

            controller.stateMove.State = EEnemyMovement.MOVE_TO_DRAGON;
            controller.StateAction = EEnemyStateAction.MOVE;
        }
	}

	public override void Exit (EnemyController obj)
	{
	}

	public void attackDragon()
	{
        if (target == null)
        {
            return;
        }
        
		DragonController dragonController = target.GetComponent<DragonController> ();

		int dmg = PlayManager.Instance.pushDamagePhysics (controller.attribute.ATK.Min, 
		                                                 controller.attribute.ATK.Max,
		                                                 dragonController.attribute.DEF);
		dragonController.attribute.HP.Current -= dmg;

        //show collision
        PlayDragonManager.Instance.showEnemyAttackCollision(target);

		if(dragonController.attribute.HP.Current < 0)
			dragonController.attribute.HP.Current = 0;

		float valueTo = dragonController.attribute.HP.Current / (float)dragonController.attribute.HP.Max;

		dragonController.updateTextHP();

        EffectSupportor.Instance.runSliderValue(dragonController.sliderHP, valueTo, EffectSupportor.TimeValueRunHP);
        EffectSupportor.Instance.runSliderValue(PlayDragonInfoController.Instance.sliderHP, valueTo, EffectSupportor.TimeValueRunHP);
	}

    public void attackDragonBaby()
    {
        if (target == null)
        {
            return;
        }
        BabyDragonController babyController = target.GetComponent<BabyDragonController>();

        if (babyController != null)
        {
            int dmg = PlayManager.Instance.pushDamagePhysics(controller.attribute.ATK.Min,
                                                         controller.attribute.ATK.Max,
                                                         babyController.attribute.DEF);
            babyController.attribute.HP.Current -= dmg;
            if (babyController.attribute.HP.Current < 0)
                babyController.attribute.HP.Current = 0;

            float valueTo = babyController.attribute.HP.Current / (float)babyController.attribute.HP.Max;

            babyController.updateTextHP();

            EffectSupportor.Instance.runSliderValue(babyController.sliderHP, valueTo, EffectSupportor.TimeValueRunHP);
        }
    }

	void setDirection()
	{
		Vector3 scale = controller.transform.GetChild(0).localScale;

		if(direction == EDragonStateDirection.RIGHT)
			controller.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
		else
			controller.transform.GetChild(0).localScale = new Vector3(-1 * scale.x, scale.y, scale.z);

		preDirection = direction;
	}
}