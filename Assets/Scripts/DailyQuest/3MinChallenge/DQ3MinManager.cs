using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DQ3MinManager : Singleton<DQ3MinManager> 
{
	private enum ESpecificTime
	{
		k0000_TO_0030,
		k0031_TO_0100,
		k0101_TO_0130,
		k0131_TO_0200,
		k0201_TO_0230,
		k0231_TO_0300,
	}

	public GameObject objTime;
	public UILabel labelTime;
	public UILabel labelKillEnemy;
	public UILabel labelCompleteDiamond;
	public TweenColor tweenTimeBackground;
	public TweenColor tweenTimeOutline;

	Dictionary<ESpecificTime, string> dataQuest;

	int gameTime;
	int minEnemyLevel;
	int maxEnemyLevel;
	int currentSpecificTime;
	int countDiamond;

	public int currentKillEnemy { get; set; }
	int killEnemy;
	public int KillEnemy
	{
		get
		{
			return killEnemy;
		}
		set
		{
			killEnemy = value;
			labelKillEnemy.text = killEnemy.ToString();
		}
	}

	void Start()
	{
		gameTime = 180; //3 mins
		currentSpecificTime = 0;
		dataQuest = new System.Collections.Generic.Dictionary<ESpecificTime, string> ();

		PlayInfo.Instance.Money = DailyQuestConfig.getGoldDefault ();
		KillEnemy = 0;
		currentKillEnemy = 0;

		initDataQuest ();
	}

	void initDataQuest()
	{
		dataQuest.Add (ESpecificTime.k0000_TO_0030, "1-1");
		dataQuest.Add (ESpecificTime.k0031_TO_0100, "1-2");
		dataQuest.Add (ESpecificTime.k0101_TO_0130, "1-3");
		dataQuest.Add (ESpecificTime.k0131_TO_0200, "2-4");
		dataQuest.Add (ESpecificTime.k0201_TO_0230, "2-5");
		dataQuest.Add (ESpecificTime.k0231_TO_0300, "3-6");
	}

	public IEnumerator Countdown()
	{
		objTime.SetActive (true);

		float totalTime = 0;
		float countTime = 0;
		tweenTimeBackground.PlayForward ();
		tweenTimeOutline.PlayForward ();

		string[] s = dataQuest[(ESpecificTime)currentSpecificTime].Split('-');
		minEnemyLevel = int.Parse(s[0]);
		maxEnemyLevel = int.Parse(s[1]);

		while(true)
		{
			totalTime += Time.deltaTime;
			if(totalTime >= 1)
			{
				gameTime--;
				countTime++;
				labelTime.text = getTimeString();
				totalTime = 0;

				if(gameTime <= 0)
				{
					end();
					yield break;
				}

				if(countTime >= 30)
				{
					currentSpecificTime++;

					string[] str = dataQuest[(ESpecificTime)currentSpecificTime].Split('-');
					minEnemyLevel = int.Parse(str[0]);
					maxEnemyLevel = int.Parse(str[1]);

					countTime = 0;
				}
			}
			yield return 0;
		}
	}

	public IEnumerator initEnemy()
	{
		int countEnemy = 0;

		System.Collections.Generic.List<EnemyData>[] listDictEnemy =
            new System.Collections.Generic.List<EnemyData>[GameConfig.MaxCurrentEnemyLevel];

		for(int i = 0 ;i< GameConfig.MaxCurrentEnemyLevel;i++)
		{
            listDictEnemy[i] = new System.Collections.Generic.List<EnemyData>();
		}

		foreach(System.Collections.Generic.KeyValuePair<string, EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
		{
			int level = iterator.Value.Level;
			listDictEnemy[level-1].Add(iterator.Value);
		}

        GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
        EnemyController enemyController = model.GetComponent<EnemyController>();
		while(true)
		{
			int amount = Random.Range(DailyQuestConfig.ThreeMinAmountEnemyMin, DailyQuestConfig.ThreeMinAmountEnemyMax);
			int enemyLevel = Random.Range(minEnemyLevel, maxEnemyLevel);

            float timeDistance = 1.0f;
            if (enemyLevel < 3)
            {
                timeDistance= Random.Range(DailyQuestConfig.ThreeMinTimeDistanceEnemyMin, DailyQuestConfig.ThreeMinTimeDistanceEnemyMax) * (1 + (float)enemyLevel / 6);
            }
            else if (enemyLevel < 5)
            {
                timeDistance = Random.Range(DailyQuestConfig.ThreeMinTimeDistanceEnemyMin, DailyQuestConfig.ThreeMinTimeDistanceEnemyMax) * (1 + (float)enemyLevel / 4);
            }
            else
            {
                timeDistance = Random.Range(DailyQuestConfig.ThreeMinTimeDistanceEnemyMin, DailyQuestConfig.ThreeMinTimeDistanceEnemyMax) * (1 + (float)enemyLevel / 3);
            }

			int levelLength = listDictEnemy[enemyLevel-1].Count;
			int iRandom = Random.Range(1, levelLength);
			int routine = Random.Range(0, WaveController.Instance.enemyRoutine.Length);
           
            EnemyData data = listDictEnemy[enemyLevel - 1][iRandom];
            GameSupportor.transferEnemyData(enemyController, data);

			// show enemy
			for(int i=0; i< amount;i++)
			{
				WaveController.Instance.checkVisibleEnemy(enemyController);

                GameObject enemy = Instantiate(model, WaveController.Instance.enemyStartPos[routine].transform.position, Quaternion.identity) as GameObject;
				enemy.transform.parent = WaveController.Instance.enemyStartPos[routine].transform;
				enemy.transform.localScale = Vector3.one;
				enemy.transform.localPosition = Vector3.zero;
				enemy.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueEnemy - countEnemy;
                enemy.GetComponent<EnemyController>().stateMove.PathGroup = WaveController.Instance.enemyRoutine[routine].transform;			

				//Set depth cho thanh hp, xu ly thanh mau xuat hien sau phai? ve~ sau
				foreach (Transform health in enemy.transform)
				{
					if (health.name == PlayNameHashIDs.Health)
					{
						foreach (Transform child in health)
						{
							if (child.name == PlayNameHashIDs.Foreground)
								child.GetComponent<UISprite>().depth -= countEnemy;
							else if (child.name == PlayNameHashIDs.Background)
								child.GetComponent<UISprite>().depth -= (countEnemy + 1);
						}
						break;
					}
				}
							
					countEnemy++;
					if (countEnemy >= 100)
						countEnemy = 0;
							
				yield return new WaitForSeconds(timeDistance);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	string getTimeString()
	{
		string result = "";
		result += gameTime / 60; //get minute
		int soDu = gameTime % 60;
		result += ":" + soDu.ToString ("00");
		return result;
	}

	public void checkEnemyDie(Transform position)
	{
		KillEnemy++;
		currentKillEnemy++;
		checkDiamond (position);
	}

	void checkDiamond(Transform transform)
	{
		if(currentKillEnemy >= DailyQuestConfig.ThreeMinAmountEnemyBonusDiamond)
		{
			PlayerInfo.Instance.addDiamond(1);
			
			GameObject temp = new GameObject();
			temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
			temp.transform.position = transform.position + new Vector3(0, 0.01f, 0);
			temp.transform.localScale = Vector3.one;
			temp.name = "Temp Player Diamond";
			
			GameObject effectDiamond = Instantiate(PlayManager.Instance.modelPlay.PlayerDiamond) as GameObject;
			effectDiamond.transform.parent = temp.transform;
			effectDiamond.transform.localPosition = Vector3.zero;
			effectDiamond.transform.localScale = Vector3.one;
			
			UILabel label = effectDiamond.GetComponent<UILabel>();
			label.text = "You have killed " + DailyQuestConfig.ThreeMinAmountEnemyBonusDiamond + " enemies\n" + "+1";

			currentKillEnemy = 0;
			countDiamond++;
		}
	}

	void end()
	{
        Time.timeScale = 0.0f;
		PlayPanel.Instance.Victory.SetActive(true);
		labelCompleteDiamond.text = "[i]You get[/i] : [85f5ff]" + countDiamond + "[-]";
	}
}
