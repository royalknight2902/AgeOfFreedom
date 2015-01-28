using UnityEngine;
using System.Collections;

public class BluetoothTowerController : MonoBehaviour {
	public GameObject CheckOK;

	
	public string ID { get; set; }
	
	public void DestroyRandomTower()
	{
		PlayManager.Instance.DestroyRandomTower ();
	}
	public void DecreaseLevelRandomTower()
	{
		PlayManager.Instance.DecreaseLevelRandomTower();
	}
}
