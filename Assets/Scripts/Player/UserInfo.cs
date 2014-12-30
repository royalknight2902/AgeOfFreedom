using UnityEngine;
using System.Collections;

public class UserInfo : ObjectData
{
	public int check; //Kiem tra lan dau tien choi
	public int version;
	public int new_player;
	public int instruction;
	public int diamond;
	public int volumeSound;
	public int volumeMusic;
	public float timeScale = 1.0f;
	public string dateTime;
    public int checkTutorialLevel;
    public int checkTutorialPlay;
}

public class TutorialInfo : ObjectData
{
    public int tutorial_build;
    public int tutorial_upgrade;
    public int tutorial_shop;
    public int tutorial_pause;
    public int tutorial_option;
    public int tutorial_guide;

    public void resetValue()
    {
        tutorial_build = 0;
        tutorial_guide = 0;
        tutorial_option = 0;
        tutorial_pause = 0;
        tutorial_shop = 0;
        tutorial_upgrade = 0;

        Save();
    }
}