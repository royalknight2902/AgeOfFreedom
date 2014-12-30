using UnityEngine;
using System.Collections;

public class UIItemShop : MonoBehaviour 
{
		GameObject parent;
		
		void Start()
		{
			parent = gameObject.transform.parent.gameObject;
		}
		
		void OnClick()
		{
			if (ShopController.Instance.target == null)
			{
				ShopController.Instance.target = parent;
				ShopController.Instance.target.GetComponent<ItemShopController>().setColor(true);
				
				ShopController.Instance.loadInfoItem();
			}
			else
			{
				if (parent == ShopController.Instance.target)
				{
					//ItemShopController itemShopController = ShopController.Instance.target.GetComponent<ItemShopController>();
					ShopController.Instance.paymentItemPanel.SetActive(true);
					ShopController.Instance.openPaymentItemPanel();
				}
				else
				{
					ShopController.Instance.target.GetComponent<ItemShopController>().setColor(false);
					ShopController.Instance.target = parent;
					parent.GetComponent<ItemShopController>().setColor(true);
					
					ShopController.Instance.loadInfoItem();
				}
			}
		}
}

