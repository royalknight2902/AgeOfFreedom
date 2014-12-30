using UnityEngine;
using System.Collections;

public enum ETowerInfoType
{
	RESET,
	SELL,
	UPGRADE,
	LOCK,
}

public class UITowerInfo : MonoBehaviour
{
	public ETowerInfoType type;

	void OnClick()
	{
		PlayManager playManager = PlayManager.Instance;

		switch (type)
		{
			case ETowerInfoType.SELL:
				// if implement sell
				if (playManager.objectUpgrade.type == EObjectUpgradeType.SELL)
				{
					playManager.towerInfoController.hasClickUpgrade = false;
					playManager.sellTower();
					playManager.resetRangeTower();

                    if (playManager.tempInit.panelDragonInfo.activeInHierarchy)
                    {
                        playManager.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();
                    }

				}
				else // if choose sell
				{
					playManager.objectUpgrade.type = EObjectUpgradeType.SELL;

                    //Set check OK
                    playManager.checkOK.transform.parent = playManager.panelTowerUpgrade.transform;
                    playManager.checkOK.transform.position = this.transform.GetChild(0).transform.position;

                    UIStretch stretch = playManager.checkOK.GetComponent<UIStretch>();
                    stretch.container = PlayManager.Instance.panelTowerBuild;
                    stretch.relativeSize.y = PlayConfig.AnchorTowerBuildCheckOK;

                    //Set check OK
                    if (!playManager.checkOK.activeSelf)
                    {
                        playManager.checkOK.SetActive(true);
                        playManager.checkOK.GetComponent<UISprite>().color = Color.white;
                    }

					// set range for current tower
					playManager.setRangeTower(playManager.towerInfoController.rangeCurrent, playManager.objectUpgrade.Tower);
					playManager.towerInfoController.setSelected(type);

					if (playManager.towerInfoController.hasClickUpgrade)
					{
						setCurrentTower();
						playManager.towerInfoController.hasClickUpgrade = false;
					}
				}
				break;
			case ETowerInfoType.UPGRADE:
				// if implement upgrade tower
				if (playManager.objectUpgrade.type == EObjectUpgradeType.UPGRADE)
				{
					playManager.towerInfoController.hasClickUpgrade = false;
					playManager.upgradeTower();

                    if (playManager.tempInit.panelDragonInfo.activeInHierarchy)
                    {
                        playManager.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();
                    }
				}
				// if choose upgrade tower
				else
				{
					playManager.objectUpgrade.type = EObjectUpgradeType.UPGRADE;
                    
                    //Set check OK
                    if (!playManager.checkOK.activeSelf)
                    {
                        playManager.checkOK.SetActive(true);
                        playManager.checkOK.GetComponent<UISprite>().color = Color.white;
                    }

                    playManager.checkOK.transform.parent = playManager.panelTowerUpgrade.transform;
                    playManager.checkOK.transform.position = this.transform.GetChild(0).transform.position;

                    UIStretch stretch = playManager.checkOK.GetComponent<UIStretch>();
                    stretch.container = PlayManager.Instance.panelTowerBuild;
                    stretch.relativeSize.y = PlayConfig.AnchorTowerBuildCheckOK;

					// set range for tower upgrade
					playManager.setRangeTower(playManager.towerInfoController.rangeUpgrade, playManager.objectUpgrade.Tower);
					playManager.towerInfoController.setSelected(type);

					// set info to next tower
					if (!playManager.towerInfoController.hasClickUpgrade)
					{
						setNextTowerInfo();
						playManager.towerInfoController.hasClickUpgrade = true;
					}
				}
				break;
			case ETowerInfoType.LOCK:
				break;
		}
	}

	void 
        setNextTowerInfo()
	{
        if (PlayManager.Instance.towerInfoController.isOnTowerInfo)
        {
            TowerController nextTower = PlayManager.Instance.objectUpgrade.Tower.GetComponent<TowerController>().nextLevel.GetComponent<TowerController>();

            PlayManager.Instance.towerInfoController.setNextTowerInfo(nextTower);
            PlayManager.Instance.towerInfoController.setBulletInfo(nextTower.ID, nextTower.attackType.ToString(), nextTower.bullet);
        }
        else
        {
            //PlayManager.Instance.towerInfoController.setNextHouseIcon(new STowerID(ETower.DRAGON, 
            //    PlayManager.Instance.objectUpgrade.Tower.GetComponent<HouseController>().ID.Level));

        }   
	}

	void setCurrentTower()
	{
        if (PlayManager.Instance.towerInfoController.isOnTowerInfo)
        {
            TowerController towerController = PlayManager.Instance.objectUpgrade.Tower.GetComponent<TowerController>();

            PlayManager.Instance.towerInfoController.setTowerInfo(towerController);
            PlayManager.Instance.towerInfoController.setBulletInfo(towerController.ID, towerController.attackType.ToString(), towerController.bullet);
        }
        else
        {
            PlayManager.Instance.towerInfoController.setHouseInfo(PlayManager.Instance.objectUpgrade.Tower.GetComponent<HouseController>());
        }
	}
}
