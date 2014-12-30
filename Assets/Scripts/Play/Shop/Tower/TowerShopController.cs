using UnityEngine;
using System.Collections;

public class TowerShopController : MonoBehaviour
{
    [HideInInspector]
    public STowerID ID;

    public UITexture icon;
    public UILabel Name;
    public UILabel diamondLabel;
    public UILabel moneyLabel;

    public UISprite background;
    public UISprite diamondIcon;
    public UISprite moneyIcon;

	public int Index {get; set;}

    int diamond;
    public int Diamond
    {
        set
        {
            diamond = value;
            diamondLabel.text = diamond.ToString();
        }
        get
        {
            return diamond;
        }
    }

    int money;
    public int Money
    {
        set
        {
            money = value;
            moneyLabel.text = money.ToString();
        }
        get
        {
            return money;
        }
    }

    public void setColor(bool isTap)
    {
        if (isTap)
        {
            icon.color = Color.white;
            diamondLabel.color = PlayConfig.ColorDiamond;
            moneyLabel.color = PlayConfig.ColorMoney;

            background.color = Color.white;
            diamondIcon.color = Color.white;
            moneyIcon.color = Color.white;

            Color[] nameColor = PlayConfig.getColorTowerName(ID);
            Name.color = nameColor[0];
            Name.effectColor = nameColor[1];
        }
        else
        {
            icon.color = PlayConfig.ColorOff;
            Name.color = PlayConfig.ColorOff;
            Name.effectColor = Color.black;
            diamondLabel.color = PlayConfig.ColorOff;
            moneyLabel.color = PlayConfig.ColorOff;

            background.color = PlayConfig.ColorOff;
            diamondIcon.color = PlayConfig.ColorOff;
            moneyIcon.color = PlayConfig.ColorOff;
        }
    }
}
