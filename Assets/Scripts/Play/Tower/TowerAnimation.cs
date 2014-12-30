using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AutoTower))]
public class TowerAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void building(float timeBuild)
    {
    }

    public void idle()
    {

        animator.SetBool(TowerHashIDs.STATE_SHOOT, false);
        animator.SetBool(TowerHashIDs.STATE_BUILDING, false);
        animator.SetBool(TowerHashIDs.STATE_IDLE, true);
        Transform[] all = transform.parent.GetComponentsInChildren<Transform>();
    }

    public void shoot()
    {
        animator.SetBool(TowerHashIDs.STATE_BUILDING, false);
        animator.SetBool(TowerHashIDs.STATE_IDLE, false);
        animator.SetBool(TowerHashIDs.STATE_SHOOT, true);
    }

    public void sell(GameObject tower)
    {
        GetComponent<AutoTower>().Tower = tower;
        animator.SetBool(TowerHashIDs.STATE_BUILDING, false);
        animator.SetBool(TowerHashIDs.STATE_IDLE, false);
        animator.SetBool(TowerHashIDs.STATE_SHOOT, false);
        animator.SetBool(TowerHashIDs.STATE_SELL, true);
    }
}