using UnityEngine;
using System.Collections;

public enum ETutorialLevel
{
    DAILY_REQUEST,
    ACHIVEMENT,
    TO_PLAY,
    SKIP,
}

public class UIButtonTutorialLevel : MonoBehaviour {

    public ETutorialLevel type = ETutorialLevel.DAILY_REQUEST;

    UITutorialAll uiTutorialAll;
    GameObject arrowTutorialAll;
    GameObject contentTutorialAll;

    int count = 0;

    void Start()
    {
        count = 0;

        uiTutorialAll = LevelManager.Instance.tutorial.GetComponent<UITutorialAll>();
        arrowTutorialAll = uiTutorialAll.arrow;
        contentTutorialAll = uiTutorialAll.content;

        if (type == ETutorialLevel.DAILY_REQUEST)
        {
            arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowDaily.x, TutorialDetailConfig.AnchorArrowDaily.y);
            arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowDaily.z);
            contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentDaily.x, TutorialDetailConfig.AnchorContentDaily.y);
            contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentDaily.z);
            uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentDaily;
        }
                
    }

    void Update()
    {
        if (count <= 9)
        {
            count++;
        }
        else if (count == 10)
        {
            arrowTutorialAll.GetComponent<UIAnchor>().enabled = false;
            contentTutorialAll.GetComponent<UIAnchor>().enabled = false;
            count++;
        }
        
    }

    void OnClick()
    {
        switch (type)
        {
            case ETutorialLevel.DAILY_REQUEST:
                type = ETutorialLevel.ACHIVEMENT;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowAchievement.x, TutorialDetailConfig.AnchorArrowAchievement.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowAchievement.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentAchievement.x, TutorialDetailConfig.AnchorContentAchievement.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentAchievement.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentAchievement;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                count = 0;
                break;
            case ETutorialLevel.ACHIVEMENT:
                type = ETutorialLevel.TO_PLAY;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowUnlock.x, TutorialDetailConfig.AnchorArrowUnlock.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowUnlock.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentUnlock.x, TutorialDetailConfig.AnchorContentUnlock.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentUnlock.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentUnlock;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                count = 0;
                break;
            case ETutorialLevel.TO_PLAY:
                LevelManager.Instance.tutorial.SetActive(false);
                break;
            case ETutorialLevel.SKIP:
                LevelManager.Instance.tutorial.SetActive(false);
                break;
        }
    }

    // hieu ung di chuyen  mui ten cua tutorial
    void tweenPositionArrow()
    {
        TweenPosition tweenPosition = arrowTutorialAll.GetComponent<TweenPosition>();
        tweenPosition.from = arrowTutorialAll.transform.localPosition;

        // goc la 90
        if (arrowTutorialAll.transform.eulerAngles.z >= 80.0 && arrowTutorialAll.transform.eulerAngles.z <= 100.0)
        {
            tweenPosition.to = tweenPosition.from - new Vector3(0, 5, 0);

        }
        // 180
        else if (arrowTutorialAll.transform.eulerAngles.z >= 170.0 && arrowTutorialAll.transform.eulerAngles.z <= 190.0)
        {
            tweenPosition.to = tweenPosition.from + new Vector3(5, 0, 0);

        }
        // 0
        else if (arrowTutorialAll.transform.eulerAngles.z >= -10.0 && arrowTutorialAll.transform.eulerAngles.z <= 10.0)
        {
            tweenPosition.to = tweenPosition.from - new Vector3(5, 0, 0);

        }
        // -90 (270)
        else if (arrowTutorialAll.transform.eulerAngles.z >= 260.0 && arrowTutorialAll.transform.eulerAngles.z <= 280.0)
        {
            tweenPosition.to = tweenPosition.from + new Vector3(0, 5, 0);
        }
        else
        {
            return;
        }
    }
}
