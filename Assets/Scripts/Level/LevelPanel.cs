using UnityEngine;
using System.Collections;

public class LevelPanel : Singleton<LevelPanel> {

	public GameObject WelcomeGift;
	public GameObject DailyQuest;
	public GameObject Achievement;
    public GameObject Dragon;
    public GameObject Bag;

	public virtual void showAllPanel()
	{
		WelcomeGift.SetActive(true);
		DailyQuest.SetActive (true);
		Achievement.SetActive (true);
        Dragon.SetActive(true);
        Bag.SetActive(true);
	}

    public virtual void hideAllPanel()
	{
    
		WelcomeGift.SetActive(false);
		DailyQuest.SetActive (false);
		Achievement.SetActive (false);
        Dragon.SetActive(false);
        Bag.SetActive(false);
	}
}