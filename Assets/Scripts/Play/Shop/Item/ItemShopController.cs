using UnityEngine;
using System.Collections;

public class ItemShopController : MonoBehaviour 
{
	public string ID { get; set; }
	public string MainName{ get; set; }
	public EItemState ItemState{get; set;}

	public UISprite icon;
	public UILabel Name;
	public UILabel Value;
	public UISprite background;

	public void setColor(bool isTap)
	{
		if (isTap)
		{
			icon.color = Color.white;
			Name.color = PlayConfig.ColorShopItemName;
			Name.effectColor = PlayConfig.ColorShopItemNameOutline;
			Value.color = PlayConfig.ColorShopItemValue;
			Value.effectColor = PlayConfig.ColorShopItemValueOutline;
			background.color = Color.white;
		}
		else
		{
			icon.color = PlayConfig.ColorOff;
			Name.color = PlayConfig.ColorOff;
			Name.effectColor = Color.black;
			Value.color = PlayConfig.ColorOff;
			background.color = PlayConfig.ColorOff;
		}
	}
}
