using UnityEngine;
using System.Collections;

public enum UIGuideButton
{
    TOWER,
    ENEMY,
    NOTE,
    CLICK,
}

public class UIGuide : MonoBehaviour
{
    public UIGuideButton type;

    [HideInInspector]
    public GameObject bulletEffect;

    void OnClick()
    {
        if (GuideController.Instance.guideSelected.transform.parent != this.transform)
        {
            GuideController guideController = GuideController.Instance;
            UIAnchor anchor = guideController.guideSelected.GetComponent<UIAnchor>();

            switch (type)
            {
                case UIGuideButton.TOWER:
                    audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                    audio.PlayScheduled(0.5f);

                    anchor.relativeOffset.x = PlayConfig.AnchorGuideSelectedTower;

                    guideController.regSelected.SetActive(true);
                    guideController.regInfo.SetActive(true);
                    guideController.regNote.SetActive(false);

					guideController.loadTower();
                    break;
                case UIGuideButton.ENEMY:
                    audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                    audio.PlayScheduled(0.5f);

                    anchor.relativeOffset.x = PlayConfig.AnchorGuideSelectedEnemy;

                    guideController.regSelected.SetActive(true);
                    guideController.regInfo.SetActive(true);
                    guideController.regNote.SetActive(false);

                    guideController.loadEnemy();
                    break;
                case UIGuideButton.NOTE:
                    audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                    audio.PlayScheduled(0.5f);

                    anchor.relativeOffset.x = PlayConfig.AnchorGuideSelectedNote;
                    guideController.regSelected.SetActive(false);
                    guideController.regInfo.SetActive(false);
                    guideController.regNote.SetActive(true);
                    break;
                case UIGuideButton.CLICK:
                    audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                    audio.PlayScheduled(0.5f);

                    return;
            }

            guideController.guideSelected.transform.parent = this.transform;
            guideController.guideSelected.transform.localScale = Vector3.one;
            anchor.container = this.gameObject;
            anchor.enabled = true;
        }
    }
}

