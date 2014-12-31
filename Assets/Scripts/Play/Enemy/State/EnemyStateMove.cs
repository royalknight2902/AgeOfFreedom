using UnityEngine;
using System.Collections;

public enum EEnemyMovement
{
	MOVE_ON_PATHS,
	MOVE_TO_DRAGON,
}

public class EnemyStateMove : FSMState<EnemyController>
{
	public Transform PathGroup;
	public int iCurrentPath = 0;
	public Transform[] Paths;
	public EEnemyMovement State;
	public EDragonStateDirection Direction;

	float distance;
	float fDistFromPath = 0.5f;
	float fJourneyLength;

	public override void Enter (EnemyController obj)
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
		
		Vector3 scale = obj.transform.GetChild(0).localScale;
		obj.transform.GetChild(0).localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);

		State = EEnemyMovement.MOVE_ON_PATHS;
		Direction = EDragonStateDirection.LEFT;
	}

	public override void Execute (EnemyController obj)
	{
		if (!obj.isDie)
		{
			if(State == EEnemyMovement.MOVE_ON_PATHS)
			{
				getSteer(obj);
                checkDireciotn(obj);
				moveEnemy(obj);
			}
			else if(State == EEnemyMovement.MOVE_TO_DRAGON)
			{
				GameObject dragon = obj.stateAttack.target;
				BoxCollider test = obj.stateAttack.target.GetComponent<BoxCollider>();
				Vector3 vec3 = new Vector3(test.size.x / 2, 0, 0);
				Vector3 realPosition = new Vector3(dragon.transform.position.x + ( Direction == EDragonStateDirection.LEFT ? - vec3.x : vec3.x) * PlayManager.Instance.tempInit.uiRoot.transform.localScale.x, 
				                                   dragon.transform.position.y - vec3.y * PlayManager.Instance.tempInit.uiRoot.transform.localScale.y,
				                                   dragon.transform.position.z);
				moveToTarget(obj, realPosition);

				if(Vector3.Distance(obj.transform.position, realPosition) <= 0.1f)
					obj.StateAction = EEnemyStateAction.ATTACK;
			}
		}
	}
	
	public override void Exit (EnemyController obj)
	{

	}

    void checkDireciotn(EnemyController controller)
    {
        if (iCurrentPath >= Paths.Length)
        {
            EDirection pathDirection = Paths[iCurrentPath - 1].GetComponent<PathController>().Direction;
            if (pathDirection != controller.StateDirection)
                controller.StateDirection = pathDirection;
        }
        else
        {
            EDirection pathDirection = Paths[iCurrentPath].GetComponent<PathController>().Direction;
            if (pathDirection != controller.StateDirection)
                controller.StateDirection = pathDirection;
        }
    }

	void getSteer(EnemyController controller)
	{
		Vector3 steerVector = controller.transform.InverseTransformPoint(new Vector3(Paths[iCurrentPath].position.x,
		                                                                             controller.transform.position.y, 
		                                                                             Paths[iCurrentPath].position.z));
		//float newSteer = fMaxSteer / 60f * (steerVector.x / steerVector.magnitude);
		if (steerVector.magnitude <= fDistFromPath)
			iCurrentPath++;
		// delete enemy when go to end path
		if (iCurrentPath >= Paths.Length)
		{
            Debug.Log(Paths.Length);
            Debug.Log(iCurrentPath);

			if(SceneState.Instance.State != ESceneState.ADVENTURE)
			{
				MonoBehaviour.Destroy(controller.gameObject);
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
				WaveController.Instance.checkDiamond(controller.waveID, PlayManager.Instance.heartEffectPosition, false);
				WaveController.Instance.showEffectHeart();
				
				if (WaveController.Instance.enemy_current <= 0)
				{
					PlayManager.Instance.showVictory();
				}
			}
			MonoBehaviour.Destroy(controller.gameObject);
		}
	}
	
	void moveEnemy(EnemyController controller)
	{
		if (iCurrentPath < Paths.Length)
		{
			fJourneyLength = Vector3.Distance(controller.transform.position, Paths[iCurrentPath].position);
			float distCovered = Time.deltaTime * controller.speed / 60f;
			float fracJourney = distCovered / fJourneyLength;
			controller.transform.position = Vector3.Lerp(controller.transform.position, Paths[iCurrentPath].position, fracJourney);
		}
	}

	void moveToTarget(EnemyController controller, Vector3 targetPosition)
	{
		fJourneyLength = Vector3.Distance(controller.transform.position, targetPosition);
		float distCovered = Time.deltaTime * controller.speed / 60f;
		float fracJourney = distCovered / fJourneyLength;
		controller.transform.position = Vector3.Lerp(controller.transform.position, targetPosition, fracJourney);
	}
}