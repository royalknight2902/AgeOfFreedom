using UnityEngine;
using System.Collections;

public class DragonStateAttack : FSMState<DragonController>
{
    public DragonController controller;
	public GameObject target;
    public EDragonStateDirection direction;

    EDragonStateDirection preDirection;

	public override void Enter (DragonController obj)
	{
        controller = obj;

        if (target.transform.position.x >= controller.transform.position.x)
            controller.stateAttack.direction = EDragonStateDirection.LEFT;
        else
            controller.stateAttack.direction = EDragonStateDirection.RIGHT;

        preDirection = direction;
        setDirection();
	}
	
	public override void Execute (DragonController obj)
	{
        if (target.transform.position.x >= controller.transform.position.x && direction == EDragonStateDirection.LEFT)
        {
            direction = EDragonStateDirection.RIGHT;

            if (preDirection != direction)
                setDirection();
        }
        else if (target.transform.position.x < controller.transform.position.x && direction == EDragonStateDirection.RIGHT)
        {
            direction = EDragonStateDirection.LEFT;

            if (preDirection != direction)
                setDirection();
        }
	}
	
	public override void Exit (DragonController obj)
	{

	}

    public void attackEnemy()
    {
        EnemyController enemyController = target.GetComponent<EnemyController>();

        int dmg = PlayManager.Instance.pushDamagePhysics(controller.attribute.ATK.Min,
                                                         controller.attribute.ATK.Max,
                                                         enemyController.attribute.DEF);
        enemyController.attribute.HP.Current -= dmg;
        if (enemyController.attribute.HP.Current < 0)
            enemyController.attribute.HP.Current = 0;

        float valueTo = enemyController.attribute.HP.Current / (float)enemyController.attribute.HP.Max;
        EffectSupportor.Instance.runSliderValue(enemyController.sliderHP, valueTo);

        if (enemyController.attribute.HP.Current <= 0)
        {
            enemyController.StateAction = EEnemyStateAction.DIE;
            controller.StateAction = EDragonStateAction.IDLE;
            target = null;
        }
    }

    void setDirection()
    {
        Vector3 scale = controller.transform.GetChild(0).localScale;

        if (direction == EDragonStateDirection.RIGHT)
            controller.transform.GetChild(0).localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
        else
            controller.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);

        preDirection = direction;
    }
}