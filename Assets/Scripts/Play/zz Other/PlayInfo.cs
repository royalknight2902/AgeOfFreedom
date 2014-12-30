using UnityEngine;
using System.Collections;

public class PlayInfo : Singleton<PlayInfo> {
    public UILabel labelMoney;
    public UILabel labelHeart;
    public UILabel labelWave;
    public UILabel labelDiamondShop;

    private int m_Money;
    public int Money
    {
        set
        {
            m_Money = value;
            labelMoney.text = m_Money.ToString();
        }
        get
        {
            return m_Money;
        }
    }

    private int m_Heart;
    public int Heart
    {
        set
        {
            m_Heart = value;
            labelHeart.text = m_Heart.ToString();
        }
        get
        {
            return m_Heart;
        }
    }

    private int m_Wave;
    public int Wave
    {
        set
        {
            m_Wave = value;
            labelWave.text = m_Wave + "/" + m_totalWave;
        }
        get
        {
            return m_Wave;
        }
    }

    private int m_Diamond;
    public int Diamond
    {
        get { return m_Diamond; }
        set
        { 
            m_Diamond = value;
            labelDiamondShop.text = m_Diamond.ToString();
        }
    }

    private int m_totalWave;

    public void setTotalWave(int waves)
    {
        m_totalWave = waves;
        Wave = 0;
    }
}
