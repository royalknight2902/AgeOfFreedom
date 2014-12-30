using UnityEngine;
using System.Collections;

public class PlayerAchievement : ObjectData
{
	public int id;
	public object value;

	public PlayerAchievement()
	{
		id = 0;
		value = "";
	}

	public PlayerAchievement(int idPlayerAchievement, object valuePlayerAchievement)
	{
		id = idPlayerAchievement;
		value = valuePlayerAchievement;
	}
}

