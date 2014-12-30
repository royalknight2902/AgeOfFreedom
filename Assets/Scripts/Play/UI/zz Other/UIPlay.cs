using UnityEngine;
using System.Collections;

public enum EPlayButton
{
	PAUSE,
	OPTION,
	GUIDE,
	SHOP,
	CONTINUE,
	MENU,
	RESTART,
	CONTINUE_VICTORY,
	EFFECT,
	CLOSE_EFFECT_PANEL,
	CLICK,
	CONTINUE_TAP,
    DRAGON_COPULATE,
}

public class UIPlay : MonoBehaviour
{
	public EPlayButton type;

	[HideInInspector]
	public GameObject bulletEffect;

	void OnClick()
	{
		switch (type)
		{
			case EPlayButton.PAUSE:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;

				Time.timeScale = 0.0f;
				PlayPanel.Instance.Pause.SetActive(true);
				break;
			case EPlayButton.OPTION:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;

				Time.timeScale = 0.0f;
				PlayManager.Instance.updateSliderAudio();
				PlayPanel.Instance.Option.SetActive(true);

                if (PlayerInfo.Instance.tutorialInfo.tutorial_option == 0 && WaveController.Instance.currentMap == 1 
                    && SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayerInfo.Instance.tutorialInfo.tutorial_option = 1;
                    PlayerInfo.Instance.tutorialInfo.Save();

                    PlayManager.Instance.tutorial.SetActive(true);
                    UIButtonTutorialPlay.Instance.startTutorialOption();
                }
				break;
			case EPlayButton.SHOP:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;

				Time.timeScale = 0.0f;
				PlayManager.Instance.isOnShop = true;
				PlayPanel.Instance.Shop.SetActive(true);

                if (PlayerInfo.Instance.tutorialInfo.tutorial_shop == 0 && WaveController.Instance.currentMap == 1
                    && SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayerInfo.Instance.tutorialInfo.tutorial_shop = 1;
                    PlayerInfo.Instance.tutorialInfo.Save();

                    PlayManager.Instance.tutorial.SetActive(true);
                    UIButtonTutorialPlay.Instance.startTutorialShop();
                }
				break;
			case EPlayButton.GUIDE:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;

				Time.timeScale = 0.0f;
				PlayManager.Instance.isOnGuide = true;
				PlayPanel.Instance.Guide.SetActive(true);

                if (PlayerInfo.Instance.tutorialInfo.tutorial_guide == 0 && WaveController.Instance.currentMap == 1 
                    && SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayerInfo.Instance.tutorialInfo.tutorial_guide = 1;
                    PlayerInfo.Instance.tutorialInfo.Save();

                    PlayManager.Instance.tutorial.SetActive(true);
                    UIButtonTutorialPlay.Instance.startTutorialGuide();
                }
				break;
			case EPlayButton.CONTINUE:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = true;

				StartCoroutine(waitToContinue(0.2f));
				break;
			case EPlayButton.CONTINUE_TAP:
				PlayManager.Instance.isZoom = true;

				StartCoroutine(waitToContinue(0.1f));
				break;
			case EPlayButton.RESTART:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = true;

                if (SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    StartCoroutine(waitToRestart(0.2f));
                }
                else if (SceneState.Instance.State == ESceneState.BLUETOOTH)
                {
                    DeviceService.Instance.openToast("Mode bluetooth, can't restart game!");
                }
				break;
			case EPlayButton.MENU:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;

				StartCoroutine(waitToMenu(0.2f));
                if (SceneState.Instance.State == ESceneState.BLUETOOTH)
                {
                    BluetoothManager.Instance.StartMenuClient();
                }
				break;
			case EPlayButton.CONTINUE_VICTORY:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = true;

                if (SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    StartCoroutine(waitToContinueVitory(0.2f));
                }
                else if(SceneState.Instance.State == ESceneState.BLUETOOTH)
                {
                    StartCoroutine(waitToContinueVitory(0.2f));
                }
				break;
			case EPlayButton.EFFECT:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
				PlayManager.Instance.isZoom = false;
				Time.timeScale = 0.0f;
				PlayPanel.Instance.Effect.SetActive(true);

				EffectPanelController controller = PlayPanel.Instance.Effect.GetComponentInChildren<EffectPanelController>();
				controller.icon.spriteName = this.GetComponent<UISprite>().spriteName;

				//Set color for effect panel
				BulletController bulletController = bulletEffect.GetComponent<BulletController>();
				controller.setColor(bulletController.effect);
				controller.setText(bulletController);
				break;
			case EPlayButton.CLOSE_EFFECT_PANEL:
				if (!PlayManager.Instance.isOnShop && !PlayManager.Instance.isOnGuide)
					Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;

				PlayPanel.Instance.Effect.SetActive(false);
				break;
			case EPlayButton.CLICK:
				audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);
                break;
            case EPlayButton.DRAGON_COPULATE:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
				audio.PlayScheduled(0.5f);

                //hidden panel info and show panel dragon
                PlayManager.Instance.towerInfoController.GetComponent<TweenPosition>().PlayReverse();
                PlayManager.Instance.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();

                PlayDragonManager.Instance.moveToHouse();

                break;
		}
	}

#region WAITING
	IEnumerator waitToContinue(float time)
	{
		Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
		yield return new WaitForSeconds(time);
		PlayPanel.Instance.hideAllPanel();
		PlayManager.Instance.isOnGuide = false;
	}

	IEnumerator waitToRestart(float time)
	{
		PlayerInfo.Instance.userInfo.timeScale = 1.0f;
		PlayerInfo.Instance.userInfo.Save();
		Time.timeScale = 1.0f;

		yield return new WaitForSeconds(time);
		PlayPanel.Instance.hideAllPanel();
		SceneManager.Instance.Load(Application.loadedLevelName);
	}

	IEnumerator waitToMenu(float time)
	{
		PlayerInfo.Instance.userInfo.timeScale = 1.0f;
		PlayerInfo.Instance.userInfo.Save();
		Time.timeScale = 1.0f;

		yield return new WaitForSeconds(time);
		PlayPanel.Instance.hideAllPanel();
		SceneManager.Instance.Load(SceneHashIDs.LEVEL);
	}

	IEnumerator waitToContinueVitory(float time)
	{
		PlayerInfo.Instance.userInfo.timeScale = 1.0f;
		PlayerInfo.Instance.userInfo.Save();
		Time.timeScale = 1.0f;

		yield return new WaitForSeconds(time);
		PlayPanel.Instance.hideAllPanel();
		SceneManager.Instance.Load(SceneHashIDs.LEVEL);
	}

	IEnumerator waitToClick(float time)
	{
		yield return new WaitForSeconds(time);
	}
#endregion
	
}
