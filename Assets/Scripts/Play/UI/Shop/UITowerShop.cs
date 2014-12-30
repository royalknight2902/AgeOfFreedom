using UnityEngine;
using System.Collections;

public class UITowerShop : MonoBehaviour
{
    GameObject parent;

    public ETowerShopState type { get; set; }

    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    void OnClick()
    {
        if (ShopController.Instance.target == null)
        {
            ShopController.Instance.target = parent;
            ShopController.Instance.target.GetComponent<TowerShopController>().setColor(true);

            ShopController.Instance.loadInfoTower();
          //  ShopController.Instance.loadInfoTowerPassive();
            if (type == ETowerShopState.TOWER_BUY_ENABLE)
                ShopController.Instance.showToast(type);
        }
        else
        {
            if (parent == ShopController.Instance.target)
            {
                TowerShopController towerShopController = ShopController.Instance.target.GetComponent<TowerShopController>();
				ShopController.Instance.paymentTowerPanel.GetComponent<PaymentController>().target = towerShopController;

                if (type != ETowerShopState.TOWER_BUY_ENABLE)
                {
                    ShopController.Instance.showToast(type);
                    return;
                }
                else
					ShopController.Instance.paymentTowerPanel.SetActive(true);
            }
            else
            {
                if (type == ETowerShopState.TOWER_BUY_ENABLE)
                    ShopController.Instance.showToast(type);

                ShopController.Instance.target.GetComponent<TowerShopController>().setColor(false);
                ShopController.Instance.target = parent;
                parent.GetComponent<TowerShopController>().setColor(true);

                ShopController.Instance.loadInfoTower();
            //    ShopController.Instance.loadInfoTowerPassive();
            }
        }
    }
}
