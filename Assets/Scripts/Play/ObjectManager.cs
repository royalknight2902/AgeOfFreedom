using UnityEngine;
using System.Collections;

public class ObjectManager : Singleton<ObjectManager>
{
    [HideInInspector]
    public GameObject[] Towers;
    [HideInInspector]
    public GameObject[] TowersPassive;
    [HideInInspector]
    public GameObject DragonHouse;
    [HideInInspector]
    public GameObject House;

    void Awake()
    {
        initTowers();
        initTowersPassive();
        initHouse();

        getTowerDatabase();
        getTowerPassiveDatabase();
        getDragonHouseDatabase();

        //DontDestroyOnLoad(this.gameObject);
    }
    void initTowers()
    {
        Towers = new GameObject[5];
        Towers[0] = Resources.Load<GameObject>("Prefab/Tower/Architect/Tower Architect 1");
        Towers[1] = Resources.Load<GameObject>("Prefab/Tower/Ice/Tower Ice 1");
        Towers[2] = Resources.Load<GameObject>("Prefab/Tower/Poison/Tower Poison 1");
        Towers[3] = Resources.Load<GameObject>("Prefab/Tower/Rock/Tower Rock 1");
        Towers[4] = Resources.Load<GameObject>("Prefab/Tower/Fire/Tower Fire 1");
    }
    void initTowersPassive()
    {
        TowersPassive = new GameObject[1];
        TowersPassive[0] = Resources.Load<GameObject>("Prefab/Tower/Gold/Tower Gold");
    }

    void initHouse()
    {
        DragonHouse = Resources.Load<GameObject>("Prefab/House/House");
    }

    void getTowerDatabase()
    {
        int length = Towers.Length;

        for (int i = 0; i < length; i++)
        {
            TowerController tower = Towers[i].GetComponent<TowerController>();
            TowerController tower2;

            while (true)
            {
                if (ReadDatabase.Instance.TowerInfo.ContainsKey(tower.ID.Type.ToString() + tower.ID.Level.ToString()))
                {
                    TowerData data = ReadDatabase.Instance.TowerInfo[tower.ID.Type.ToString() + tower.ID.Level.ToString()];

                    tower.attribute.Range = data.Range;
                    tower.attribute.Cost = data.Cost;
                    tower.attribute.MinATK = data.MinATK;
                    tower.attribute.MaxATK = data.MaxATK;
                    tower.attribute.SpawnShoot = data.ShootSpwan;
                    tower.attribute.TimeBuild = data.TimeBuild;
                    switch (data.Type)
                    {
                        case "IRON":
                            tower.Branch = EBranchGame.IRON;
                            break;
                        case "PLANT":
                            tower.Branch = EBranchGame.PLANT;
                            break;
                        case "ICE":
                            tower.Branch = EBranchGame.ICE;
                            break;
                        case "FIRE":
                            tower.Branch = EBranchGame.FIRE;
                            break;
                        case "EARTH":
                            tower.Branch = EBranchGame.EARTH;
                            break;
                    }
                }

                if (tower.nextLevel)
                {
                    tower2 = tower.nextLevel;
                    tower = tower2;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void getTowerPassiveDatabase()
    {
        int length = TowersPassive.Length;

        for (int i = 0; i < length; i++)
        {
            TowerPassiveController tower = TowersPassive[i].GetComponent<TowerPassiveController>();
            TowerPassiveController tower2;
            while (true)
            {
                if (ReadDatabase.Instance.TowerPassiveInfo.ContainsKey(tower.ID.Type.ToString() + tower.ID.Level.ToString()))
                {

                    TowerPassiveData data = ReadDatabase.Instance.TowerPassiveInfo[tower.ID.Type.ToString() + tower.ID.Level.ToString()];
                    tower.passiveAttribute.Cost = data.Cost;
                    tower.passiveAttribute.UpdateTime = data.UpdateTime;

                    tower.passiveAttribute.Value = data.Value;
                    tower.passiveAttribute.TimeBuild = data.TimeBuild;
					tower.passiveAttribute.Describe = data.Describe;
                    switch (data.Type)
                    {
                        case "IRON":
                            tower.Branch = EBranchGame.IRON;
                            break;
                        case "PLANT":
							tower.Branch = EBranchGame.PLANT;
                            break;
                        case "ICE":
							tower.Branch = EBranchGame.ICE;
                            break;
                        case "FIRE":
							tower.Branch = EBranchGame.FIRE;
                            break;
                        case "EARTH":
							tower.Branch = EBranchGame.EARTH;
                            break;
                    }
                }

                if (tower.nextLevel)
                {
                    tower2 = (TowerPassiveController)tower.nextLevel;
                    tower = tower2;
                }
                else
                {
                    break;
                }
            }
        }
    }

    void getDragonHouseDatabase()
    {
        GameSupportor.transferHouseDragonData(DragonHouse.GetComponent<HouseController>(), 1);
    }
}
