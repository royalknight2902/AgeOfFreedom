using UnityEngine;
using System.Collections;

public enum ELevelDailyQuestButton
{
	BACK,
	NEXT,
	CANCEL_PANEL,
	MAIN,
}

public class UIDailyQuest : MonoBehaviour {
	public ELevelDailyQuestButton type;

	GameObject parent;

	void Start()
	{
		parent = this.transform.parent.gameObject;
	}

	void OnClick()
	{
		switch(type)
		{
		case ELevelDailyQuestButton.BACK:
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
			audio.PlayScheduled(0.5f);

			if(QuestManager.Instance.currentPage > 1)
			{
				foreach(Transform child in QuestManager.Instance.questTemp.transform)
				{
					UIAnchor anchor = child.GetComponent<UIAnchor>();
					anchor.relativeOffset.x +=  LevelConfig.AnchorDailyQuestDistanceX;
					anchor.enabled = true;
				}
				QuestManager.Instance.currentPage--;
				QuestManager.Instance.labelPage.text = QuestManager.Instance.currentPage + "/" + QuestManager.Instance.maxPage;
			}
			break;
		case ELevelDailyQuestButton.NEXT:
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
			audio.PlayScheduled(0.5f);

			if(QuestManager.Instance.currentPage < QuestManager.Instance.maxPage)
			{
				foreach(Transform child in QuestManager.Instance.questTemp.transform)
				{
					UIAnchor anchor = child.GetComponent<UIAnchor>();
					anchor.relativeOffset.x -=  LevelConfig.AnchorDailyQuestDistanceX;
					anchor.enabled = true;
				}
				QuestManager.Instance.currentPage++;
				QuestManager.Instance.labelPage.text = QuestManager.Instance.currentPage + "/" + QuestManager.Instance.maxPage;
			}
			break;
		case ELevelDailyQuestButton.CANCEL_PANEL:
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
			audio.PlayScheduled(0.5f);
			
			StartCoroutine(waitToCancelDailyQuest(0.2f));
			break;
		case ELevelDailyQuestButton.MAIN:
			if(QuestManager.Instance.target == null)
			{
				QuestManager.Instance.target = parent;
				QuestManager.Instance.target.GetComponent<QuestController>().setColor(true);

				DeviceService.Instance.openToast("One more tap to go '" + parent.GetComponent<QuestController>().labelName.text + "'!");
			}
			else
			{
				if(QuestManager.Instance.target == parent)
				{
					QuestManager.Instance.doDailyQuest();
				}
				else
				{
					QuestManager.Instance.target.GetComponent<QuestController>().setColor(false);
					QuestManager.Instance.target = parent;
					QuestManager.Instance.target.GetComponent<QuestController>().setColor(true);

					DeviceService.Instance.openToast("One more tap to go '" + parent.GetComponent<QuestController>().labelName.text + "'!");
				}
			}
			break;
		}
	}

	IEnumerator waitToCancelDailyQuest(float time)
	{
		yield return new WaitForSeconds(time);
		if (LevelPanel.Instance.DailyQuest.activeSelf)
		{
			LevelPanel.Instance.DailyQuest.SetActive(false);
		}
	}
}
