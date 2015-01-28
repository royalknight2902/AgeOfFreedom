using UnityEngine;
using System.Collections;

public class SkillStateOnce : SkillState
{
    public GameObject enemy;
    public Vector3 destPosition;

    public override void Enter(SkillController obj)
    {
        base.Enter(obj);

        if (obj.Type == ESkillType.TARGET)
        {
            if (enemy != null)
            {
                if ((ESkillOffense)obj.Ability == ESkillOffense.SINGLE)
                {
                    obj.transform.position = enemy.transform.position;

                    SkillState skill = obj.getCurrentState();
                    if (skill.hasDamage || skill.effectType != EBulletEffect.NONE)
                    {
                        if (obj.skillAnimation.GetComponent<SkillDamage>() == null)
                        {
                            SkillDamage dmg = obj.skillAnimation.gameObject.AddComponent<SkillDamage>();
                            dmg.damage(enemy);
                        }
                    }
                }
                else
                    obj.transform.position = destPosition;
            }
            else
                obj.transform.position = destPosition;
        }
    }

    public override void Execute(SkillController obj)
    {
        base.Execute(obj);
    }

    public override void Exit(SkillController obj)
    {
        base.Exit(obj);
    }
}
