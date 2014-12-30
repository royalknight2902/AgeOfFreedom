using UnityEngine;
using System.Collections;

public class QuestController : MonoBehaviour {
	public UISprite Icon;
	public UISprite Background;
	public UILabel labelName;
	public UILabel labelText;
	public UILabel labelTime;
	public UILabel labelReward;

	public int ID {get; set;}
	public string SceneName {get; set;}

	public void setColor(bool isEnable)
	{
		if(isEnable)
		{
			Background.color = LevelConfig.ColorDailyQuestBackgroundSelected;
		}
		else
		{
			Background.color = LevelConfig.ColorDailyQuestBackgroundDefault;
		}
	}
}
