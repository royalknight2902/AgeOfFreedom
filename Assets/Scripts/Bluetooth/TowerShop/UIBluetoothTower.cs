using UnityEngine;
using System.Collections;

public enum EBluetoothTower
{
	DECREASE_LEVEL,
	DESTROY,
}
public class UIBluetoothTower : MonoBehaviour
{
	public EBluetoothTower eBluetoothTower;

	float CooldownTime;
	bool isEnable;
	BluetoothTowerController bluetoothTowerController;
	void Awake()
	{
		//dragonItemAction = GetComponent<DragonItemAction>();
		bluetoothTowerController = GetComponentInParent<BluetoothTowerController>();
		isEnable = true;
		CooldownTime = GameConfig.TowerShopBluetoothCooldownTime;
	}
	
	void OnClick()
	{
		if (isEnable) {
						GameObject goChecked = bluetoothTowerController.CheckOK;
						if (goChecked.transform.parent != this.gameObject.transform) {
								goChecked.transform.parent = this.gameObject.transform;
								goChecked.transform.localScale = Vector2.one;
								goChecked.transform.localPosition = Vector2.one;
								goChecked.transform.GetComponent<UIAnchor> ().container = this.gameObject;

								TweenScale scale = goChecked.GetComponent<TweenScale> ();
								scale.enabled = true;
			
								scale.PlayForward ();

								TweenAlpha alpha = goChecked.GetComponent<TweenAlpha> ();
								alpha.enabled = true;
			
								alpha.PlayForward ();
								goChecked.SetActive (true);
						} else {
						
								if (eBluetoothTower == EBluetoothTower.DESTROY)
				{
										bluetoothTowerController.DestroyRandomTower ();
					PlayInfo.Instance.Money -= GameConfig.DestroyTowerBluetoothCost;
				}
								else
				{
										bluetoothTowerController.DecreaseLevelRandomTower ();
					PlayInfo.Instance.Money -= GameConfig.DecreaseTowerLevelBluetoothCost;
				}
								goChecked.SetActive (false);
								goChecked.GetComponent<TweenScale> ().PlayReverse ();
								goChecked.GetComponent<TweenAlpha> ().PlayReverse ();
								goChecked.transform.parent = this.transform.parent;
						
								this.transform.FindChild ("Cooldown").GetComponent<UISprite> ().fillAmount = 1;
				isEnable = false;
				StartCoroutine(runCooldown());
						}
				}
	}

	public IEnumerator runCooldown()
	{
		float valueEachTime = Time.fixedDeltaTime / (CooldownTime / PlayerInfo.Instance.userInfo.timeScale);
		UISprite cooldown = this.transform.FindChild ("Cooldown").GetComponent<UISprite> ();
		while (true)
		{
			if (Time.timeScale != 0.0f)
			{
				cooldown.fillAmount -= valueEachTime;
				if (cooldown.fillAmount <= 0.0f)
				{
					//cooldown.gameObject.SetActive(false);
					isEnable = true;
					yield break;
				}
			}
			yield return 0;
		}
	}
}