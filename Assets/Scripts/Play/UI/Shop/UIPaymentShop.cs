using UnityEngine;
using System.Collections;

public enum EPaymentShopButton
{
	DIAMOND,
	MONEY,
	CANCEL,
	CANCEL_ITEM_PAYMENT,
}

public class UIPaymentShop : MonoBehaviour
{
	public EPaymentShopButton type;

	void OnClick()
	{
		switch (type)
		{
			case EPaymentShopButton.CANCEL:
				ShopController.Instance.paymentTowerPanel.SetActive(false);
				break;
			case EPaymentShopButton.DIAMOND:
				PlayManager.Instance.buyTower(ShopController.Instance.paymentTowerPanel.GetComponent<PaymentController>().target, false);
				break;
			case EPaymentShopButton.MONEY:
				PlayManager.Instance.buyTower(ShopController.Instance.paymentTowerPanel.GetComponent<PaymentController>().target, true);
				break;
		case EPaymentShopButton.CANCEL_ITEM_PAYMENT:
			ShopController.Instance.paymentItemPanel.SetActive(false);
			break;
		}
	}
}
