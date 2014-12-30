using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour 
{
	public GameObject tempAchievement;

	void Start()
	{
		initAchievement ();
	}

	void initAchievement()
	{
		AutoDestroy.destroyChildren (tempAchievement, "Collider Drag", "Scroll Bar");

		int i = 0;
		foreach(System.Collections.Generic.KeyValuePair<int, AchievementData> iterator in ReadDatabase.Instance.AchievementInfo)
		{
			GameObject achievement = Instantiate(LevelManager.Instance.Model.Achievement) as GameObject;
			achievement.transform.parent = tempAchievement.transform;
			achievement.transform.localScale = Vector3.one;

			//Anchor
			UIAnchor uiAnchor = achievement.GetComponent<UIAnchor>();
			uiAnchor.container = tempAchievement;
			uiAnchor.relativeOffset = new Vector2(LevelConfig.AnchorAchievementX,
			                                      LevelConfig.AnchorAchievementY - i * LevelConfig.AnchorAchievementDistanceY);

			//Stretch
			achievement.GetComponent<UIStretch>().container = tempAchievement;

			//Drag object
			achievement.GetComponent<UIDragScrollView>().scrollView = tempAchievement.GetComponent<UIScrollView>();

			AchievementController controller = achievement.GetComponent<AchievementController>();
			controller.spriteIcon.spriteName = iterator.Value.Icon;
			controller.labelName.text = iterator.Value.Name;
			controller.labelSub.text = LevelConfig.getAchievementTextValue(iterator.Key, iterator.Value,
			                                                               PlayerInfo.Instance.listAchievement[iterator.Key]);
			controller.labelReward.text = iterator.Value.RewardAmount.ToString();
			controller.completeIcon.SetActive(false);

			checkAchievement(controller, iterator.Key, iterator.Value, PlayerInfo.Instance.listAchievement[iterator.Key]);

			i++;
		}
	}

	void checkAchievement(AchievementController controller, int id, AchievementData data, object text)
	{
		if(id<=2)
		{
			if(LevelConfig.getAchievementKillEnemy(id,data,text))
			{
				controller.spriteOutline.color = LevelConfig.ColorAchievementCompletedOutline;
				controller.spriteBackground.color = LevelConfig.ColorAchievementCompletedBackground;
				controller.completeIcon.SetActive(true);
			}
		}
	}
}
