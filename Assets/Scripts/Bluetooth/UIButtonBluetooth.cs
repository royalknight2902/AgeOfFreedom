using UnityEngine;
using System.Collections;

public enum ETypeButtonBluetooth
{
    OPEN_PANEL,
    CLOSE_PANEL,
	OPEN_ENEMY_SHOP,
	CLOSE_ENEMY_SHOP
}

public class UIButtonBluetooth : MonoBehaviour
{
    public ETypeButtonBluetooth type;

    void OnClick()
    {
        switch (type)
        {
            case ETypeButtonBluetooth.OPEN_PANEL:
                UIBluetooth.Instance.gameObject.GetComponent<TweenPosition>().PlayForward();
                UIBluetooth.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayForward();
                UIBluetooth.Instance.buttonClose.GetComponent<TweenAlpha>().PlayForward();
                break;
            case ETypeButtonBluetooth.CLOSE_PANEL:
                UIBluetooth.Instance.gameObject.GetComponent<TweenPosition>().PlayReverse();
                UIBluetooth.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayReverse();
                UIBluetooth.Instance.buttonClose.GetComponent<TweenAlpha>().PlayReverse();
                break;
			case ETypeButtonBluetooth.OPEN_ENEMY_SHOP:
				UIEnemyShop.Instance.gameObject.GetComponent<TweenPosition>().PlayForward();
				UIEnemyShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayForward();
				UIEnemyShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayForward();
				break;
			case ETypeButtonBluetooth.CLOSE_ENEMY_SHOP:
				UIEnemyShop.Instance.gameObject.GetComponent<TweenPosition>().PlayReverse();
				UIEnemyShop.Instance.buttonOpen.GetComponent<TweenAlpha>().PlayReverse();
				UIEnemyShop.Instance.buttonClose.GetComponent<TweenAlpha>().PlayReverse();
				break;
        }
    }
}
