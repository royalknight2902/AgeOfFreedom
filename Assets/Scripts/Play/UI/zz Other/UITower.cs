using UnityEngine;
using System.Collections;

public enum ETowerType
{
    NONE,
    CHOOSED,
    UPGRADED,
    MAX_LEVEL,
}

public class UITower : MonoBehaviour
{
    [HideInInspector]
    public ETowerType type = ETowerType.NONE;

    TowerController towerController;
    TowerAction towerAction;
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

        GameObject parent = this.gameObject.transform.parent.gameObject;
        towerController = parent.GetComponent<TowerController>();
        towerAction = parent.GetComponent<TowerAction>();
    }

    public void reset()
    {
        type = ETowerType.NONE;
    }

    void OnClick()
    {
        if (type == ETowerType.NONE && towerAction.isActivity)
        {
            PlayManager playManager = PlayManager.Instance;
            if (playManager.objectUpgrade.Tower != towerController.gameObject)
                playManager.resetUpgrade();

            type = ETowerType.CHOOSED;

            panelTween.PlayForward();
            playManager.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayReverse();

            playManager.objectUpgrade.Tower = towerController.gameObject;
            playManager.resetBuilding();

            // set range for tower
            playManager.setRangeTower(towerController.attribute.Range, this.gameObject);

            // get range of tower current
            playManager.towerInfoController.rangeCurrent = towerController.attribute.Range;

            TowerController nextLV = towerController.nextLevel;

            if (nextLV != null)
            {
                playManager.towerInfoController.setTowerInfo(towerController);

                playManager.towerInfoController.setNextTowerIcon(nextLV.ID);
                playManager.towerInfoController.setBulletInfo(towerController.ID, towerController.attackType.ToString(), towerController.bullet);
                // get range of tower next level
                playManager.towerInfoController.rangeUpgrade = nextLV.attribute.Range;
            }
            else
            {
                playManager.towerInfoController.setTowerInfo(towerController);
                playManager.towerInfoController.setBulletInfo(towerController.ID, towerController.attackType.ToString(), towerController.bullet);
            }

            // show tutorial upgrade neu lan dau tien su dung
            if (PlayerInfo.Instance.tutorialInfo.tutorial_upgrade == 0 && WaveController.Instance.currentMap == 1
                && SceneState.Instance.State == ESceneState.ADVENTURE)
            {
                PlayerInfo.Instance.tutorialInfo.tutorial_upgrade = 1;
                PlayerInfo.Instance.tutorialInfo.Save();

                playManager.tutorial.SetActive(true);
                UIButtonTutorialPlay.Instance.startTutorialUpgrade();
            }
        }
    }
}
