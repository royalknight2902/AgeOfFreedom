using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovement : MonoBehaviour
{
    public EnemyController enemyController;

    //-----------------------------------------------VARIABLE PUBLIC HIDE------------------------------//
    [HideInInspector]
    public Transform PathGroup;
    [HideInInspector]
    public int iCurrentPath = 0;
    [HideInInspector]
    public Transform[] Paths;
    //------------------------------------------------ VARIABLE PRIVATE----------------------------//
    float distance;
    //private float fMaxSteer = 15.0f;
    float fDistFromPath = 0.5f;
    float fJourneyLength;

    void Start()
    {
        getPath();
    }

    void Update()
    {
        if (!enemyController.isDie)
        {
            getSteer();
            moveEnemy();
        }
    }

    private void getPath()
    {
        Transform[] temps = PathGroup.GetComponentsInChildren<Transform>();
        Paths = new Transform[temps.Length - 1];
        int count = 0;
        for (int i = 0; i < temps.Length; i++)
        {
            if (temps[i] != PathGroup)
            {
                Paths[count] = temps[i];
                count++;
            }
        }
    }

    private void getSteer()
    {
        Vector3 steerVector = transform.InverseTransformPoint(new Vector3(Paths[iCurrentPath].position.x,
            transform.position.y, Paths[iCurrentPath].position.z));
        //float newSteer = fMaxSteer / 60f * (steerVector.x / steerVector.magnitude);
        if (steerVector.magnitude <= fDistFromPath)
            iCurrentPath++;
        // delete enemy when go to end path
        if (iCurrentPath >= Paths.Length)
        {
			if(SceneState.Instance.State != ESceneState.ADVENTURE)
			{
				Destroy(gameObject);
				return;
			}

            WaveController.Instance.enemy_current--;
    		PlayInfo.Instance.Heart--;

            if (PlayInfo.Instance.Heart <= 0)
            {
                PlayManager.Instance.showGameOver();
            }
            else
            {
                WaveController.Instance.checkDiamond(enemyController.waveID, PlayManager.Instance.heartEffectPosition, false);
                WaveController.Instance.showEffectHeart();

                if (WaveController.Instance.enemy_current <= 0)
                {
                    PlayManager.Instance.showVictory();
                }
            }
			Destroy(gameObject);
        }
    }

    private void moveEnemy()
    {
        if (iCurrentPath < Paths.Length)
        {
            fJourneyLength = Vector3.Distance(transform.position, Paths[iCurrentPath].position);
            float distCovered = Time.deltaTime * enemyController.speed / 60f;
            float fracJourney = distCovered / fJourneyLength;
            transform.position = Vector3.Lerp(transform.position, Paths[iCurrentPath].position, fracJourney);
        }
    }
}
