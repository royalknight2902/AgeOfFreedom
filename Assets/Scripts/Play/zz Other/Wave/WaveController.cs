using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : Singleton<WaveController>
{
    public GameObject[] enemyRoutine;
    public GameObject[] enemyStartPos;
    public NextWaveController nextWave;

    [HideInInspector]
    public int enemy_current;
    [HideInInspector]
    public Dictionary<int, int> enemy_of_wave = new Dictionary<int, int>();
    [HideInInspector]
    public MapGameData infoMap = null;
    [HideInInspector]
    public int WaveCurrent = 0;

    public int currentMap { get; set; }
    public int countEnemy { get; set; }
    public bool isGameStart { get; set; }

    void Start()
    {
        isGameStart = false;
        if (nextWave)
            nextWave.gameObject.SetActive(false);
        countEnemy = 0;
        showMission();
    }

    public void showMission()
    {
        if (SceneState.Instance.State == ESceneState.ADVENTURE)
            PlayManager.Instance.showMission(infoMap.Name, infoMap.Heart, infoMap.Money, infoMap.WaveLength);
        else if (SceneState.Instance.State == ESceneState.DAILY_QUEST_3MINS)
            PlayManager.Instance.showMission(DailyQuestConfig.ThreeMinMissionText);
    }

    public void getInfoMap()
    {
        string sceneName = Application.loadedLevelName;

        currentMap = int.Parse(sceneName.Substring(3, sceneName.Length - 3));
        infoMap = ReadDatabase.Instance.MapLevelInfo[currentMap];
        enemy_current = infoMap.EnemyTotal;

        PlayInfo.Instance.Heart = infoMap.Heart;
        PlayInfo.Instance.Money = infoMap.Money;
        PlayInfo.Instance.Diamond = PlayerInfo.Instance.userInfo.diamond;
        PlayInfo.Instance.setTotalWave(infoMap.WaveLength);
    }

    public void gameStart()
    {
        if (!isGameStart)
        {
            switch (SceneState.Instance.State)
            {
                case ESceneState.ADVENTURE:
                    StartCoroutine(initEnemy());
                    break;
                case ESceneState.DAILY_QUEST_3MINS:
                    StartCoroutine(DQ3MinManager.Instance.Countdown());
                    StartCoroutine(DQ3MinManager.Instance.initEnemy());
                    break;
                case ESceneState.BLUETOOTH:
                    StartCoroutine(initEnemy());
                    break;
                default:
                    break;
            }

            isGameStart = true;
        }
    }

    float countTimeWave(SWave wave)
    {
        float count = wave.TimeEnemy * wave.Enemies.Count;
        foreach (SEnemyWave infoEnemy in wave.Enemies)
        {
            count += infoEnemy.Quantity * infoEnemy.TimeSpawn;
        }
        return count;
    }

    IEnumerator initEnemy()
    {
        int wave_lenght = infoMap.Waves.Count;
        for (int k = 0; k < wave_lenght; k++)
        {
            WaveCurrent++;
            SWave wave = (SWave)infoMap.Waves[k];

            enemy_of_wave.Add(wave.WaveID, wave.TotalEnemy);

            // time show of wave
            if (wave.TimeWave > 0)
                yield return new WaitForSeconds(wave.TimeWave);

            PlayInfo.Instance.Wave++;
            checkItemBuffAvailable();

            countEnemy = 0;

            if (k + 1 < infoMap.Waves.Count)
                StartCoroutine(nextWaveCooldown(countTimeWave(wave), ((SWave)infoMap.Waves[k + 1]).hasBoss));

            GameObject model = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
            EnemyController enemyController = model.GetComponent<EnemyController>();

            // show enemy
            foreach (SEnemyWave infoEnemy in wave.Enemies)
            {
                //Set attribute
				EnemyData tempData = ReadDatabase.Instance.EnemyInfo.ContainsKey(infoEnemy.ID)? ReadDatabase.Instance.EnemyInfo[infoEnemy.ID] : null;

				if(tempData == null)
					break;
				GameSupportor.transferEnemyData(enemyController, tempData);

                int routine = Random.Range(0, enemyRoutine.Length);

                if(SceneState.Instance.State != ESceneState.BLUETOOTH)
                    checkVisibleEnemy(enemyController);

                for (int i = 0; i < infoEnemy.Quantity; i++)
                {
                    GameObject enemy = Instantiate(model, enemyStartPos[routine].transform.position, Quaternion.identity) as GameObject;

                    enemy.transform.parent = enemyStartPos[routine].transform;
                    enemy.transform.localScale = Vector3.one;
                    enemy.transform.localPosition = Vector3.zero;
                    enemy.GetComponentInChildren<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueEnemy - countEnemy;

                    EnemyController ec = enemy.GetComponent<EnemyController>();

                    ec.stateMove.PathGroup = enemyRoutine[routine].transform;
                    ec.waveID = wave.WaveID;

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

                    yield return new WaitForSeconds(infoEnemy.TimeSpawn);
                }
                yield return new WaitForSeconds(wave.TimeEnemy);
            }
        }
    }

    void checkItemBuffAvailable()
    {
        if (ItemManager.Instance.listItemBuff.Count == 0)
            return;

        if (WaveCurrent >= 2)
        {
            ArrayList listToDestroy = new ArrayList();

            foreach (System.Collections.Generic.KeyValuePair<string, GameObject> iterator in ItemManager.Instance.listItemBuff)
            {
                ItemBuffController itemBuffController = iterator.Value.GetComponent<ItemBuffController>();
                itemBuffController.Waves--;

                if (itemBuffController.Waves == 0)
                    listToDestroy.Add(iterator.Key);
            }

            bool hasUpdate = false;
            foreach (var i in listToDestroy)
            {
                GameObject obj = ItemManager.Instance.listItemBuff[i.ToString()];

                ItemManager.Instance.listItemBuff.Remove(i.ToString()); //remove from list
                ItemManager.Instance.listItemState.Remove(obj.GetComponent<ItemBuffController>().State);
                Destroy(obj); //delete game object

                hasUpdate = true;
            }

            if (hasUpdate)
            {
                PlayManager.Instance.resetTowerBonus();
                PlayManager.Instance.towerInfoController.checkTowerBonus();
                ItemManager.Instance.resetItemBuff();
            }
        }
    }

    // Next wave cooldown
    IEnumerator nextWaveCooldown(float totalTime, bool isBoss)
    {
        bool isEnable = false;
        float elapsedTime = 0.0f;
        while (true)
        {
            totalTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if (!isEnable)
            {
                if (totalTime <= PlayConfig.TimeNextWaveCooldown)
                {
                    if (!nextWave.gameObject.activeSelf)
                        nextWave.gameObject.SetActive(true);
                    nextWave.countdown.text = ((int)totalTime).ToString();
                    nextWave.setColor(isBoss);
                    nextWave.tween(true);
                    isEnable = true;
                }
            }
            else
            {
                if (elapsedTime >= 1.0)
                {
                    if ((int)totalTime <= 0)
                    {
                        nextWave.tween(false);
                        yield break;
                    }

                    nextWave.countdown.text = ((int)totalTime).ToString();
                    elapsedTime = 0;
                }
            }
            yield return 0;
        }
    }

    // Kiểm tra enemy xem đã được xuất hiện chưa, nếu chưa thì cập nhật lại database của player
    public void checkVisibleEnemy(EnemyController enemyController)
    {
        if (!PlayerInfo.Instance.listEnemy[enemyController.ID])
        {
            PlayerInfo.Instance.addEnemy(enemyController.ID);
            GuideController.hasUpdateEnemy = true;
            PlayManager.Instance.showTutorialNewEnemy(enemyController);
        }
    }

    // check diamond for player
    public void checkDiamond(int wave_id_enemy, GameObject enemy, bool isShootDie)
    {
        if (enemy_of_wave.ContainsKey(wave_id_enemy))
        {
            if (!isShootDie)
            {
                enemy_of_wave.Remove(wave_id_enemy);
                return;
            }

            int iTemp = enemy_of_wave[wave_id_enemy] - 1;
            if (iTemp <= 0)
            {
                PlayerInfo.Instance.addDiamond(1);
                PlayInfo.Instance.Diamond = PlayerInfo.Instance.userInfo.diamond;

                // create effect diamond
                // create parent for effect diamond
                GameObject temp = new GameObject();
                temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
                temp.transform.position = enemy.transform.position + new Vector3(0, 0.01f, 0);
                temp.transform.localScale = Vector3.one;
                temp.name = "Temp Player Diamond";

                GameObject effectDiamond = Instantiate(PlayManager.Instance.modelPlay.PlayerDiamond) as GameObject;
                effectDiamond.transform.parent = temp.transform;
                effectDiamond.transform.localPosition = Vector3.zero;
                effectDiamond.transform.localScale = Vector3.one;

                UILabel label = effectDiamond.GetComponent<UILabel>();
                label.text = "Complete wave " + wave_id_enemy + "\n" + "+1";
            }
            else
            {
                enemy_of_wave.Remove(wave_id_enemy);
                enemy_of_wave.Add(wave_id_enemy, iTemp);
            }
        }
    }

    public void showEffectHeart()
    {
        GameObject temp = new GameObject();
        temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
        temp.transform.position = PlayManager.Instance.heartEffectPosition.transform.position;
        temp.transform.localScale = Vector3.one;
        temp.name = "Temp Enemy Heart";

        GameObject enemyScore = Instantiate(PlayManager.Instance.modelPlay.EnemyHeart) as GameObject;
        enemyScore.transform.parent = temp.transform;
        enemyScore.transform.localPosition = Vector3.zero;
        enemyScore.transform.localScale = Vector3.one;
    }
}
