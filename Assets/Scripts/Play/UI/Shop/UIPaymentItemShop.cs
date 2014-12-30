using UnityEngine;
using System.Collections;

public enum EPaymentItemShop
{
	PACKAGE,
	CANCEL,
}

public class UIPaymentItemShop : MonoBehaviour 
{
	public UILabel labelDiamond;
	public UILabel labelGold;
	public EPaymentItemShop type;

	public GameObject parent { get; set; }
	public int wave {get; set;}

	PaymentItemController controller;

	void Start()
	{
		controller = ShopController.Instance.paymentItemPanel.GetComponent<PaymentItemController> ();
		parent = this.transform.parent.gameObject;
	}

	int diamond;
	public int Diamond
	{
		get
		{
			return diamond;
		}
		set
		{
			diamond = value;
			labelDiamond.text = diamond.ToString();
		}
	}

	int gold;
	public int Gold
	{
		get
		{
			return gold;
		}
		set
		{
			gold = value;
			labelGold.text = gold.ToString();
		}
	}

	public void OnClick()
	{
		switch (type)
		{
		case EPaymentItemShop.CANCEL:
			ShopController.Instance.paymentItemPanel.SetActive(false);
			break;
		case EPaymentItemShop.PACKAGE:
			if(controller.target == parent)
			{
				if(controller.paymentType == EPaymentType.NONE)
				{
					DeviceService.Instance.openToast("Please select the payment type :T");
					Debug.Log("Please select the payment type :T");
				}
				else if(controller.paymentType == EPaymentType.GOLD)
				{
					if (PlayInfo.Instance.Money < Gold)
					{
						DeviceService.Instance.openToast("Not enought gold @@");
						Debug.Log("Not enought gold @@");
					}
					else
					{
						PlayInfo.Instance.Money -= Gold;

						ItemShopController itemController = ShopController.Instance.target.GetComponent<ItemShopController>();
						ItemManager.Instance.enableItem(itemController.ID, itemController.ItemState, wave);

                        Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
						ShopController.Instance.paymentItemPanel.SetActive(false);
						PlayPanel.Instance.Shop.SetActive(false);

						PlayManager.Instance.setTowerBonus();
						PlayManager.Instance.towerInfoController.checkTowerBonus();
					}
				}
				else
				{
					if (PlayerInfo.Instance.userInfo.diamond < Diamond)
					{
						DeviceService.Instance.openToast("Not enought diamond >'<");
						Debug.Log("Not enought diamond >'<");
					}
					else
					{
						PlayerInfo.Instance.userInfo.diamond -= Diamond;
						PlayerInfo.Instance.userInfo.Save();

						ItemShopController itemController = ShopController.Instance.target.GetComponent<ItemShopController>();
						ItemManager.Instance.enableItem(itemController.ID, itemController.ItemState, wave);

                        Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
						ShopController.Instance.paymentItemPanel.SetActive(false);
						PlayPanel.Instance.Shop.SetActive(false);

						PlayManager.Instance.setTowerBonus();
						PlayManager.Instance.towerInfoController.checkTowerBonus();
					}
				}
			}
			else
			{
				controller.target = parent;
				controller.selectedPackage.transform.position = this.transform.position;
				DeviceService.Instance.openToast("One more tap to purchase!");
			}
			break;
		}
	}

	public void setSelectedPackage()
	{
		if(controller != null)
			controller.selectedPackage.transform.position = this.transform.position;
	}
}
