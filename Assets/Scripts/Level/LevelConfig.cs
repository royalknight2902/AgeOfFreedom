using UnityEngine;
using System.Collections;

public class LevelConfig
{
	#region DAILY QUEST
	public const float AnchorDailyQuestStartX = 0;
	public const float AnchorDailyQuestStartY = -0.12f;
	public const float AnchorDailyQuestDistanceX = 1.0f;
	public const float AnchorDailyQuestDistanceY = 0.38f;

	public const int ValueDailyQuestPerPage = 2;

	public static Color ColorDailyQuestBackgroundSelected = new Color((float)214 / 255, (float)197 / 255, (float)63 / 255);
	public static Color ColorDailyQuestBackgroundDefault = new Color((float)32 / 255, (float)26/ 255, (float)26 / 255);
	#endregion

	#region ACHIEVEMENT
	public const float AnchorAchievementX = -0.02f;
	public const float AnchorAchievementY = -0.18f;
	public const float AnchorAchievementDistanceY = 0.33f;

	public static Color ColorAchievementCompletedBackground = new Color((float)141 / 255, (float)142 / 255, (float)63 / 255);
	public static Color ColorAchievementCompletedOutline = new Color((float)195 / 255, (float)179 / 255, (float)92 / 255);
	#endregion

	public static string getAchievementTextValue(int id, AchievementData data, object text)
	{
		string s = "";
		if(id <= 5) // neu dang la nhiem vu giet x enemies
		{
			int result = -1;
			if(int.TryParse(text.ToString(), out result))
			{
				s += (result <= int.Parse(data.Value) ? result : int.Parse(data.Value)).ToString() + " / " + data.Value;
			}
			else
				s += "0 / " + data.Value;
		}
		return s;
	}

	public static bool getAchievementKillEnemy(int id, AchievementData data, object text)
	{
		int result = -1;
		bool b = false;
		if(int.TryParse(text.ToString(), out result))
		{
			if(result < int.Parse(data.Value))
				b = false;
			else
				b = true;
		}
		return b;
	}
}
