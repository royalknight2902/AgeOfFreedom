using UnityEngine;
using System.Collections;

public class AchievementController : MonoBehaviour 
{
	public UISprite spriteBackground;
	public UISprite spriteOutline;
	public UISprite spriteReward;
	public UISprite spriteIcon;
	public UILabel labelName;
	public UILabel labelSub;
	public UILabel labelReward;
	public GameObject completeIcon;

	public int ID {get; set;}
}
