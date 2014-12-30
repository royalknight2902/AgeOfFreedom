using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public bool IsMusic;

	void Awake ()
	{
		if (IsMusic)
		{
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeMusic / 100;
		}
		else
		{
			audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
			audio.playOnAwake = true;
		}
	}
}