using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class ReadDatabase
{
    #region SINGLETON PATTERN
    static ReadDatabase m_instance;
    public static ReadDatabase Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new ReadDatabase();
            return m_instance;
        }
    }

    #endregion

    public Dictionary<int, MapGameData> MapLevelInfo;
    public Dictionary<string, EnemyData> EnemyInfo;
    public Dictionary<string, TowerData> TowerInfo;
    public Dictionary<string, TowerPassiveData> TowerPassiveInfo;
    public Dictionary<string, STowerCost> TowerCostInfo;
    public Dictionary<string, ItemData> ItemInfo;
    public Dictionary<int, QuestData> QuestInfo;
    public Dictionary<int, AchievementData> AchievementInfo;
    public DragonData DragonInfo;

    ReadDatabase()
    {
        readEnemy();
        readTower();
        readTowerPassive();
        readLevelWave();
        readTowerCost();
        readItem();
        readDailyQuest();
        readAchievement();
        readDragon();
    }

    #region ENEMY
    void readEnemy()
    {
        EnemyInfo = new Dictionary<string, EnemyData>();

        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathEnemyInfo);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i < lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            EnemyData data = new EnemyData();
            string[] s = temp[i].Split(';');
            data.Name = s[1].Replace('\"', ' ').ToString().Trim();
            data.Region = s[3].Replace('\"', ' ').ToString().Trim();
            data.Branch = s[4].Replace('\"', ' ').ToString().Trim();

            data.HP = (int)(float.Parse(s[5].ToString()));
            data.Speed = float.Parse(s[7].ToString());
            data.DEF = (int)(float.Parse(s[9].ToString()));
            data.Coin = (float.Parse(s[10].ToString()) - (int)(float.Parse(s[10].ToString())) >= 0.5)
                ? (int)(float.Parse(s[10].ToString())) + 1 : (int)(float.Parse(s[10].ToString()));
            data.Level = (int)(float.Parse(s[11].ToString()));

            string[] tempState = s[13].Split('-');
            string[] tempTimeFrame = s[14].Split('-');
            string[] tempEvent = s[15].Split(',');

            for (int k1 = 0; k1 < tempState.Length; k1++)
            {
                AnimationEventState animationState = new AnimationEventState();
                animationState.TimeFrame = float.Parse(tempTimeFrame[k1]);

                for (int k2 = 0; k2 < tempEvent.Length; k2++)
                {
                    if (tempEvent[k2] != "")
                    {
                        string[] ss = tempEvent[k2].Split('-');
                        if (tempState[k1].Equals(ss[0]))
                        {
                            for (int k3 = 1; k3 < ss.Length; k3++)
                                animationState.listKeyEventFrame.Add(ss[k3]);

                            break;
                        }
                    }
                }
                data.States.Add(tempState[k1].ToUpper(), animationState);
            }

            EnemyInfo.Add(data.Name.ToString().Trim(), data);
        }
    }
    #endregion

    #region TOWER
    void readTower()
    {
        TowerInfo = new Dictionary<string, TowerData>();

        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathTowerInfo);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i < lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            TowerData data = new TowerData();
            string[] s = temp[i].Split(';');

            data.Type = s[1].Replace('\"', ' ').ToString().Trim();
            data.Level = int.Parse(s[2].ToString());
            data.Region = s[4].Replace('\"', ' ').ToString().Trim();

            data.Target = s[5].Replace('\"', ' ').ToString().Trim();
            data.BulletMode = s[6].Replace('\"', ' ').ToString().Trim();
            data.Range = int.Parse(s[7].ToString());
            data.Cost = int.Parse(s[8].ToString());
            data.MinATK = int.Parse(s[9].ToString());
            data.MaxATK = int.Parse(s[10].ToString());
            data.ShootSpwan = float.Parse(s[11].ToString());
            data.TimeBuild = float.Parse(s[12].ToString());
            data.Branch = s[13].Replace('\"', ' ').ToString().Trim();
            TowerInfo.Add(data.Type + data.Level.ToString(), data);
        }
    }

    void readTowerPassive()
    {
        TowerPassiveInfo = new Dictionary<string, TowerPassiveData>();

        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathTowerPassiveInfo);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i < lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            TowerPassiveData data = new TowerPassiveData();
            string[] s = temp[i].Split(';');

            data.Type = s[1].Replace('\"', ' ').ToString().Trim();
            data.Level = int.Parse(s[2].ToString());
            data.Region = s[4].Replace('\"', ' ').ToString().Trim();

            data.Cost = int.Parse(s[5].ToString());
            data.Value = int.Parse(s[6].ToString());
            data.UpdateTime = float.Parse(s[7].ToString());

            data.TimeBuild = float.Parse(s[8].ToString());

            data.Branch = s[9].Replace('\"', ' ').ToString().Trim();
            TowerPassiveInfo.Add(data.Type + data.Level.ToString(), data);
        }
    }
    #endregion

    #region LEVEL
    void readLevelWave()
    {
        MapLevelInfo = new Dictionary<int, MapGameData>();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathPlay);
        xmlDoc.LoadXml(path.text);

        XmlNodeList listMap = xmlDoc.GetElementsByTagName("Map");

        foreach (XmlNode infoMap in listMap)
        {
            MapGameData map = new MapGameData();
            map.EnemyTotal = 0;

            map.Name = infoMap.SelectSingleNode("Name").InnerText.ToString();
            map.Heart = int.Parse(infoMap.SelectSingleNode("Heart").InnerText.ToString());
            map.Money = int.Parse(infoMap.SelectSingleNode("Money").InnerText.ToString());
            map.StarTotal = int.Parse(infoMap.SelectSingleNode("StarTotal").InnerText.ToString());
            map.TowerUsed = infoMap.SelectSingleNode("TowerUsed").InnerText.ToString();

            XmlNode nodeWaves = infoMap.SelectSingleNode("Waves");
            XmlNodeList listWave = nodeWaves.SelectNodes("Wave");

            map.WaveLength = listWave.Count;

            foreach (XmlNode waveInfo in listWave)
            {
                SWave wave = new SWave(0);

                wave.WaveID = int.Parse(waveInfo.Attributes["WaveID"].InnerText.ToString());
                wave.TimeWave = float.Parse(waveInfo.SelectSingleNode("TimeWave").InnerText.ToString());
                wave.TimeEnemy = float.Parse(waveInfo.SelectSingleNode("TimeEnemy").InnerText.ToString());
                wave.hasBoss = bool.Parse(waveInfo.Attributes["hasBoss"].InnerText.ToString());

                XmlNodeList listEnemy = waveInfo.SelectNodes("Enemy");
                foreach (XmlNode enemyInfo in listEnemy)
                {
                    string[] arr = enemyInfo.InnerText.ToString().Split('-');

                    SEnemyWave enemy = new SEnemyWave();
                    enemy.ID = arr[0].ToString();
                    enemy.Quantity = int.Parse(arr[1].ToString());
                    enemy.TimeSpawn = float.Parse(arr[2].ToString());

                    wave.Enemies.Add(enemy);
                    wave.TotalEnemy += enemy.Quantity;
                    map.EnemyTotal += enemy.Quantity;
                }
                // add wave into map
                map.Waves.Add(wave);
            }
            // add map into array map
            MapLevelInfo.Add(int.Parse(infoMap.Attributes["MapID"].InnerText.ToString()), map);
        }
    }
    #endregion

    #region TOWER COST
    void readTowerCost()
    {
        TowerCostInfo = new Dictionary<string, STowerCost>();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathTowerCost);
        xmlDoc.LoadXml(path.text);

        XmlNodeList listMap = xmlDoc.GetElementsByTagName("Tower");

        foreach (XmlNode infoMap in listMap)
        {
            STowerCost towerCost = new STowerCost();
            towerCost.Diamond = int.Parse(infoMap.SelectSingleNode("Diamond").InnerText);
            towerCost.Gold = int.Parse(infoMap.SelectSingleNode("Gold").InnerText);

            TowerCostInfo.Add(infoMap.Attributes["id"].InnerText, towerCost);
        }
    }
    #endregion

    #region GAME ITEM
    void readItem()
    {
        ItemInfo = new Dictionary<string, ItemData>();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathItem);
        xmlDoc.LoadXml(path.text);

        XmlNodeList listItem = xmlDoc.GetElementsByTagName("Item");

        foreach (XmlNode infoItem in listItem)
        {
            ItemData itemData = new ItemData();
            itemData.Name = infoItem.SelectSingleNode("Name").InnerText;
            itemData.ValueEffect = float.Parse(infoItem.SelectSingleNode("ValueEffect").InnerText);
            itemData.ValueText = infoItem.SelectSingleNode("ValueText").InnerText;

            XmlNodeList listPackage = infoItem.SelectNodes("Package");
            foreach (XmlNode infoPackage in listPackage)
            {
                ItemPackage package = new ItemPackage();
                package.Index = int.Parse(infoPackage.Attributes["ID"].InnerText);
                package.Diamond = int.Parse(infoPackage.SelectSingleNode("Diamond").InnerText);
                package.Gold = int.Parse(infoPackage.SelectSingleNode("Gold").InnerText);
                package.Wave = infoPackage.SelectSingleNode("Wave").InnerText;

                itemData.Packages.Add(package);
            }
            ItemInfo.Add(infoItem.Attributes["ID"].InnerText.Trim(), itemData);
        }
    }
    #endregion

    #region DAILY QUEST
    void readDailyQuest()
    {
        QuestInfo = new Dictionary<int, QuestData>();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathDailyQuest);
        xmlDoc.LoadXml(path.text);

        XmlNodeList listQuest = xmlDoc.GetElementsByTagName("Quest");

        foreach (XmlNode infoQuest in listQuest)
        {
            QuestData questData = new QuestData();
            questData.Name = infoQuest.SelectSingleNode("Name").InnerText;
            questData.Text = infoQuest.SelectSingleNode("Text").InnerText;
            questData.Time = int.Parse(infoQuest.SelectSingleNode("Time").InnerText);
            questData.Reward = infoQuest.SelectSingleNode("Reward").InnerText;
            questData.SceneName = infoQuest.SelectSingleNode("SceneName").InnerText;

            QuestInfo.Add(int.Parse(infoQuest.Attributes["ID"].InnerText.Trim()), questData);
        }
    }
    #endregion

    #region ACHIEVEMENT
    void readAchievement()
    {
        AchievementInfo = new Dictionary<int, AchievementData>();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathAchievement);
        xmlDoc.LoadXml(path.text);

        XmlNodeList listAchievement = xmlDoc.GetElementsByTagName("Achievement");

        foreach (XmlNode infoAchievement in listAchievement)
        {
            AchievementData achievementData = new AchievementData();
            achievementData.Icon = infoAchievement.SelectSingleNode("Icon").InnerText;
            achievementData.Name = infoAchievement.SelectSingleNode("Name").InnerText;
            achievementData.Value = infoAchievement.SelectSingleNode("Value").InnerText;
            XmlNodeList listReward = infoAchievement.SelectNodes("Reward");
            foreach (XmlNode infoReward in listReward)
            {
                achievementData.RewardAmount = int.Parse(infoReward.SelectSingleNode("Amount").InnerText);
                achievementData.RewardValue = infoReward.SelectSingleNode("Value").InnerText;
            }

            AchievementInfo.Add(int.Parse(infoAchievement.Attributes["ID"].InnerText.Trim()), achievementData);
        }
    }
    #endregion

    #region DRAGON
    void readDragon()
    {
        DragonInfo = new DragonData();

        readDragonPlayer();
        readDragonHouse();
        readDragonItem();
    }

    void readDragonPlayer()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathDragonPlayer);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i <= lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            DragonPlayerData data = new DragonPlayerData();
            string[] s = temp[i].Split(';');
            data.Name = s[1];
            data.HP = int.Parse(s[2]);
            data.MP = int.Parse(s[3]);
            data.DEF = int.Parse(s[4]);
            string[] tempATK = s[5].Split('-');
            data.ATK = new SMinMax(int.Parse(tempATK[0]), int.Parse(tempATK[1]));
            data.ATKSpeed = float.Parse(s[7]);
            data.MoveSpeed = float.Parse(s[8]);

            #region SKILL
            int index = 0;
            string skillLine = s[12].Substring(0, s[12].Length - 1);
            if (!skillLine.Equals("none"))
            {
                string[] tempSkill = s[12].Split('-');
                foreach (string skill in tempSkill)
                {
                    DragonPlayerSkillData skillData = new DragonPlayerSkillData();
                    skillData.Index = index;

                    int indexSS = skill.IndexOf("(ss)");
                    string tempStr = skill;
                    if (indexSS == -1)
                        skillData.Ulti = false;
                    else
                    {
                        tempStr = skill.Substring(0, indexSS);
                        skillData.Ulti = true;
                    }
                    skillData.ID = tempStr;

                    data.Skills.Add(skillData);
                    index++;
                }
            }
            #endregion

            #region STATE
            string[] tempState = s[9].Split('-');
            string[] tempTimeFrame = s[10].Split('-');
            string[] tempEvent = s[11].Split(',');

            for (int k1 = 0; k1 < tempState.Length; k1++)
            {
                AnimationEventState animationState = new AnimationEventState();
                animationState.TimeFrame = float.Parse(tempTimeFrame[k1]);

                for (int k2 = 0; k2 < tempEvent.Length; k2++)
                {
                    if (tempEvent[k2] != "")
                    {
                        string[] ss = tempEvent[k2].Split('-');
                        if (tempState[k1].Equals(ss[0]))
                        {
                            for (int k3 = 1; k3 < ss.Length; k3++)
                                animationState.listKeyEventFrame.Add(ss[k3]);

                            break;
                        }
                    }
                }
                data.States.Add(tempState[k1].ToUpper(), animationState);
            }
            #endregion

            string branch = s[6];
            DragonInfo.Player.Add(branch.ToUpper(), data);
        }
    }


    void readDragonHouse()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathDragonHouse);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i <= lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            if (!string.IsNullOrEmpty(temp[i].Trim()))
            {
                DragonHouseData data = new DragonHouseData();

                string[] s = temp[i].Split(';');
                data.Name = s[1];
                data.Level = int.Parse(s[2]);
                data.Cost = int.Parse(s[3]);
                data.TimeBuild = float.Parse(s[4]);
                data.TimeGenerateChild = float.Parse(s[5]);
                data.LimitChild = int.Parse(s[6]);

                DragonInfo.House.Add(data.Level, data);
            }
        }
    }

    void readDragonItem()
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset path = Resources.Load<TextAsset>(GameConfig.DatabasePathDragonItem);
        xmlDoc.LoadXml(path.text);
        XmlNodeList listDragonItems = xmlDoc.GetElementsByTagName("Item");

        foreach (XmlNode infoDragonItem in listDragonItems)
        {
            DragonItemData dragonItemData = new DragonItemData();
            dragonItemData.Icon = infoDragonItem.SelectSingleNode("Icon").InnerText;
            dragonItemData.Name = infoDragonItem.SelectSingleNode("Name").InnerText;
            XmlNodeList listOption = infoDragonItem.SelectNodes("Option");
            dragonItemData.Options = new float[DragonItemData.nameOptions.Length];

            foreach (XmlNode infoOption in listOption)
            {
                dragonItemData.Options[0] = float.Parse(infoOption.SelectSingleNode("ATK").InnerText);
                dragonItemData.Options[1] = float.Parse(infoOption.SelectSingleNode("DEF").InnerText);
                dragonItemData.Options[2] = float.Parse(infoOption.SelectSingleNode("HP").InnerText);
                dragonItemData.Options[3] = float.Parse(infoOption.SelectSingleNode("MP").InnerText);
                dragonItemData.Options[4] = float.Parse(infoOption.SelectSingleNode("AtkSpeed").InnerText);
                dragonItemData.Options[5] = float.Parse(infoOption.SelectSingleNode("MoveSpeed").InnerText);
            }
            string id = infoDragonItem.Attributes["ID"].InnerText.Trim();
            dragonItemData.ID = id;
            DragonInfo.Item.Add(id, dragonItemData);
        }
    }
    #endregion
}
