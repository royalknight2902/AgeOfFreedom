using UnityEngine;
using System.Collections;

public class LevelDragonManager : Singleton<LevelDragonManager> 
{
    public UISprite spriteBranch;
    public UITexture spriteIcon;
    public UILabel labelRank;

    void Start()
    {
        initFromData();
    }

    void initFromData()
    {
        spriteBranch.spriteName = "icon-branch-" + PlayerInfo.Instance.dragonInfo.id.ToLower();
        spriteIcon.mainTexture = Resources.Load<Texture>("Image/Dragon/Icon/dragon-" + PlayerInfo.Instance.dragonInfo.id.ToLower());
        labelRank.text = PlayerInfo.Instance.dragonInfo.rank.ToString();
    }

    public void updateSelectedDragon(string branch)
    {
        spriteBranch.spriteName = "icon-branch-" + PlayerInfo.Instance.dragonInfo.id.ToLower();
        spriteIcon.mainTexture = Resources.Load<Texture>("Image/Dragon/Icon/dragon-" + PlayerInfo.Instance.dragonInfo.id.ToLower());
    }
}
