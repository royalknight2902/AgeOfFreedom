using UnityEngine;
using System.Collections;

public enum EWelcomePanel
{
	CONTINUE_PANEL_WELCOME,
	CLOSE_PANEL_WELCOME,
}

public class UIWelcomePanel : MonoBehaviour
{
	public UILabel LabelText;

	private int count = 0;

	void Start()
	{
		LabelText.text = "[000000]Welcome to Age Of Freedom game![-]";
	}

	void OnClick()
	{
		switch(count)
		{
			case 0:
				LabelText.text = "[000000]This is your first time\n"
				+ "We give you [ff0000]" + GameConfig.DiamondForNewPlayer + "[-] (diamond)\n"
				+ "Happy playing.[-]";
				count++;
				break;
			case 1:
				LevelPanel.Instance.hideAllPanel();
				PlayerInfo.Instance.userInfo.new_player = 1;
				PlayerInfo.Instance.userInfo.diamond = 10;
				PlayerInfo.Instance.userInfo.Save();

                LevelManager.Instance.initTutorial();
                // da hien thi trong lan choi nay
                PlayerInfo.Instance.userInfo.checkTutorialLevel = 1;
                PlayerInfo.Instance.userInfo.Save();
				break;
		}
	}
}
