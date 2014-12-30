using UnityEngine;
using System.Collections;

public class GameSupportor
{
    public static void transferEnemyData(EnemyController controller, EnemyData data)
    {
        controller.attribute.HP.Max = controller.attribute.HP.Current = data.HP;
        controller.attribute.DEF = data.DEF;
        controller.level = data.Level;
        controller.attribute.Name = data.Name;
        controller.region = data.Region.Equals("LAND") ? EEnemyRegion.LAND : EEnemyRegion.AIR;
        controller.speed = data.Speed;
        controller.money = data.Coin;
    }

    public static void transferHouseDragonData(HouseController controller, int ID)
    {
        controller.ID = new STowerID(ETower.DRAGON, ID);

        DragonHouseData data = ReadDatabase.Instance.DragonInfo.House[ID];
        controller.attribute.Name = data.Name + " " + ID;
        controller.attribute.Cost = data.Cost;
        controller.attribute.TimeBuild = data.TimeBuild;
        controller.attribute.TimeGenerateChild = data.TimeGenerateChild;
        controller.attribute.LimitChild = data.LimitChild;
    }
}