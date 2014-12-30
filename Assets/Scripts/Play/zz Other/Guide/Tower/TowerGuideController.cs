using UnityEngine;
using System.Collections;

public class TowerGuideController : MonoBehaviour {
    public UISprite Icon;
    public UILabel Name;
    public UISprite Outline;

    public STowerID ID { get; set; }

    GameObject parent;

    void Start()
    {
        parent = this.transform.parent.gameObject;
    }

    void OnClick()
    {
        if (GuideController.Instance.target != this.gameObject)
        {
            if (GuideController.Instance.target != null)
                GuideController.Instance.target.GetComponentInChildren<TowerGuideController>().setColor(false);
            GuideController.Instance.target = parent;
            GuideController.Instance.loadTowerInfo();
            this.setColor(true);
        }
    }

    public void setColor(bool isEnable)
    {
        Outline.color = isEnable ? PlayConfig.ColorGuideEnemyBorderSelected : Color.white;
    }
}
