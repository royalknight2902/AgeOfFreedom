using UnityEngine;
using System.Collections;

public class QuestManager : Singleton<QuestManager> {
	public GameObject questTemp;
	public GameObject mainBoard;
	public UILabel labelPage;

	public int currentPage {get; set;}
	public int maxPage {get; set;}

	[HideInInspector]
	public GameObject target;

	public void initDailyQuest()
	{
        currentPage = 1;

        //set maxPage
        int questLength = ReadDatabase.Instance.QuestInfo.Count;
        if (questLength % LevelConfig.ValueDailyQuestPerPage == 0)
            maxPage = questLength / LevelConfig.ValueDailyQuestPerPage;
        else
            maxPage = (questLength / LevelConfig.ValueDailyQuestPerPage) + 1;

        labelPage.text = currentPage + "/" + maxPage;

		AutoDestroy.destroyChildren (questTemp);

		int i = 0;
		int x = 0;

		foreach(System.Collections.Generic.KeyValuePair<int, QuestData> iterator in ReadDatabase.Instance.QuestInfo)
		{
			GameObject quest = Instantiate(LevelManager.Instance.Model.Quest) as GameObject;
			quest.transform.parent = questTemp.transform;
			quest.transform.localScale = Vector3.one;

			//anchor
			UIAnchor anchor = quest.GetComponent<UIAnchor>();
			anchor.container = mainBoard;
			anchor.relativeOffset = new Vector2(LevelConfig.AnchorDailyQuestStartX + x * LevelConfig.AnchorDailyQuestDistanceX,
				   LevelConfig.AnchorDailyQuestStartY - i * LevelConfig.AnchorDailyQuestDistanceY);

			//stretch
			quest.GetComponent<UIStretch>().container = mainBoard;

			QuestController controller = quest.GetComponent<QuestController>();
			controller.labelName.text = iterator.Value.Name;
			controller.labelText.text = iterator.Value.Text;

			controller.labelTime.text = ((PlayerInfo.Instance.listDailyQuest[iterator.Key] <= iterator.Value.Time) ?
				PlayerInfo.Instance.listDailyQuest[iterator.Key] : iterator.Value.Time)
				+ " / " + iterator.Value.Time;
			controller.ID = iterator.Key;
			controller.SceneName = iterator.Value.SceneName;

			string[] rewards = iterator.Value.Reward.Split(',');
			string result = "";
			int strLength = rewards.Length;
			for(int k = 0 ; k < strLength ; k++)
			{
				result += " (" + rewards[k].ToLower() + ")";
			}
			result = result.Trim();
			controller.labelReward.text = result;

			i++;
			if(i == LevelConfig.ValueDailyQuestPerPage)
			{
				i = 0;
				x++;
			}
		}
	}

	public void doDailyQuest()
	{
		QuestController controller = target.GetComponent<QuestController> ();
		if (PlayerInfo.Instance.listDailyQuest [controller.ID] >= ReadDatabase.Instance.QuestInfo[controller.ID].Time)
		{
			DeviceService.Instance.openToast("You can't do this quest anymore, please wait to tomorrow");
			return;
		}

		//Update amount of play
		PlayerInfo.Instance.updateDailyQuest(controller.ID);
		SceneState.Instance.State = ESceneState.DAILY_QUEST_3MINS;
		SceneManager.Instance.Load (controller.SceneName);
	}
}
