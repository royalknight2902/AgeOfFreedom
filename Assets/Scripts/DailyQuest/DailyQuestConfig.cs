using UnityEngine;
using System.Collections;

public class DailyQuestConfig
{
	#region 3 MINNUTES CHALLENGE
	public const int ThreeMinGoldChallenge = 500;
	public const int ThreeMinAmountEnemyMin = 5;
	public const int ThreeMinAmountEnemyMax = 10;
	public const float ThreeMinTimeDistanceEnemyMin = 0.8f;
	public const float ThreeMinTimeDistanceEnemyMax = 1.2f;
	public const int ThreeMinAmountEnemyBonusDiamond = 20; //giet 20 con duoc tang 1 kim cuong

	public static string ThreeMinMissionText = "[000000]Welcome to daily-quest '3 Mins Challenge', "
				+ "on this quest, the gifts are :\n"
				+ "+ " + ThreeMinGoldChallenge + " (gold)\n"
				+ "Try to kill enemy [ff0000]as plenty as possible[-]\n"
				+ "Get 1 (diamond) for every " + ThreeMinAmountEnemyBonusDiamond + " enemies killed\n"
				+ "Good luck![-]";

	#endregion
	
	public static int getGoldDefault()
	{
		int gold = 0;
		switch(SceneState.Instance.State)
		{
		case ESceneState.DAILY_QUEST_3MINS:
			gold = ThreeMinGoldChallenge;

			break;
		}
		return gold;
	}
}
