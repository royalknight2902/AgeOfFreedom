using UnityEngine;
using System.Collections;

public enum ELevelButton
{
    PLAY,
    BACK,
    CANCEL,
    DAILY_QUEST,
    ACHIEVEMENT,
    DRAGONHOUSE,
    BACKGROUND,
    DRAGON,
    BAG,
}

public class UILevelButton : MonoBehaviour
{
    public ELevelButton typeButton;

    void OnClick()
    {
        switch (typeButton)
        {
            case ELevelButton.PLAY:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);
                if (SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayerInfo.Instance.userInfo.timeScale = 1.0f;
                    PlayerInfo.Instance.userInfo.Save();
                    StartCoroutine(waitToPlay(0.2f));
                }
                else if (SceneState.Instance.State == ESceneState.BLUETOOTH)
                {
                    if (BluetoothManager.Instance.desiredMode == BluetoothMultiplayerMode.Server)
                    {
                        if (BluetoothManager.Instance.countConnection() >= 1)
                        {
                            PlayerInfo.Instance.userInfo.timeScale = 1.0f;
                            PlayerInfo.Instance.userInfo.Save();

                            StartCoroutine(waitToPlay(0.2f));
                            BluetoothManager.Instance.StartGameClient(LevelManager.Instance.mapInfoController.MapID);
                        }
                        else
                        {
                            DeviceService.Instance.openToast("Can't client connect to server");
                        }
                    }
                    else if (BluetoothManager.Instance.desiredMode == BluetoothMultiplayerMode.Client)
                    {
                    }
                    else
                    {

                    }
                }
                break;
            case ELevelButton.BACK:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToBack(0.2f));
                break;
            case ELevelButton.CANCEL:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToCancel(0.2f));
                break;
            case ELevelButton.DAILY_QUEST:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToDailyQuest(0.2f));
                break;
            case ELevelButton.ACHIEVEMENT:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToAchievement(0.2f));

                break;
            case ELevelButton.DRAGONHOUSE:
                if (!LevelPanel.Instance.Dragon.activeSelf)
                {
                    audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                    audio.PlayScheduled(0.5f);
                    LevelManager.Instance.Objects.btnDragon.GetComponent<UIButtonScale>().enabled = false;
                    //LevelManager.Instance.Objects.btnDragonHouseTemp.SetActive(true);
                    LevelManager.Instance.openDragonHosuse();
                }
                break;
            case ELevelButton.BACKGROUND:
                //audio.volume = (float)playerinfo.instance.userinfo.volumesound / 100;
                //audio.playscheduled(0.5f);

                if (LevelManager.Instance.Objects.btnDragonHouseTemp.activeSelf)
                {
                    LevelManager.Instance.Objects.btnDragon.GetComponent<UIButtonScale>().enabled = true;
                    LevelManager.Instance.Objects.btnDragonHouseTemp.SetActive(false);
                    LevelManager.Instance.closeDragonHouse();
                }
                break;
            case ELevelButton.DRAGON:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToDragonPanel(0.2f));

                break;
            case ELevelButton.BAG:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToBagPanel(0.2f));

                break;
        }
    }

    #region WAITING
    IEnumerator waitToPlay(float time)
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(time);
        SceneManager.Instance.Load("Map" + LevelManager.Instance.mapInfoController.MapID);
    }

    IEnumerator waitToBack(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.Instance.Load(SceneHashIDs.MENU);
    }

    IEnumerator waitToCancel(float time)
    {
        yield return new WaitForSeconds(time);
        if (LevelManager.Instance.mapInfoController.gameObject.activeSelf)
        {
            LevelManager.Instance.mapInfoController.gameObject.SetActive(false);
        }
        if (LevelPanel.Instance.Dragon.gameObject.activeSelf)
        {
            LevelPanel.Instance.Dragon.gameObject.SetActive(false);
        }
    }

    IEnumerator waitToDailyQuest(float time)
    {
        yield return new WaitForSeconds(time);
        if (!LevelPanel.Instance.DailyQuest.activeSelf)
        {
            LevelPanel.Instance.DailyQuest.SetActive(true);
            PlayerInfo.Instance.checkResetDailyQuest();
            QuestManager.Instance.initDailyQuest();
        }
    }

    IEnumerator waitToAchievement(float time)
    {
        yield return new WaitForSeconds(time);
        if (!LevelPanel.Instance.Achievement.activeSelf)
        {
            LevelPanel.Instance.Achievement.SetActive(true);
        }
    }

    

    IEnumerator waitToDragonPanel(float time)
    {
        yield return new WaitForSeconds(time);
        if (!LevelPanel.Instance.Dragon.activeSelf)
        {
            LevelPanel.Instance.Dragon.SetActive(true);
        }
    }

    IEnumerator waitToBagPanel(float time)
    {

        yield return new WaitForSeconds(time);
        if (!LevelPanel.Instance.Bag.activeSelf)
        {
            LevelPanel.Instance.Bag.SetActive(true);
        }
    }
    #endregion

}
