using UnityEngine;
using System.Collections;

public class PlayerDailyQuest : ObjectData {
	public int id;
	public int Amount;

	public PlayerDailyQuest()
	{
		id = 1;
		Amount = 0;
	}

	public PlayerDailyQuest(int idDailyQuest, int amount)
	{
		id = idDailyQuest;
		Amount = amount;
	}
}
