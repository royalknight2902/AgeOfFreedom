using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerInfo
{
	#region SINGLETON PATTERN
	static PlayerInfo m_instance;
	public static PlayerInfo Instance
	{
		get
		{
			if (m_instance == null)
				m_instance = new PlayerInfo();
			return m_instance;
		}
	}

	#endregion

	XCollection<PlayerMap> maps = new XCollection<PlayerMap>();
	XCollection<PlayerEnemy> enemies = new XCollection<PlayerEnemy>();
	XCollection<PlayerDailyQuest> dailyQuests = new XCollection<PlayerDailyQuest>();
	XCollection<PlayerAchievement> achievements = new XCollection<PlayerAchievement>();

	public UserInfo userInfo = new UserInfo();
    public PlayerDragon dragonInfo = new PlayerDragon();
    public TutorialInfo tutorialInfo = new TutorialInfo();
	public Dictionary<int, PlayerMap> listMap = new Dictionary<int, PlayerMap>();
	public Dictionary<string, bool> listEnemy = new Dictionary<string, bool>();
	public Dictionary<int, int> listDailyQuest = new Dictionary<int, int>();
	public Dictionary<int, object> listAchievement = new Dictionary<int, object> ();

	PlayerInfo()
	{
		Load();
	}

	public void Save()
	{
		userInfo.Save();
        dragonInfo.Save();
        tutorialInfo.Save();
		maps.Save();
		enemies.Save();
		dailyQuests.Save ();
		achievements.Save ();
	}

	public void Load()
	{
		int length;

        //PlayerPrefs.DeleteAll();
        //userInfo = new UserInfo();

		userInfo = userInfo.Load<UserInfo>();
        dragonInfo = dragonInfo.Load<PlayerDragon>();
        tutorialInfo.resetValue();

        // reset lai tutorial trong game, chi xuat hien mot lan duy nhat trong 1 lan choi game cua nguoi choi
		userInfo.checkTutorialLevel = 0;
		userInfo.checkTutorialPlay = 0;
		userInfo.Save();

		if (userInfo.check == 0)
		{
			reset();
			//openAllMap();
		}
		else
		{
			updateVersion();
		}

		maps.DeleteAll();
		enemies.DeleteAll();
		dailyQuests.DeleteAll();
		achievements.DeleteAll();

		PlayerMap[] tempMap = this.maps.Load<PlayerMap>();
		length = tempMap.Length;
		for (int i = 0; i < length; i++)
		{
			maps.Add(tempMap[i]);
			listMap.Add(tempMap[i].id, new PlayerMap(tempMap[i].id, tempMap[i].starSuccess, tempMap[i].starTotal));
		}

		PlayerEnemy[] tempEnemy = this.enemies.Load<PlayerEnemy>();
		length = tempEnemy.Length;
		for (int i = 0; i < length; i++)
		{
			enemies.Add(tempEnemy[i]);
			listEnemy.Add(tempEnemy[i].id, tempEnemy[i].visible);
		}

		PlayerDailyQuest[] tempDailyQuest = this.dailyQuests.Load<PlayerDailyQuest>();
		length = tempDailyQuest.Length;
		for (int i = 0; i < length; i++)
		{
			dailyQuests.Add(tempDailyQuest[i]);
			listDailyQuest.Add(tempDailyQuest[i].id, tempDailyQuest[i].Amount);

		}

		PlayerAchievement[] tempAchievement = this.achievements.Load<PlayerAchievement> ();
		length = tempAchievement.Length;

		for(int i=0;i<length;i++)
		{
			achievements.Add(tempAchievement[i]);
			listAchievement.Add(tempAchievement[i].id, tempAchievement[i].value);
		}

	}

	#region USER
	void updateVersion()
	{
        if (userInfo.version <= 4)
        {
            PlayerMap[] tempMaps = maps.Load<PlayerMap>();
            int tempDiamond = userInfo.Load<UserInfo>().diamond + 10;

            PlayerPrefs.DeleteAll();

            //map
            maps.DeleteAll();
            int len = tempMaps.Length;
            for (int i = 0; i < len; i++)
            {
                maps.Add(new PlayerMap(tempMaps[i].id, tempMaps[i].starSuccess, tempMaps[i].starTotal));
            }

            //enemy
            enemies.DeleteAll();
            foreach(System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
                enemies.Add(new PlayerEnemy(iterator.Key, false));

            dailyQuests.DeleteAll();
            len = ReadDatabase.Instance.QuestInfo.Count;
            for (int i = 1; i <= len; i++)
                dailyQuests.Add(new PlayerDailyQuest(i, 0));

            achievements.DeleteAll();
            len = ReadDatabase.Instance.AchievementInfo.Count;
            for (int i = 0; i < len; i++)
            {
                int result = -1;
                if (int.TryParse(ReadDatabase.Instance.AchievementInfo[i].Value, out result))
                    achievements.Add(new PlayerAchievement(i, 0));
                else
                    achievements.Add(new PlayerAchievement(i, ""));
            }

            //user
            userInfo.diamond = tempDiamond; //Diamond trong game
            userInfo.new_player = 1;
            userInfo.volumeSound = 100;
            userInfo.volumeMusic = 100;
            userInfo.instruction = 1;  // huong dan choi game: 1 la co hien ra, 0 la khong co
            userInfo.check = 1; //Kiem tra lan dau khi choi, 0 la choi lan dau , 1 la da choi roi
            userInfo.version = 5; //Version hien tai cua game
            userInfo.dateTime = DateTime.Now.ToShortDateString();

            Save();
        }
        if (userInfo.version > 4 && userInfo.version < 7) //version add dragon
        {
            if (dragonInfo.id.Equals(""))
            {
                dragonInfo.id = EBranchGame.FIRE.ToString().ToUpper();
                dragonInfo.rank = 1;
                dragonInfo.Save();
            }

            // version hien tai
            userInfo.version = 7;
            userInfo.Save();
        }
        if (userInfo.version == 7)
        {
        }

	}

	public void setUserInstruction(bool enable)
	{
		userInfo.instruction = System.Convert.ToInt32(enable);
		userInfo.Save ();
	}

	public void addDiamond(int value)
	{
		userInfo.diamond += value;
		userInfo.Save();
	}
	#endregion

	#region MAP
	public void addMap(PlayerMap mapPlayer)
	{
		listMap.Remove(mapPlayer.id);
		listMap[mapPlayer.id] = mapPlayer;
		maps.Delete(mapPlayer);
		maps.Add(mapPlayer);
		maps.Save();
	}
	#endregion

	#region ENEMY
	public void addEnemy(string enemyID)
	{
		PlayerEnemy[] list = enemies.GetAll();
		int length = list.Length;
		for (int i = 0; i < length; i++)
		{
			if (list[i].id.Equals(enemyID))
			{
				if (!list[i].visible)
				{
					list[i].visible = true;
				}
				break;
			}
		}

		listEnemy[enemyID] = true;

		enemies.Save();
	}
	#endregion

	#region DAILY QUEST
	public void updateDailyQuest(int dailyQuestID)
	{
		PlayerDailyQuest[] list = dailyQuests.GetAll();
		int length = list.Length;
		for (int i = 0; i < length; i++)
		{
			if (list[i].id == dailyQuestID)
			{
				list[i].Amount++;
				break;
			}
		}

		listDailyQuest [dailyQuestID]++;

		dailyQuests.Save ();
	}

	public void checkResetDailyQuest()
	{
		if (PlayerInfo.Instance.userInfo.dateTime != null)
		{
			string[] playerDT = PlayerInfo.Instance.userInfo.dateTime.Split('/');
			TimeSpan ts = DateTime.Now - new DateTime(int.Parse(playerDT[2]), int.Parse(playerDT[0]), int.Parse(playerDT[1]));

			if (ts.Days >= 1)
			{
				resetDailyQuest();
				userInfo.dateTime = DateTime.Now.ToShortDateString();
				userInfo.Save();
			}
		}
		else
		{
			resetDailyQuest();
			userInfo.dateTime = DateTime.Now.ToShortDateString();
			userInfo.Save();
		}
	}

	public void resetDailyQuest()
	{
		PlayerDailyQuest[] list = dailyQuests.GetAll();
		int length = list.Length;
		for (int i = 0; i < length; i++)
		{
			list[i].Amount = 0;
			listDailyQuest [list[i].id] = 0;
			
			break;
		}
		
		
		dailyQuests.Save ();
	}
	#endregion

	#region ACHIEVEMENT
	public void updateAchiement(int _id, object _value)
	{
		PlayerAchievement[] list = achievements.GetAll();
		int length = list.Length;
		for (int i = 0; i < length; i++)
		{
			if (list[i].id == _id)
			{
				list[i].value = _value;
				listAchievement[i] = _value;
				break;
			}
		}
		
		achievements.Save ();
	}

	#endregion

	void openAllMap()
	{
        int length;

		for(int i = 1; i <= 10; i++)
		{
			maps.Add(new PlayerMap(i,0, 3));
		}

		//enemy
		enemies.DeleteAll();
		foreach(System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
            enemies.Add(new PlayerEnemy(iterator.Key, false));

		dailyQuests.DeleteAll ();
		length = ReadDatabase.Instance.QuestInfo.Count;
		for(int i=1;i<=length;i++)
		{
			dailyQuests.Add(new PlayerDailyQuest(i, 0));
		}

		achievements.DeleteAll ();
		length = ReadDatabase.Instance.AchievementInfo.Count;
		for(int i=0;i<length;i++)
		{
			int result = -1;
			if(int.TryParse(ReadDatabase.Instance.AchievementInfo[i].Value, out result))
				achievements.Add(new PlayerAchievement(i, 0));
			else
				achievements.Add(new PlayerAchievement(i, ""));
		}

		//user
		userInfo.diamond = 0; //Diamond trong game
		userInfo.new_player = 0;
        userInfo.volumeSound = 100;
        userInfo.volumeMusic = 100;
		userInfo.instruction = 1;  // huong dan choi game: 1 la co hien ra, 0 la khong co
		userInfo.check = 1; //Kiem tra lan dau khi choi, 0 la choi lan dau , 1 la da choi roi
		userInfo.version = 6; //Version hien tai cua game
		userInfo.dateTime = DateTime.Now.ToShortDateString();

		Save();
	}

	public void reset()
	{
        int length;

        //Dragon
        dragonInfo.id = "FIRE";

		//map
		maps.DeleteAll();
		maps.Add(new PlayerMap());

		//enemy
		enemies.DeleteAll();
		foreach(System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
            enemies.Add(new PlayerEnemy(iterator.Key, false));

		dailyQuests.DeleteAll ();
		length = ReadDatabase.Instance.QuestInfo.Count;
		for(int i=1;i<=length;i++)
			dailyQuests.Add(new PlayerDailyQuest(i, 0));

		achievements.DeleteAll ();
		length = ReadDatabase.Instance.AchievementInfo.Count;
		for(int i=0;i<length;i++)
		{
			int result = -1;
			if(int.TryParse(ReadDatabase.Instance.AchievementInfo[i].Value, out result))
				achievements.Add(new PlayerAchievement(i, 0));
			else
				achievements.Add(new PlayerAchievement(i, ""));
		}
			
		//user
		userInfo.diamond = 0; //Diamond trong game
		userInfo.new_player = 0;
        userInfo.volumeSound = 100;
        userInfo.volumeMusic = 100;
		userInfo.instruction = 1;  // huong dan choi game: 1 la co hien ra, 0 la khong co
		userInfo.check = 1; //Kiem tra lan dau khi choi, 0 la choi lan dau , 1 la da choi roi
		userInfo.version = 6; //Version hien tai cua game
		userInfo.dateTime = DateTime.Now.ToShortDateString();

		Save();
	}

}
