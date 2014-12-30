using UnityEngine;
using System.Collections;

public class MissionController : MonoBehaviour
{
	public UILabel missionText;

	public void initMission(string mapName, int heart, int gold, int waves)
	{
		Time.timeScale = 0.0f;
		string[] towerUsed = WaveController.Instance.infoMap.TowerUsed.Split('-');

		int length = towerUsed.Length;
		int i = 0;
		string tower = "";
		foreach(string s in towerUsed)
		{
			tower += " (" + s.ToLower() + ")" + ((i + 1 != length)? ",": "");
			i++;

			if(i == length)
			{
				tower = tower.Trim();
				tower += " tower";
			}
		}

		missionText.text = "[000000]Welcome to '" + mapName + "', on this match, the gifts are:\n"
			+ "+ " + heart + " (heart)\n"
			+ "+ " + gold + "(gold)\n"
			+ "+ " + tower + "\n"
			+ "And " + waves + " [1d6438][/b]waves[/b][-] to challenge\n"
			+ "Try to pass all waves. Good luck![-]";
	}

	public void initMission(string s)
	{
		Time.timeScale = 0.0f;
		missionText.text = s;
	}
}
