using UnityEngine;
using System.Collections;

public enum EDragonMovement
{
    MOVE_TOUCH,
    MOVE_TO_ENEMY,
    MOVE_TO_HOUSE,
}

public class DragonStateMove : FSMState<DragonController>
{
    EDragonStateDirection preStateDirection;

    public Vector3 destPosition { get; set; }
    public GameObject destFrag { get; set; }
    public GameObject preFrag { get; set; }
    public EDragonMovement Movement { get; set; }
    public EDragonStateDirection attackDirection { get; set; }

    public System.Collections.Generic.List<Vector3> listPosition = new System.Collections.Generic.List<Vector3>();
    static int countList = 0;

    public override void Enter(DragonController obj)
    {
        preStateDirection = obj.StateDirection;
        Movement = EDragonMovement.MOVE_TOUCH;
    }

    public override void Execute(DragonController obj)
    {
        if (Movement == EDragonMovement.MOVE_TOUCH)
        {
            if (preFrag != destFrag)
            {
                preFrag = destFrag;
                PlayTouchManager.Instance.checkFrag(destFrag, false);
            }

            if (Vector3.Distance(obj.transform.position, destPosition) <= PlayConfig.DistanceBulletChase)
            {
                obj.StateAction = EDragonStateAction.IDLE;
                PlayTouchManager.Instance.checkFrag(destFrag, true);
            }
        }
        else if (Movement == EDragonMovement.MOVE_TO_ENEMY)
        {
            GameObject enemy = obj.stateAttack.target;
            BoxCollider boxCollider = obj.stateAttack.target.GetComponentInChildren<BoxCollider>();
            Vector3 vec3 = new Vector3(boxCollider.size.x / 2, 0, 0);
            destPosition = new Vector3(enemy.transform.position.x + (attackDirection == EDragonStateDirection.LEFT ? -vec3.x : vec3.x) * PlayManager.Instance.tempInit.uiRoot.transform.localScale.x,
                                               enemy.transform.position.y - vec3.y * PlayManager.Instance.tempInit.uiRoot.transform.localScale.y,
                                               enemy.transform.position.z);

            if (Vector3.Distance(obj.transform.position, destPosition) <= 0.1f)
                obj.StateAction = EDragonStateAction.ATTACK;
        }
        else if (Movement == EDragonMovement.MOVE_TO_HOUSE)
        {
            if (preFrag != destFrag)
            {
                preFrag = destFrag;
                PlayTouchManager.Instance.checkFrag(destFrag, false);
            }

            if (Vector3.Distance(obj.transform.position, destPosition) <= PlayConfig.DistanceBulletChase)
            {
                obj.StateAction = EDragonStateAction.IDLE;
                PlayDragonManager.Instance.copulateIn();
            }
        }

        if (obj.transform.position != destPosition)
        {
            if (Time.timeScale != 0.0f)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, destPosition, obj.attribute.Speed / 1000);

                if (PlayDragonManager.Instance.countBaby > 0)
                {
                    listPosition.Add(obj.transform.position);

                    if (countList >= 100)
                    {
                        listPosition.RemoveAt(0);
                    }
                    else
                        countList++;
                }
            }
        }

        if (preStateDirection != obj.StateDirection)
        {
            preStateDirection = obj.StateDirection;

            if (obj.StateDirection == EDragonStateDirection.LEFT)
            {
                Vector3 scale = obj.transform.GetChild(0).localScale;
                obj.transform.GetChild(0).localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
            }
            else
            {
                Vector3 scale = obj.transform.GetChild(0).localScale;
                obj.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
    }

    public override void Exit(DragonController obj)
    {
    }
}

