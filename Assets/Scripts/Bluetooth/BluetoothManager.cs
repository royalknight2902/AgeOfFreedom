using UnityEngine;
using System.Collections;

public class BluetoothManager : MonoBehaviour
{
    private static BluetoothManager _instance = null;
    public static BluetoothManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [HideInInspector]
    public bool initResult;
    [HideInInspector]
    public BluetoothMultiplayerMode desiredMode = BluetoothMultiplayerMode.None;

    void Awake()
    {
        if (FindObjectsOfType(typeof(BluetoothManager)).Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        initResult = BluetoothMultiplayerAndroid.Init("8ce255c0-200a-11e0-ac64-0800200c9a66");
        BluetoothMultiplayerAndroid.SetVerboseLog(true);

        // Registering the event delegates
        BluetoothMultiplayerAndroidManager.onBluetoothListeningStartedEvent += onBluetoothListeningStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothListeningCanceledEvent += onBluetoothListeningCanceled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent += onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent += onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent += onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectedToServerEvent += onBluetoothConnectedToServer;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectToServerFailedEvent += onBluetoothConnectToServerFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothDisconnectedFromServerEvent += onBluetoothDisconnectedFromServer;
        BluetoothMultiplayerAndroidManager.onBluetoothClientConnectedEvent += onBluetoothClientConnected;
        BluetoothMultiplayerAndroidManager.onBluetoothClientDisconnectedEvent += onBluetoothClientDisconnected;
        BluetoothMultiplayerAndroidManager.onBluetoothDevicePickedEvent += onBluetoothDevicePicked;
    }

    void OnDestroy()
    {
        BluetoothMultiplayerAndroidManager.onBluetoothListeningStartedEvent -= onBluetoothListeningStarted;
        BluetoothMultiplayerAndroidManager.onBluetoothListeningCanceledEvent -= onBluetoothListeningCanceled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnabledEvent -= onBluetoothAdapterEnabled;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterEnableFailedEvent -= onBluetoothAdapterEnableFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothAdapterDisabledEvent -= onBluetoothAdapterDisabled;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectedToServerEvent -= onBluetoothConnectedToServer;
        BluetoothMultiplayerAndroidManager.onBluetoothConnectToServerFailedEvent -= onBluetoothConnectToServerFailed;
        BluetoothMultiplayerAndroidManager.onBluetoothDisconnectedFromServerEvent -= onBluetoothDisconnectedFromServer;
        BluetoothMultiplayerAndroidManager.onBluetoothClientConnectedEvent -= onBluetoothClientConnected;
        BluetoothMultiplayerAndroidManager.onBluetoothClientDisconnectedEvent -= onBluetoothClientDisconnected;
        BluetoothMultiplayerAndroidManager.onBluetoothDevicePickedEvent -= onBluetoothDevicePicked;
    }

    public void disconnectNetWork()
    {
        Network.Disconnect();
        desiredMode = BluetoothMultiplayerMode.None;
    }

    public int countConnection()
    {
        return Network.connections.Length;
    }

    #region RPC

    public void StartMenuClient()
    {
        networkView.RPC("rpcStartMenuClient", RPCMode.Others, null);
    }

    [RPC]
    private void rpcStartMenuClient(int id)
    {
        SceneManager.Instance.Load("Menu");
    }

    public void StartGameClient(int map_id)
    {
        DeviceService.Instance.openToast("Start game");
        networkView.RPC("rpcStartGameClient", RPCMode.Others, new object[] { map_id });
    }

    [RPC]
    private void rpcStartGameClient(int id)
    {
        SceneManager.Instance.Load("Map" + id);
    }

    public void SendHeart(int current_heart)
    {
        networkView.RPC("rpcSendHeart", RPCMode.Others, new object[] { current_heart });
    }

    [RPC]
    private void rpcSendHeart(int current_heart)
    {
        UIBluetooth.Instance.labelHeart.text = current_heart.ToString();
    }

    public void SendWave(int current_wave)
    {
        networkView.RPC("rpcSendWave", RPCMode.Others, new object[] { current_wave });
    }

    [RPC]
    private void rpcSendWave(int current_wave)
    {
        UIBluetooth.Instance.labelWave.text = current_wave.ToString();
    }

    public void SendGold(int current_gold)
    {
        networkView.RPC("rpcSendGold", RPCMode.Others, new object[] { current_gold });
    }

    [RPC]
    private void rpcSendGold(int current_gold)
    {
        UIBluetooth.Instance.labelGold.text = current_gold.ToString();
    }

    public void SendDiamond(int current_diamond)
    {
        networkView.RPC("rpcSendDiamond", RPCMode.Others, new object[] { current_diamond });
    }

    [RPC]
    private void rpcSendDiamond(int current_diamond)
    {
        UIBluetooth.Instance.labelDiamond.text = current_diamond.ToString();
    }


    public void SendTower(int current_tower)
    {
        networkView.RPC("rpcSendTower", RPCMode.Others, new object[] { current_tower });
    }

    [RPC]
    private void rpcSendTower(int current_tower)
    {
        UIBluetooth.Instance.labelTower.text = current_tower.ToString();
    }

    public void SendEnemy(string id_enemy, int number_enemy, float time_enemy)
    {
        Debug.Log("send enemy");
        networkView.RPC("rpcSendEnemy", RPCMode.Others, new object[] { id_enemy, number_enemy, time_enemy });
    }

    [RPC]
    private void rpcSendEnemy(string id_enemy, int number_enemy, float time_enemy)
    {
        
    }

    IEnumerator initEnemy(string id_enemy, int number_enemy, float time_enemy)
    {
        GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
        EnemyController enemyController = model.GetComponent<EnemyController>();
        GameSupportor.transferEnemyData(enemyController, ReadDatabase.Instance.EnemyInfo[id_enemy]);
        int routine = Random.Range(0, WaveController.Instance.enemyRoutine.Length);
        int countEnemy = 0;

        for (int i = 0; i < number_enemy; i++)
        {
            GameObject enemy = Instantiate(model, WaveController.Instance.enemyStartPos[routine].transform.position, Quaternion.identity) as GameObject;

            enemy.transform.parent = WaveController.Instance.enemyStartPos[routine].transform;
            enemy.transform.localScale = Vector3.one;
            enemy.transform.localPosition = Vector3.zero;
            enemy.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueEnemy - WaveController.Instance.countEnemy;

            EnemyController ec = enemy.GetComponent<EnemyController>();
            ec.stateMove.PathGroup = WaveController.Instance.enemyRoutine[routine].transform;
            ec.waveID = 0;

            //Set depth cho thanh hp, xu ly thanh mau xuat hien sau phai? ve~ sau
            foreach (Transform health in enemy.transform)
            {
                if (health.name == PlayNameHashIDs.Health)
                {
                    foreach (Transform child in health)
                    {
                        if (child.name == PlayNameHashIDs.Foreground)
                            child.GetComponent<UISprite>().depth -= countEnemy;
                        else if (child.name == PlayNameHashIDs.Background)
                            child.GetComponent<UISprite>().depth -= (countEnemy + 1);
                    }
                    break;
                }
            }

            countEnemy++;
            if (countEnemy >= 100)
                countEnemy = 0;
            yield return new WaitForSeconds(time_enemy);
        }
    }

    [RPC]
    public void rpcDestroyTower()
    {

    }

    #endregion

    #region EVENT



    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        BluetoothMultiplayerAndroid.Disconnect();
    }

    void OnDisconnectedFromServer()
    {

        BluetoothMultiplayerAndroid.Disconnect();
    }

    void OnConnectedToServer()
    {
    }

    void OnServerInitialized()
    {
        if (Network.isServer)
        {
        }
    }

    void onBluetoothListeningStarted()
    {
        Network.InitializeServer(4, GameConfig.Port, false);
    }

    private void onBluetoothListeningCanceled()
    {
        BluetoothMultiplayerAndroid.Disconnect();
    }

    private void onBluetoothDevicePicked(BluetoothDevice device)
    {
        BluetoothMultiplayerAndroid.Connect(device.address, GameConfig.Port);
    }

    private void onBluetoothClientDisconnected(BluetoothDevice device)
    {
    }

    private void onBluetoothClientConnected(BluetoothDevice device)
    {
    }

    private void onBluetoothDisconnectedFromServer(BluetoothDevice device)
    {
        Network.Disconnect();
    }

    private void onBluetoothConnectToServerFailed(BluetoothDevice device)
    {
    }

    private void onBluetoothConnectedToServer(BluetoothDevice device)
    {
        Network.Connect(GameConfig.LocalIp, GameConfig.Port);
    }

    private void onBluetoothAdapterDisabled()
    {
    }

    private void onBluetoothAdapterEnableFailed()
    {
    }

    private void onBluetoothAdapterEnabled()
    {
        switch (desiredMode)
        {
            case BluetoothMultiplayerMode.Server:
                Network.Disconnect();
                BluetoothMultiplayerAndroid.InitializeServer(GameConfig.Port);
                break;
            case BluetoothMultiplayerMode.Client:
                Network.Disconnect();
                BluetoothMultiplayerAndroid.ShowDeviceList();
                break;
        }

        desiredMode = BluetoothMultiplayerMode.None;
    }
    #endregion
}
