using UnityEngine;
using System.Collections;

public class PlayPanel : Singleton<PlayPanel>
{
    public GameObject Shop;
    public GameObject Option;
    public GameObject Pause;
    public GameObject GameOver;
    public GameObject Guide;
    public GameObject Victory;
    public GameObject Effect;
    public GameObject Tutorial;
    public GameObject[] Stars;

    public void showAllPanel()
    {
		if(Shop != null) 
			Shop.SetActive(true);
		if(Option != null)
        	Option.SetActive(true);
        if(Pause != null)
			Pause.SetActive(true);
        if(GameOver != null)
			GameOver.SetActive(true);
        if(Guide != null)
			Guide.SetActive(true);
		if(Victory != null)
        	Victory.SetActive(true);
		if(Effect != null)
        	Effect.SetActive(true);
		if(Tutorial != null)
        	Tutorial.SetActive(true);
    }

    public void hideAllPanel()
    {
		if(Shop != null)
        	Shop.SetActive(false);
		if(Option != null)
        	Option.SetActive(false);
        if(Pause != null)
			Pause.SetActive(false);
        if(GameOver != null)
			GameOver.SetActive(false);
        if(Guide != null)
			Guide.SetActive(false);
        if(Victory != null)
			Victory.SetActive(false);
        if(Effect != null)
			Effect.SetActive(false);
        if(Tutorial != null)
			Tutorial.SetActive(false);
    }


}
