using UnityEngine;
using System.Collections;

public enum ETutorialButton
{
	CLOSE_TUTORIAL,
	CLOSE_MISSION,
	CLOSE_MISSION_AND_CHECK_INSTRUCTION,
	NEXT_PAGE_INSTRUCTION,
	START_GAME,
}

public class UITutorial : MonoBehaviour {
	public ETutorialButton type;

	public bool isEnable { get; set; }

	void Start()
	{
		if (type == ETutorialButton.START_GAME)
			isEnable = true;
	}

	void OnClick()
	{
		if (!isEnable)
			return;

		switch(type)
		{
		case ETutorialButton.CLOSE_TUTORIAL:
			PlayManager.Instance.isZoom = true;
			if (!WaveController.Instance.isGameStart)
			{
                // neu dang o map 1 va tutorial detail play co the xuat hien duoc
                if (WaveController.Instance.currentMap == 1 && PlayerInfo.Instance.userInfo.checkTutorialPlay == 0 &&
                    SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayManager.Instance.initTimeSpeed();
                    PlayManager.Instance.initTutorial();
                    PlayerInfo.Instance.userInfo.checkTutorialPlay = 1;
                    PlayerInfo.Instance.userInfo.Save();
                }
                else
                {
                    PlayManager.Instance.initStartBattle();
                    PlayManager.Instance.initTimeSpeed();
                }
			}
			reset ();
			break;
		case ETutorialButton.CLOSE_MISSION_AND_CHECK_INSTRUCTION:

            // hien thi instruction
			if (PlayerInfo.Instance.userInfo.instruction == 1)
			{
				PlayManager.Instance.WaitInstruction();
			}
			else
			{
				PlayManager.Instance.isZoom = true;
                if (WaveController.Instance.currentMap == 1 && PlayerInfo.Instance.userInfo.checkTutorialPlay == 0
                    && SceneState.Instance.State == ESceneState.ADVENTURE)
                {
                    PlayManager.Instance.initTimeSpeed();
                    PlayManager.Instance.initTutorial();
                    PlayerInfo.Instance.userInfo.checkTutorialPlay = 1;
                    PlayerInfo.Instance.userInfo.Save();
                }
                else
                {
                    PlayManager.Instance.initStartBattle();
                    PlayManager.Instance.initTimeSpeed();
                }
			}

			reset();
			break;
		case ETutorialButton.CLOSE_MISSION:

			reset ();
			PlayManager.Instance.isZoom = true;
            if (WaveController.Instance.currentMap == 1 && PlayerInfo.Instance.userInfo.checkTutorialPlay == 0
                && SceneState.Instance.State == ESceneState.ADVENTURE)
            {
                PlayManager.Instance.initTimeSpeed();
                PlayManager.Instance.initTutorial();
                PlayerInfo.Instance.userInfo.checkTutorialPlay = 1;
                PlayerInfo.Instance.userInfo.Save();
            }
            else
            {
                PlayManager.Instance.initStartBattle();
                PlayManager.Instance.initTimeSpeed();
            }
			break;
		case ETutorialButton.NEXT_PAGE_INSTRUCTION:

			InstructionController controller = this.GetComponentInChildren<InstructionController>();
			controller.currentPage++;
			controller.setText();
			controller.setPage();

			if(controller.currentPage >= PlayConfig.PagesInstruction)
			{
				type = ETutorialButton.CLOSE_TUTORIAL;
				controller.ToggleStartup.SetActive(true);
				controller.GetComponentInChildren<UIToggle>().onChange.Add(new EventDelegate(PlayManager.Instance.setInstructionEnable));
			}

			break;

		case ETutorialButton.START_GAME:
			PlayManager.Instance.startBallte.SetActive(false);
			WaveController.Instance.gameStart();
			break;
		}
	}

	public void setEnable()
	{
		isEnable = true;
	}

	void reset()
	{
		AutoDestroy.destroyChildren(PlayPanel.Instance.Tutorial);
		PlayPanel.Instance.Tutorial.SetActive(false);
		Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
		isEnable = false;
	}
}
