using UnityEngine;
using System.Collections;

public enum EMenuButton
{
	MODE_PLAY,
	ABOUT,
	SETTING,
	MORE_GAME,
	RATE,
	BACK_FROM_SETTING,
	BACK_FROM_ABOUT,
    PLAYER_1,
    PLAYER_2,
    BACK_FROM_MODE,
    CREATE_SERVER,
    CONNECT_SERVER,
    BACK_FROM_SERVER
}

public class UIMenu : MonoBehaviour {

	public EMenuButton Type;

    [HideInInspector]
    public GameObject bluetooth;

	void OnClick()
	{
		switch(Type)
		{
			case EMenuButton.MODE_PLAY:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                MenuManager.Instance.openModePlay();
				break;
			case EMenuButton.ABOUT:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

			MenuManager.Instance.openAbout();
				break;
			case EMenuButton.SETTING:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

				MenuManager.Instance.openSetting();
				break;
			case EMenuButton.MORE_GAME:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                Application.OpenURL(GameConfig.LinkMoreGame);
				break;
			case EMenuButton.RATE:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                Application.OpenURL(GameConfig.LinkRate);
				break;
			case EMenuButton.BACK_FROM_SETTING:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

				MenuManager.Instance.openMenu();
				break;
			case EMenuButton.BACK_FROM_ABOUT:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

				MenuManager.Instance.panelAbout.SetActive(false);
				MenuManager.Instance.openMenu();
				break;
            case EMenuButton.PLAYER_1:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                SceneState.Instance.State = ESceneState.ADVENTURE;
                MenuManager.Instance.toLevelScene();
                break;
            case EMenuButton.PLAYER_2:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                SceneState.Instance.State = ESceneState.BLUETOOTH;
                MenuManager.Instance.toBluetooth();
                break;
            case EMenuButton.BACK_FROM_MODE:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                MenuManager.Instance.backFromMode();
                break;
            case EMenuButton.CREATE_SERVER:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                if (BluetoothManager.Instance.initResult)
                {
                    if (BluetoothMultiplayerAndroid.CurrentMode() == BluetoothMultiplayerMode.None)
                    {
                        if (BluetoothMultiplayerAndroid.IsBluetoothEnabled())
                        {
                            BluetoothManager.Instance.disconnectNetWork();
                            if (BluetoothMultiplayerAndroid.InitializeServer(GameConfig.Port))
                            {
                                BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.Server;
                                DeviceService.Instance.openToast("You have create server!");
                                MenuManager.Instance.toLevelScene();
                            }
                        }
                        else
                        {
                            BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.Server;
                            BluetoothMultiplayerAndroid.RequestBluetoothEnable();
                        }
                    }
                    else if (BluetoothMultiplayerAndroid.CurrentMode() == BluetoothMultiplayerMode.Server)
                    {
                        MenuManager.Instance.toLevelScene();
                    }
                    else
                    {
                        DeviceService.Instance.openToast("You are client!");
                    }
                }
                else
                {
                    DeviceService.Instance.openToast("Device can't support bluetooth!");
                }
                break;
            case EMenuButton.CONNECT_SERVER:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                if (BluetoothManager.Instance.initResult)
                {
                    if (BluetoothMultiplayerAndroid.CurrentMode() == BluetoothMultiplayerMode.None)
                    {
                        if (BluetoothMultiplayerAndroid.IsBluetoothEnabled())
                        {
                            BluetoothManager.Instance.disconnectNetWork();
                            if (BluetoothMultiplayerAndroid.ShowDeviceList())
                            {
                                DeviceService.Instance.openToast("You have connect to server!");
                                BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.Client;
                                MenuManager.Instance.toLevelScene();
                            }
                        }
                        else
                        {
                            BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.Client;
                            BluetoothMultiplayerAndroid.RequestBluetoothEnable();
                        }
                    }
                    else if (BluetoothMultiplayerAndroid.CurrentMode() == BluetoothMultiplayerMode.Server)
                    {
                        DeviceService.Instance.openToast("You are server!");
                    }
                    else
                    {
                        MenuManager.Instance.toLevelScene();
                    }
                }
                else
                {
                    DeviceService.Instance.openToast("Device can't support bluetooth");
                }
                break;
            case EMenuButton.BACK_FROM_SERVER:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                MenuManager.Instance.backFromServer();

                Network.Disconnect();
                BluetoothManager.Instance.desiredMode = BluetoothMultiplayerMode.None;
                break;
            default:
                break;
		}
	}
}
