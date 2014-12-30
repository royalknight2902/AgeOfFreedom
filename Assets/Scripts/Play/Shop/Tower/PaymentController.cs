using UnityEngine;
using System.Collections;

public class PaymentController : MonoBehaviour {
    public UILabel labelDiamond;
    public UILabel labelMoney;
    [HideInInspector]
    public TowerShopController target;

    void OnEnable()
    {
        Money = target.Money;
        Diamond = target.Diamond;
    }

    int money;
    public int Money
    {
        set
        {
            money = value;
            labelMoney.text = money.ToString();
        }
        get
        {
            return money;
        }
    }

    int diamond;
    public int Diamond
    {
        set
        {
            diamond = value;
            labelDiamond.text = diamond.ToString();
        }
        get
        {
            return diamond;
        }
    }
}
