using UnityEngine;
using System.Collections;

public enum TypeDragon
{ 
    KIM,
    MOC,
    ice,
    fire,
    THO
}

public class UIDragonSelect : MonoBehaviour {

    public TypeDragon typeDragon;

    DragonController dragonController;

    void OnClick()
    {
        GameObject arrow = GameObject.FindWithTag("DragonSelectArrow");
        arrow.GetComponent<UIAnchor>().container = this.gameObject;

        GameObject skillPanel = GameObject.FindWithTag("SkillPanel");

        for (int i = 0; i < skillPanel.transform.childCount; i++)
        {
            skillPanel.transform.GetChild(i).gameObject.GetComponent<UITexture>().mainTexture = Resources.Load<Texture>(GameConfig.PathSkillDragonIcon + typeDragon.ToString() + "-skill-" + (i + 1).ToString());
        }

    }
}
