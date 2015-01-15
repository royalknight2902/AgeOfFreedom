using UnityEngine;
using System.Collections;

public class PlayDragonInfoController : Singleton<PlayDragonInfoController>
{
	public UITexture spriteIcon;
	public UISprite spriteBranch;
	public UILabel labelLevel;
	public UILabel labelHP;
	public UILabel labelMP;
	public UISlider sliderHP;
	public UISlider sliderMP;
    public SpriteRenderer renderUlti;
    public GameObject tempSkill;
    public GameObject[] Skills;

    void Start()
    {
        string branch = PlayerInfo.Instance.dragonInfo.id;
        spriteIcon.mainTexture = Resources.Load<Texture>("Image/Dragon/Icon/dragon-" + branch.ToLower());
        spriteBranch.spriteName = "icon-branch-" + branch.ToLower();

        renderUlti.material.renderQueue = GameConfig.RenderQueueUlti;
    }
}

