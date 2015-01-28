using UnityEngine;
using System.Collections;

public static class Extensions
{
    public static object GetEnum(System.Type type, string s)
    {
        System.Array arr = System.Enum.GetValues(type);
        object[] objectValues = new object[arr.Length];
        System.Array.Copy(arr, objectValues, arr.Length);

        object result = null;
        foreach (object e in objectValues)
        {
            if (e.ToString().Equals(s))
            {
                result = e;
                break;
            }
        }
        return result;
    }

    public static T Next<T>(this T src) where T : struct
    {
        //if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
        T[] Arr = (T[])System.Enum.GetValues(src.GetType());
        int j = System.Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }

    public static T Previous<T>(this T src) where T : struct
    {
        //if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
        T[] Arr = (T[])System.Enum.GetValues(src.GetType());
        int j = System.Array.IndexOf<T>(Arr, src) - 1;
        return (j < 0) ? Arr[Arr.Length - 1] : Arr[j];
    }

    public static T First<T>(this T src) where T : struct
    {
        //if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
        T[] Arr = (T[])System.Enum.GetValues(src.GetType());
        return Arr[0];
    }

    public static T Last<T>(this T src) where T : struct
    {
        //if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
        T[] Arr = (T[])System.Enum.GetValues(src.GetType());
        return Arr[Arr.Length - 1];
    }
}

public class GameSupportor
{
    public static void transferEnemyData(EnemyController controller, EnemyData data)
    {
        controller.ID = data.Name;
        controller.attribute.HP.Max = controller.attribute.HP.Current = data.HP;
        controller.attribute.DEF = data.DEF;
        controller.level = data.Level;
        controller.attribute.Name = data.Name;
        controller.region = data.Region.Equals("LAND") ? EEnemyRegion.LAND : EEnemyRegion.AIR;
        controller.speed = data.Speed;
        controller.money = data.Coin;
        controller.EXP = data.EXP;
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

    public static void initEffect(EBulletEffect type, GameObject enemy, string objectID, params object[] obj)
    {
        object[] newList = new object[obj.Length + 1];
        newList[0] = objectID;

        for (int i = 0; i < obj.Length; i++)
        {
            newList[i + 1] = obj[i];
        }

        BulletEffectManager.Instance.initEffect(type, enemy, newList);
    }

    public static float getRatioAspect(GameObject nguiSprite, SpriteRenderer render)
    {
        return nguiSprite.GetComponent<UIWidget>().height / render.sprite.textureRect.height;
    }

    public static Vector2 getPivotSpriteRenderer(GameObject go)
    {
        Bounds bounds = go.GetComponentInChildren<SpriteRenderer>().bounds;
        Vector2 position = go.transform.GetChild(0).position;
        Vector2 min = bounds.min;
        Vector2 size = bounds.size;
        Vector2 offsetOfAbsolutePositionRelativelyToMinOfBounds = position - min;
        return new Vector2(
                        offsetOfAbsolutePositionRelativelyToMinOfBounds.x
                                /
                              size.x,
                        offsetOfAbsolutePositionRelativelyToMinOfBounds.y
                                /
                              size.y
                );

    }
}