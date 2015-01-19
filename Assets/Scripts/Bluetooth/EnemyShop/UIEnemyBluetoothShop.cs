using UnityEngine;
using System.Collections;

public enum EEnemyBluetoothState
{ 
	Normal,
	Checked,
}

public class UIEnemyBluetoothShop : MonoBehaviour {
	

	public EEnemyBluetoothState enemyBluetoothState;

	bool isClicked;
//	DragonItemAction dragonItemAction;
	EnemyBluetoothController enemyBluetoothController;
	
	void Awake()
	{
		//dragonItemAction = GetComponent<DragonItemAction>();
		enemyBluetoothController = GetComponent<EnemyBluetoothController>();

	}

	void OnClick()
	{

		if (!isClicked) 
		{
			isClicked = true;
//			GameObject goChecked = Instantiate( Resources.Load("Prefab/Play/EnemyBluetoothCheck")) as GameObject;
//			goChecked.transform.parent = this.gameObject.transform;
//			goChecked.transform.localScale = Vector2.one;
//			goChecked.transform.localPosition = Vector2.one;
//			goChecked.transform.GetComponent<UIWidget>().SetAnchor(this.gameObject);
//			goChecked.SetActive(true);
			this.enemyBluetoothState = EEnemyBluetoothState.Checked;
		}
		else 
		{
			initToGame();


			this.enemyBluetoothState = EEnemyBluetoothState.Normal;	
			isClicked = false;
		}
		
	}

	void initToGame()
	{


		GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
		EnemyController enemyController = model.GetComponent<EnemyController>(); 

		//except money
		GameSupportor.transferEnemyData(enemyController, ReadDatabase.Instance.EnemyInfo[this.enemyBluetoothController.ID]);
		if (PlayInfo.Instance.Money < enemyController.money)
						return;


		PlayInfo.Instance.Money -= enemyController.money;
		int routine = Random.Range(0, WaveController.Instance.enemyRoutine.Length);
	
		GameObject enemy = Instantiate (model, WaveController.Instance.enemyStartPos [routine].transform.position, Quaternion.identity) as GameObject;
		checkVisibleEnemy(enemyController);
		enemy.transform.parent = WaveController.Instance.enemyStartPos [routine].transform;
		enemy.transform.localScale = Vector3.one;
		enemy.transform.localPosition = Vector3.zero;
		//enemy.GetComponentInChildren<SpriteRenderer> ().material.renderQueue = GameConfig.RenderQueueEnemy - ;
		
		EnemyController ec = enemy.GetComponent<EnemyController> ();
		ec.stateMove.PathGroup = WaveController.Instance.enemyRoutine [routine].transform;
	
		//Debug.Log (WaveController.Instance.enemyRoutine [routine]);
		//Set depth cho thanh hp, xu ly thanh mau xuat hien sau phai? ve~ sau
		foreach (Transform health in enemy.transform) 
		{
			if (health.name == PlayNameHashIDs.Health) 
			{
				foreach (Transform child in health)
				{
					if (child.name == PlayNameHashIDs.Foreground)
							child.GetComponent<UISprite> ().depth -= 1;
				else if (child.name == PlayNameHashIDs.Background)
						child.GetComponent<UISprite> ().depth -= (1 + 1);
					}
					break;
				}
		}

	
	}
	public void checkVisibleEnemy(EnemyController enemyController)
	{
		if (!PlayerInfo.Instance.listEnemy[enemyController.ID])
		{
			PlayerInfo.Instance.addEnemy(enemyController.ID);
			GuideController.hasUpdateEnemy = true;
			PlayManager.Instance.showTutorialNewEnemy(enemyController);
		}
	}
}