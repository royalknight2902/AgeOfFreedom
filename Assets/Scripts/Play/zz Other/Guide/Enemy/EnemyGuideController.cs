using UnityEngine;
using System.Collections;

public class EnemyGuideController : MonoBehaviour
{
    public UITexture Icon;
    public GameObject Lock;

    UISprite border;

    public string ID { get; set; }
    public bool visible { get; set; }

    void Awake()
    {
        border = GetComponent<UISprite>();
    }

    void Start()
    {
        if (visible)
        {
            Icon.gameObject.SetActive(true);
            Lock.SetActive(false);
        }
        else
        {
            Icon.gameObject.SetActive(false);
            Lock.SetActive(true);
        }

    }

    void OnClick()
    {
        if (visible)
        {
            if (GuideController.Instance.target != this.gameObject)
            {
                if (GuideController.Instance.target != null)
                    GuideController.Instance.target.GetComponent<EnemyGuideController>().setColor(false);
                GuideController.Instance.target = this.gameObject;
                GuideController.Instance.loadEnemyInfo();
                this.setColor(true);
            }
        }
    }

    public void setColor(bool isEnable)
    {
        border.color = isEnable ? PlayConfig.ColorGuideEnemyBorderSelected : Color.white;
    }
}
