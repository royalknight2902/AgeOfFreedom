using UnityEngine;
using System.Collections;

public class ItemBuffController : MonoBehaviour 
{
	public UISprite icon;
	public UILabel labelWave;

	public string ID {get; set;}
	public EItemState State {get; set;}

	int waves;
	public int Waves
	{
		get
		{
			return waves;
		}
		set
		{
			waves = value;
			labelWave.text = waves.ToString(); 
		}
	}

	void Start()
	{
		icon.spriteName = "buff-" + ID.ToLower ();
	}
}
