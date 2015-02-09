using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager>
{
    public UILabel text;

	public GameObject panelMenu;
	public GameObject panelSetting;
	public GameObject panelAbout;
	public UISlider SliderSound;
	public UISlider SliderMusic;
	public TweenScale[] tweenScaleSetting; // setting, play, about, rate, more game
    public TweenScale[] tweenScaleSetting2; // 1 player, 2 player, back (mode play), create server, connect server, back (server)

    public TweenPosition[] tweenPositionMode; // play, about, 1 player, 2 player, back (mode play)
    public TweenAlpha[] tweenAlphaMode; // play, about, 1 player, 2 player, back (mode play)

    public TweenPosition[] tweenPositionBluetooth; // 1 player, 2 player, back (mode play), create server, connect server, back (server)
    public TweenAlpha[] tweenAlphaBluetooth; // 1 player, 2 player, back (mode play), create server, connect server, back (server)

	private AnimationCurve curve;

    void Awake()
    {
    }

	void Start()
	{
		Keyframe[] ks = new Keyframe[7];
		ks [0] = new Keyframe(0, 0); ks [0].tangentMode = 0;
		ks [1] = new Keyframe(0.392f, 0.966f); ks [1].tangentMode = 0;
		ks [2] = new Keyframe(0.456f, 1.02f); ks [2].tangentMode = 0;
		ks [3] = new Keyframe (0.598f, 1.044f); ks [3].tangentMode = 0;
		ks [4] = new Keyframe (0.736f, 1.034f); ks [4].tangentMode = 0;
		ks [5] = new Keyframe (0.816f, 1.023f); ks [5].tangentMode = 0;
		ks [6] = new Keyframe (1.0f, 1.0f); ks [6].tangentMode = 0;
		curve = new AnimationCurve(ks);

        //BluetoothManager.Instance.disconnectNetWork();
	}

    #region 1 PLAYER

    // to level scene with 1 player
    public void toLevelScene()
    {
        StartCoroutine(waitToLevelScene(0.1f));
    }

    // coroutine with 1 player
    IEnumerator waitToLevelScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.Instance.Load(SceneHashIDs.LEVEL);
    }
    #endregion
    


    #region SETTING

    // open setting
	public void openSetting()
	{
		float time = 0.0f;
		bool isGenerate = false;
		foreach(var tween in tweenScaleSetting)
		{
			tween.from = Vector3.zero;
			tween.to = Vector3.one;
			tween.duration = 0.5f;
			tween.animationCurve = curve;
			tween.PlayReverse();
			if (!isGenerate)
			{
				time = tween.duration;
				isGenerate = true;
			}
		}

        foreach (var tween in tweenScaleSetting2)
        {
            tween.PlayForward();
        }

        updateSliderAudio();

		StartCoroutine(waitToSetting(time));
	}

    // update slider audio (music and sound)
    private void updateSliderAudio()
    {
        SliderSound.value = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
		SliderMusic.value = (float)PlayerInfo.Instance.userInfo.volumeMusic / 100;
    }

    // coroutine of setting
	IEnumerator waitToSetting(float time)
	{
		yield return new WaitForSeconds(time);
		panelSetting.SetActive(true);
		panelSetting.GetComponent<TweenScale>().PlayForward();
	}

    // update value music
    public void updateMusic()
    {
        PlayerInfo.Instance.userInfo.volumeMusic = (int)(SliderMusic.value * 100);
        PlayerInfo.Instance.userInfo.Save();

        Camera.main.GetComponent<AudioSource>().volume = SliderMusic.value;
    }

    // update value sound
    public void updateSound()
    {
        PlayerInfo.Instance.userInfo.volumeSound = (int)(SliderSound.value * 100);
        PlayerInfo.Instance.userInfo.Save();
    }

	#endregion



	#region MENU
    
    // open choose mode play (1 player or 2 player)
    public void openModePlay()
    {
	
        StartCoroutine(waitToMode(0.2f));
    }

    // coroutine mode play (1 player or 2 player)
    IEnumerator waitToMode(float time)
    {

        foreach (var tween in tweenPositionMode)
        {
            tween.PlayForward();

            tween.enabled = true;
        }
        foreach (var tween in tweenAlphaMode)
        {
            tween.PlayReverse();
            tween.enabled = true;
        }
		yield return new WaitForSeconds(time);
    }

    // open menu from setting
	public void openMenu()
	{
		TweenScale tweenScale = panelSetting.GetComponent<TweenScale>();
		tweenScale.PlayReverse();

		float duration = tweenScale.duration;
		StartCoroutine(waitToMenu(duration));
	}

    //coroutine open menu from setting
	IEnumerator waitToMenu(float time)
	{
		yield return new WaitForSeconds(time);
		panelSetting.SetActive(false);

		foreach (var tween in tweenScaleSetting)
		{
			tween.PlayForward();
		}

        foreach (var tween in tweenScaleSetting2)
        {
            tween.PlayReverse();
        }
	}

    // open menu from mode play (back of mode play)
    public void backFromMode()
    {
        StartCoroutine(waitBackFromMode(0.1f));
    }

    // coroutine open menu from mode play (back of mode play)
    IEnumerator waitBackFromMode(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var tween in tweenPositionMode)
        {
            tween.PlayReverse();
        }
        foreach (var tween in tweenAlphaMode)
        {
            tween.PlayForward();
        }
    }
	
    #endregion



    #region 2 PLAYER

    // 2 player
    public void toBluetooth()
    {
        StartCoroutine(waitBluetooth(0.2f));
    }

    // coroutine 2 player
    IEnumerator waitBluetooth(float time)
    {
        
        foreach (var tween in tweenPositionBluetooth)
        {
            tween.PlayReverse();
            tween.enabled = true;
        }
        foreach (var tween in tweenAlphaBluetooth)
        {
            tween.PlayForward();
            tween.enabled = true;
        }
		yield return new WaitForSeconds(time);
    }
    #endregion
    


    #region SERVER

    // back from mode bluetooth
    public void backFromServer()
    {
        StartCoroutine(waitBackFromServer(0.2f));
    }

    // coroutine back from mode bluetooth
    IEnumerator waitBackFromServer(float time)
    {
        
        foreach (var tween in tweenPositionBluetooth)
        {
            tween.PlayForward();
            tween.enabled = true;
        }
        foreach (var tween in tweenAlphaBluetooth)
        {
            tween.PlayReverse();
            tween.enabled = true;
        }
		yield return new WaitForSeconds(time);
    }
    #endregion



    #region ABOUT

    // open about
    public void openAbout()
	{
		float time = 0.0f;
		bool isGenerate = false;
		foreach(var tween in tweenScaleSetting)
		{
			tween.from = Vector3.zero;
			tween.to = Vector3.one;
			tween.duration = 0.5f;
			tween.animationCurve = curve;
			tween.PlayReverse();
			if (!isGenerate)
			{
				time = tween.duration;
				isGenerate = true;
			}
		}
        foreach (var tween in tweenScaleSetting2)
        {
            tween.PlayReverse();
        }
		StartCoroutine(waitToAbout(time));
	}

    // coroutine open about
	IEnumerator waitToAbout(float time)
	{
		yield return new WaitForSeconds(time);
		panelAbout.SetActive(true);
	}
	#endregion



	#region DEVICE - BACK, HOME (UPDATE)
	void Update()
	{
		//BACK
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	#endregion
}
