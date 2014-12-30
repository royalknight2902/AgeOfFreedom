using UnityEngine;
using System.Collections;

public class SceneManager : Singleton<SceneManager> 
{
    string sceneLoad;
    float sceneTime;

	void Start () 
    {
        //DontDestroyOnLoad(this.gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	public void Load(string sceneName)
    {
        sceneLoad = sceneName;
        Application.LoadLevel(sceneLoad);
    }

    public void Load(string sceneName, float time)
    {
        sceneLoad = sceneName;
        StartCoroutine(SceneTime(time));
    }

    IEnumerator SceneTime(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(sceneLoad);
    }
}
