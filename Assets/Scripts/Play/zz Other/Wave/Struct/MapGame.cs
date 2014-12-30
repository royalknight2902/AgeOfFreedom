using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapGame
{
	public int MapID { get; set; }

	public string Name { get; set; }

	public int Heart { get; set; }

	public int Money { get; set; }

	public int StarTotal { get; set; }

	public string TowerUsed { get; set; }

	public int WaveLength { get; set; }

	public int EnemyTotal { get; set; }

	public ArrayList Waves { get; set; }

	public MapGame()
	{
		Waves = new ArrayList();
	}
}
