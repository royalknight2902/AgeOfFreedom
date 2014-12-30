using UnityEngine;
using System.Collections;

public class MapGameData
{
    public string Name { get; set; }
    public int Heart { get; set; }
    public int Money { get; set; }
    public int StarTotal { get; set; }
    public string TowerUsed { get; set; }
    public int WaveLength { get; set; }
    public int EnemyTotal { get; set; }
    public System.Collections.Generic.List<SWave> Waves { get; set; }

    public MapGameData()
    {
        Waves = new System.Collections.Generic.List<SWave>();
    }
}
