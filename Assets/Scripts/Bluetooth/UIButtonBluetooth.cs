using UnityEngine;
using System.Collections;

public enum ETypeButtonBluetooth
{
    OPEN_PANEL,
    CLOSE_PANEL,
	OPEN_ENEMY_SHOP,
	CLOSE_ENEMY_SHOP, 
	OPEN_TOWER_SHOP,
	CLOSE_TOWER_SHOP,
}

public class UIButtonBluetooth : MonoBehaviour
{
    public ETypeButtonBluetooth type;
	[HideInInspector]
	static bool isEnemyShopOpen;
	static bool isTowerShopOpen;
    void OnClick()
    {
        switch (type)
        {
            case ETypeButtonBluetooth.OPEN_PANEL:
			OpenPanel();
                break;
            case ETypeButtonBluetooth.CLOSE_PANEL:
			ClosePanel();
                break;
			case ETypeButtonBluetooth.OPEN_ENEMY_SHOP:
			OpenEnemyShop();

				break;
			case ETypeButtonBluetooth.CLOSE_ENEMY_SHOP:
			CloseEnemyShop();
				break;
			case ETypeButtonBluetooth.OPEN_TOWER_SHOP:
			OpenTowerShop();

				break;
			case ETypeButtonBluetooth.CLOSE_TOWER_SHOP:
			CloseTowerShop();
				break;
        }
    }
	void OpenPanel()
	{
		UIBluetooth.Instance.gameObject.GetComponent<TweenPosition>().PlayForward();
		UIBluetooth.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayForward();
		UIBluetooth.Instance.buttonClose.GetComponent<TweenAlpha>().PlayForward();
	}
	void OpenEnemyShop()
	{
		UIEnemyShop.Instance.gameObject.GetComponent<TweenPosition>().PlayForward();
		UIEnemyShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayForward();
		UIEnemyShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayForward();
		isEnemyShopOpen = true;
	}
	void OpenTowerShop()
	{
		UIBluetoothTowerShop.Instance.gameObject.GetComponent<TweenPosition>().PlayForward();
		UIBluetoothTowerShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayForward();
		UIBluetoothTowerShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayForward();
		isTowerShopOpen = true;
	
	}
	void ClosePanel()
	{
		UIBluetooth.Instance.gameObject.GetComponent<TweenPosition>().PlayReverse();
		UIBluetooth.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayReverse();
		UIBluetooth.Instance.buttonClose.GetComponent<TweenAlpha>().PlayReverse();
	}
	void CloseEnemyShop()
	{
		UIEnemyShop.Instance.gameObject.GetComponent<TweenPosition>().PlayReverse();
		UIEnemyShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayReverse();
		UIEnemyShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayReverse();
		isEnemyShopOpen = false;
	}
	void CloseTowerShop()
	{
		UIBluetoothTowerShop.Instance.gameObject.GetComponent<TweenPosition>().PlayReverse();
		UIBluetoothTowerShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayReverse();
		UIBluetoothTowerShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayReverse();
		isTowerShopOpen = false;
	}
}
