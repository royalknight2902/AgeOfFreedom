using UnityEngine;
using System.Collections;

public class UIHouse : MonoBehaviour 
{
    [HideInInspector]
    public ETowerType type = ETowerType.NONE;

    HouseController houseController;
    HouseAction houseAction;
    TweenPosition panelTween;

    void Start()
    {
        panelTween = PlayManager.Instance.towerInfoController.GetComponent<TweenPosition>();

        foreach (Transform child in PlayManager.Instance.footerBar.transform)
        {
            if (child.name == PlayNameHashIDs.PanelTowerBuild)
            {
                GetComponent<UIPlayTween>().tweenTarget = child.gameObject;
                break;
            }
        }

        houseController = transform.parent.GetComponent<HouseController>();
        houseAction = transform.parent.GetComponent<HouseAction>();
    }

    public void reset()
    {
        type = ETowerType.NONE;
    }

    void OnClick()
    {
        if (type == ETowerType.NONE && houseAction.isActivity)
        {
            PlayManager playManager = PlayManager.Instance;
            if (playManager.objectUpgrade.Tower != houseController.gameObject)
                playManager.resetUpgrade();

            type = ETowerType.CHOOSED;

            panelTween.PlayForward();
            playManager.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayReverse();

            playManager.objectUpgrade.Tower = houseController.gameObject;
            playManager.resetBuilding();

            // set range for tower
            playManager.setRangeTower(0, this.gameObject);

            // get range of tower current
            playManager.towerInfoController.rangeCurrent = 0;

            try
            {
                DragonHouseData nextLV = ReadDatabase.Instance.DragonInfo.House[houseController.ID.Level + 1];

                playManager.towerInfoController.setHouseInfo(houseController);
                playManager.towerInfoController.setNextHouseIcon(new STowerID(ETower.DRAGON, houseController.ID.Level + 1));

                // get range of tower next level
                playManager.towerInfoController.rangeUpgrade = 0;
            }
            catch
            {
                playManager.towerInfoController.setHouseInfo(houseController);
            }

            // show tutorial upgrade neu lan dau tien su dung
            if (PlayerInfo.Instance.tutorialInfo.tutorial_upgrade == 0 && WaveController.Instance.currentMap == 1)
            {
                PlayerInfo.Instance.tutorialInfo.tutorial_upgrade = 1;
                PlayerInfo.Instance.tutorialInfo.Save();

                playManager.tutorial.SetActive(true);
                UIButtonTutorialPlay.Instance.startTutorialUpgrade();
            }
        }
    }

}
