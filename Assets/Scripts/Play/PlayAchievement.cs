using UnityEngine;
using System.Collections;

public struct SPlayAchievement
{
	public int id;
	public AchievementData data;

	public SPlayAchievement(int _id, AchievementData _data)
	{
		id = _id;
		data = _data;
	}
}

public class PlayAchievement : Singleton<PlayAchievement>
{
	System.Collections.Generic.List<SPlayAchievement> queue = new System.Collections.Generic.List<SPlayAchievement> ();
	bool isRunning;
	SPlayAchievement currentAchievement;

	public void updateValueEnemy()
	{
        int lenght = PlayerInfo.Instance.listAchievement.Count;
		for(int i = 0; i < lenght; i++)
		{
            // kill enemy
            if (i <= 2)
            {
                int playerValue = int.Parse(PlayerInfo.Instance.listAchievement[i].ToString());
                int dataValue = int.Parse(ReadDatabase.Instance.AchievementInfo[i].Value.ToString());

                if (playerValue < dataValue)
                {
                    int num = playerValue + 1;
                    PlayerInfo.Instance.updateAchiement(i, num);

                    //Show popup achievement
                    if (num >= dataValue)
                    {
                        showPopup(new SPlayAchievement(i, ReadDatabase.Instance.AchievementInfo[i]));
                    }
                }
            }
		}
	}

    public void updateValueEnemyAir()
    {
        int lenght = PlayerInfo.Instance.listAchievement.Count;
        for (int i = 0; i < lenght; i++)
        {
            if (i >= 3 && i <= 5)
            {
                int playerValue = int.Parse(PlayerInfo.Instance.listAchievement[i].ToString());
                int dataValue = int.Parse(ReadDatabase.Instance.AchievementInfo[i].Value.ToString());

                if (playerValue < dataValue)
                {
                    int num = playerValue + 1;
                    PlayerInfo.Instance.updateAchiement(i, num);

                    //Show popup achievement
                    if (num >= dataValue)
                    {
                        showPopup(new SPlayAchievement(i, ReadDatabase.Instance.AchievementInfo[i]));
                    }
                }
            }
        }
    }

	public void showPopup(SPlayAchievement achievement)
	{
		queue.Add (achievement);

		if(!isRunning)
		{
			isRunning = true;

			PlayAchievementController controller = PlayManager.Instance.achievement.GetComponent<PlayAchievementController> ();
			controller.Icon.spriteName = achievement.data.Icon;
			controller.labelText.text = achievement.data.Name;
			controller.gameObject.SetActive (true);

			TweenAlpha tween = controller.GetComponent<TweenAlpha> ();
			tween.enabled = true;
			tween.PlayForward ();

			StartCoroutine (countdown (achievement));
		}
	}

	IEnumerator countdown(SPlayAchievement achievement)
	{
		currentAchievement = achievement;
		addDiamond (achievement.data.RewardAmount);
		yield return new WaitForSeconds (4.0f);
		TweenAlpha tween = PlayManager.Instance.achievement.GetComponent<TweenAlpha> ();
		tween.enabled = true;
		tween.PlayReverse ();

		StartCoroutine (checkQueue ());
	}

	IEnumerator checkQueue()
	{
		yield return new WaitForSeconds (1.0f);

		isRunning = false;
		queue.Remove (currentAchievement);
		
		if(queue.Count > 0)
		{
			isRunning = true;
			
			PlayAchievementController controller = PlayManager.Instance.achievement.GetComponent<PlayAchievementController> ();
			controller.Icon.spriteName = queue[0].data.Icon;
			controller.labelText.text = queue[0].data.Name;
			controller.gameObject.SetActive (true);
			
			TweenAlpha tween = controller.GetComponent<TweenAlpha> ();
			tween.enabled = true;
			tween.PlayForward ();
			
			StartCoroutine (countdown (queue[0]));
		}
	}

	void addDiamond(int diamonds)
	{
		StartCoroutine (waitForDiamondEffect (diamonds));
	}

	IEnumerator waitForDiamondEffect(int diamonds)
	{
		yield return new WaitForSeconds (0.1f);

		PlayerInfo.Instance.addDiamond(diamonds);
		
		GameObject temp = new GameObject();
		temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
		temp.transform.localScale = Vector3.one;
		temp.name = "Temp Player Diamond";

		//add anchor
		UIAnchor anchor = temp.AddComponent<UIAnchor> ();
		anchor.container = PlayManager.Instance.achievement;
		anchor.runOnlyOnce = true;
		anchor.side = UIAnchor.Side.Bottom;

		GameObject effectDiamond = Instantiate(PlayManager.Instance.modelPlay.PlayerDiamond) as GameObject;
		effectDiamond.transform.parent = temp.transform;
		effectDiamond.transform.localPosition = Vector3.zero;
		effectDiamond.transform.localScale = Vector3.one;
		
		UILabel label = effectDiamond.GetComponent<UILabel>();
		label.text = "Achievement completed\n+" + diamonds;
	}
}
