using UnityEngine;
using System.Collections;

public enum EButtonShop
{
	TOWER,
	SKILL,
	CANCEL,
	EFFECT,
	CLICK,
}

public class UIShop : MonoBehaviour
{
	public EButtonShop type;

	[HideInInspector]
	public GameObject bulletEffect;

	void OnClick()
	{
		switch (type)
		{
			case EButtonShop.TOWER:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);

				PlayPanel.Instance.Shop.GetComponent<ShopController>().loadTower();
				break;
			case EButtonShop.SKILL:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);

				PlayPanel.Instance.Shop.GetComponent<ShopController>().loadItem();
				break;
			case EButtonShop.CANCEL:
				//ShopController.Instance.reset();
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);

				StartCoroutine(waitToCancel(0.2f));
				break;
			case EButtonShop.EFFECT:
				PlayPanel.Instance.Effect.SetActive(true);

				EffectPanelController controller = PlayPanel.Instance.Effect.GetComponentInChildren<EffectPanelController>();
				controller.icon.spriteName = this.GetComponent<UISprite>().spriteName;

				//Set color for effect panel
				BulletController bulletController = bulletEffect.GetComponent<BulletController>();

				controller.setColor(bulletController.effect);
				controller.setText(bulletController);
				break;
			case EButtonShop.CLICK:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);

				break;
		}
	}

#region WAITING
	IEnumerator waitToCancel(float time)
	{
		Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
		yield return new WaitForSeconds(time);
		PlayManager.Instance.isOnShop = false;
		PlayPanel.Instance.Shop.SetActive(false);
	}
#endregion
}
