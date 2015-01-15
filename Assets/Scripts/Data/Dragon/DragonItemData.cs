using UnityEngine;
using System.Collections;

public class DragonItemData 
{
    public string Icon { get; set; }
    public string Name { get; set; }
    public float[] Options { get; set; }
    public static string[] nameOptions = { "ATK", "DEF", "HP", "MP", "AS", "MS" };

    public string ID { get; set; }
}
