using UnityEngine;
using System.Collections;

public class UIBluetooth : Singleton<UIBluetooth>
{
    public UILabel labelWave;
    public UILabel labelGold;
    public UILabel labelDiamond;
    public UILabel labelTower;
    public UILabel labelHeart;
    public GameObject buttonOpen;
    public GameObject buttonClose;

    [HideInInspector]
    public int wave_before;
    [HideInInspector]
    public int gold_before;
    [HideInInspector]
    public int diamond_before;
    [HideInInspector]
    public int heart_before;

    void Start()
    {
        wave_before = 0;
        gold_before = 0;
        diamond_before = 0;
    }

    void Update()
    {
        if (SceneState.Instance.State == ESceneState.BLUETOOTH)
        {
            if (PlayInfo.Instance.Wave != wave_before)
            {
                wave_before = PlayInfo.Instance.Wave;
                BluetoothManager.Instance.SendWave(wave_before);
            }
            if (PlayInfo.Instance.Money != gold_before)
            {
                gold_before = PlayInfo.Instance.Money;
                BluetoothManager.Instance.SendGold(gold_before);
            }
            if (PlayerInfo.Instance.userInfo.diamond != diamond_before)
            {
                diamond_before = PlayerInfo.Instance.userInfo.diamond;
                BluetoothManager.Instance.SendDiamond(diamond_before);
            }
            if (PlayInfo.Instance.Heart != heart_before)
            {
                heart_before = PlayInfo.Instance.Heart;
                BluetoothManager.Instance.SendHeart(heart_before);
            }
        }
        else
        {

        }
    }
}
