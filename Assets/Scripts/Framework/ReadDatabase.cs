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
    public Dictionary<string, SkillData> SkillInfo;
    public Dictionary<string, ObjectGameData> ObjectInfo;
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
        readSkill();
        readObject();
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

            data.HP = (int)float.Parse(s[5]);
            data.Speed = float.Parse(s[7]);
            data.DEF = int.Parse(s[9]);
            data.Coin = (float.Parse(s[10].ToString()) - (int)(float.Parse(s[10].ToString())) >= 0.5)
                ? (int)(float.Parse(s[10].ToString())) + 1 : (int)(float.Parse(s[10].ToString()));
            data.Level = int.Parse(s[11]);
            data.Scale = int.Parse(s[16]);
            data.EXP = int.Parse(s[17]);

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
            data.Describe = s[4].Replace('\"', ' ').ToString().Trim();

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
        readDragonConfig();
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

    void readDragonConfig()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathDragonConfig);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i <= lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            if (!string.IsNullOrEmpty(temp[i].Trim()))
            {
                string[] s = temp[i].Split(';');
                DragonInfo.Config.ExpUpLV = int.Parse(s[0]);
                DragonInfo.Config.ValueUpLV = float.Parse(s[1]);
                DragonInfo.Config.MaxLV = int.Parse(s[2]);
                DragonInfo.Config.ValueAttributeUpLV = float.Parse(s[3]);
            }
        }
    }
    #endregion

    #region SKILL
    void readSkill()
    {
        SkillInfo = new Dictionary<string, SkillData>();
        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathSkill);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i <= lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            SkillData data = new SkillData();
            string[] s = temp[i].Split(';');
            data.Name = s[2];
            data.Mana = int.Parse(s[3]);
            data.Cooldown = float.Parse(s[4]);

            #region SKILL TYPE
            s[5] = s[5].Trim();
            string[] tempType = s[5].Split('-');
            ESkillType skillType = (ESkillType)Extensions.GetEnum(ESkillType.TARGET.GetType(), tempType[0].ToUpper());
            data.Type = skillType;

            if (skillType == ESkillType.TARGET || skillType == ESkillType.GLOBAL) //ATK skill
            {
                data.Ability = Extensions.GetEnum(ESkillOffense.AOE.GetType(), tempType[1].ToUpper());
            }
            else if (skillType == ESkillType.BUFF)
            {
                data.Ability = Extensions.GetEnum(ESkillBuff.PLAYER.GetType(), tempType[1].ToUpper());
            }
            #endregion

            #region STATE
            string[] tempState = s[6].Split('-');
            string[] tempEvent = null;

            s[8] = s[8].Trim();
            for (int k1 = 0; k1 < tempState.Length; k1++)
            {
                AnimationEventState animationState = new AnimationEventState();

                if (!s[8].Equals("none"))
                {
                    tempEvent = s[8].Split(',');
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
                }
                data.States.Add(tempState[k1].ToUpper(), animationState);
            }
            #endregion

            #region TIME FRAME
            s[7] = s[7].Trim();
            if (!s[7].Equals("none"))
            {
                string[] tempTimeFrame = s[7].Split(',');

                for (int t = 0; t < tempTimeFrame.Length; t++)
                {
                    tempTimeFrame[t] = tempTimeFrame[t].Trim();
                    int index = tempTimeFrame[t].IndexOf('(');
                    if (index != -1)
                    {
                        string state = tempTimeFrame[t].Substring(0, index);
                        foreach (KeyValuePair<string, AnimationEventState> iterator in data.States)
                        {
                            if (state.ToUpper().Equals(iterator.Key))
                            {
                                string value = tempTimeFrame[t].Substring(index, tempTimeFrame[t].Length - index);
                                value = value.Substring(1, value.Length - 2); // substring '(' & ')'

                                iterator.Value.TimeFrame = float.Parse(value);
                            }
                        }
                    }
                }
            }
            #endregion

            #region SPECIFIC LOOP
            s[9] = s[9].Trim();
            string[] tempPath = s[9].Split(',');

            for (int t = 0; t < tempPath.Length; t++)
            {
                tempPath[t] = tempPath[t].Trim();
                int index = tempPath[t].IndexOf('(');
                if (index != -1)
                {
                    string state = tempPath[t].Substring(0, index);
                    foreach (KeyValuePair<string, AnimationEventState> iterator in data.States)
                    {
                        if (state.ToUpper().Equals(iterator.Key))
                        {
                            tempPath[t] = tempPath[t].Substring(index, tempPath[t].Length - index);
                            tempPath[t] = tempPath[t].Substring(1, tempPath[t].Length - 2).Trim(); // substring '(' & ')'

                            int iSpecial = tempPath[t].IndexOf('(');
                            if (iSpecial != -1)
                            {
                                iterator.Value.isSpecificLoop = true;

                                string strSpecial = tempPath[t].Substring(iSpecial, tempPath[t].Length - iSpecial);
                                strSpecial = strSpecial.Substring(1, strSpecial.Length - 2); // substring '(' & ')'

                                string[] arr = strSpecial.Split('-');
                                iterator.Value.SpecificLoopIndex = new int[arr.Length];
                                for (int m = 0; m < arr.Length; m++)
                                {
                                    iterator.Value.SpecificLoopIndex[m] = int.Parse(arr[m]);
                                }

                                tempPath[t] = tempPath[t].Substring(0, iSpecial).Trim();
                            }

                            iterator.Value.ResourcePath = tempPath[t];
                        }
                    }
                }
            }
            #endregion

            #region VALUE
            s[10] = s[10].Trim();
            if (!s[10].Equals("none"))
            {
                string[] tempValue = s[10].Split(',');

                for (int t = 0; t < tempValue.Length; t++)
                {
                    tempValue[t] = tempValue[t].Trim();
                    int index = tempValue[t].IndexOf('(');
                    if (index != -1)
                    {
                        string state = tempValue[t].Substring(0, index);
                        foreach (KeyValuePair<string, AnimationEventState> iterator in data.States)
                        {
                            if (state.ToUpper().Equals(iterator.Key))
                            {
                                string value = tempValue[t].Substring(index, tempValue[t].Length - index);
                                value = value.Substring(1, value.Length - 2); // substring '(' & ')'
                                string[] ss = value.Trim().Split('_');
                                foreach (string eachValue in ss)
                                {
                                    string[] arrValue = eachValue.Trim().Split(':');
                                    iterator.Value.Values.Add(arrValue[0], arrValue[1]);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            SkillInfo.Add(s[1].ToUpper(), data);
        }
    }
    #endregion

    #region OBJECT
    void readObject()
    {
        ObjectInfo = new Dictionary<string, ObjectGameData>();
        TextAsset textAsset = (TextAsset)Resources.Load(GameConfig.DatabasePathObject);
        string[] temp = textAsset.text.Split('\n');

        int lenght = temp.Length;
        for (int i = 1; i <= lenght - 1; i++)
        {
            if (temp[i].Equals(""))
                break;

            ObjectGameData data = new ObjectGameData();
            string[] s = temp[i].Split(';');

            #region STATE
            string[] tempState = s[2].Split('-');
            string[] tempEvent = null;

            s[4] = s[4].Trim();
            for (int k1 = 0; k1 < tempState.Length; k1++)
            {
                AnimationEventState animationState = new AnimationEventState();

                if (!s[4].Equals("none"))
                {
                    tempEvent = s[4].Split(',');
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
                }
                data.States.Add(tempState[k1].ToUpper(), animationState);
            }
            #endregion

            #region TIME FRAME
            s[3] = s[3].Trim();
            if (!s[3].Equals("none"))
            {
                string[] tempTimeFrame = s[3].Split(',');

                for (int t = 0; t < tempTimeFrame.Length; t++)
                {
                    tempTimeFrame[t] = tempTimeFrame[t].Trim();
                    int index = tempTimeFrame[t].IndexOf('(');
                    if (index != -1)
                    {
                        string state = tempTimeFrame[t].Substring(0, index);
                        foreach (KeyValuePair<string, AnimationEventState> iterator in data.States)
                        {
                            if (state.ToUpper().Equals(iterator.Key))
                            {
                                string value = tempTimeFrame[t].Substring(index, tempTimeFrame[t].Length - index);
                                value = value.Substring(1, value.Length - 2); // substring '(' & ')'

                                iterator.Value.TimeFrame = float.Parse(value);
                            }
                        }
                    }
                }
            }
            #endregion

            #region SPECIFIC LOOP
            s[5] = s[5].Trim();
            string[] tempPath = s[5].Split(',');

            for (int t = 0; t < tempPath.Length; t++)
            {
                tempPath[t] = tempPath[t].Trim();
                int index = tempPath[t].IndexOf('(');
                if (index != -1)
                {
                    string state = tempPath[t].Substring(0, index);
                    foreach (KeyValuePair<string, AnimationEventState> iterator in data.States)
                    {
                        if (state.ToUpper().Equals(iterator.Key))
                        {
                            tempPath[t] = tempPath[t].Substring(index, tempPath[t].Length - index);
                            tempPath[t] = tempPath[t].Substring(1, tempPath[t].Length - 2).Trim(); // substring '(' & ')'

                            int iSpecial = tempPath[t].IndexOf('(');
                            if (iSpecial != -1)
                            {
                                iterator.Value.isSpecificLoop = true;

                                string strSpecial = tempPath[t].Substring(iSpecial, tempPath[t].Length - iSpecial);
                                strSpecial = strSpecial.Substring(1, strSpecial.Length - 2); // substring '(' & ')'

                                string[] arr = strSpecial.Split('-');
                                iterator.Value.SpecificLoopIndex = new int[arr.Length];
                                for (int m = 0; m < arr.Length; m++)
                                {
                                    iterator.Value.SpecificLoopIndex[m] = int.Parse(arr[m]);
                                }

                                tempPath[t] = tempPath[t].Substring(0, iSpecial).Trim();
                            }

                            iterator.Value.ResourcePath = tempPath[t];
                        }
                    }
                }
            }
            #endregion
            ObjectInfo.Add(s[1].ToUpper(), data);
        }
    }
    #endregion
}
