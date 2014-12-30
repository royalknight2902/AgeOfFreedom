using UnityEngine;
using System.Collections;

public enum ELevelAchievementButton
{
	CANCEL_PANEL,
}

public class UIAchievement : MonoBehaviour 
{
	public ELevelAchievementButton type;

	void OnClick()
	{
		switch(type)
		{
		case ELevelAchievementButton.CANCEL_PANEL:
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
			audio.PlayScheduled(0.5f);
			
			StartCoroutine(waitToCancelPanel(0.2f));
			break;
		}
	}

	IEnumerator waitToCancelPanel(float time)
	{
		yield return new WaitForSeconds(time);
		if (LevelPanel.Instance.Achievement.activeSelf)
		{
			LevelPanel.Instance.Achievement.SetActive(false);
		}
	}
}
