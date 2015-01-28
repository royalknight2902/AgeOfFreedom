using UnityEngine;
using System.Collections;

public class UITowerBuild : MonoBehaviour
{
    bool isChoosed;

    public GameObject rootTowerBuild;
    public UITexture texture;
    public UILabel money;
    public int range;
    public bool isEnable { get; set; }

    public void reset()
    {
        isChoosed = false;
    }

    void OnClick()
    {
		
        if (!isEnable) //Open shop to buy
        {
            PlayManager.Instance.openShopWithTowerSelected(rootTowerBuild.GetComponent<TowerBuildController>().ID);
        }
        else //Build
        {
            PlayManager playManager = PlayManager.Instance;

            if (isChoosed)
            {
                if (playManager.objectBuild.Tower == rootTowerBuild)
                {
                    playManager.checkOK.SetActive(false);
                    // build tower
                    playManager.buildTower();
                    playManager.rangeTower.SetActive(false);
                    isChoosed = false;

                    if (playManager.tempInit.panelDragonInfo != null)
                    {
                        if (playManager.tempInit.panelDragonInfo.activeInHierarchy)
                        {
                            playManager.tempInit.panelDragonInfo.GetComponent<TweenPosition>().PlayForward();
                        }
                    }
                }
                else
                {
                    isChoosed = false;
                    playManager.objectBuild.Tower.GetComponentInChildren<UITowerBuild>().isChoosed = false;
                    setTargetTower(playManager);
                }
            }
            else
            { // show info tower build
                setTargetTower(playManager);
            }
        }
    }

    void setTargetTower(PlayManager playManager)
    {
        if (playManager.objectBuild.Tower)
            playManager.objectBuild.PreTower = playManager.objectBuild.Tower;
        playManager.objectBuild.Tower = rootTowerBuild;

        isChoosed = true;

        if (rootTowerBuild.GetComponent<TowerBuildController>().ID.Type != ETower.DRAGON) //is tower
        {
            playManager.selectedTowerBuild.GetComponent<UISprite>().color = Color.yellow;
            playManager.checkOK.GetComponent<UISprite>().color = Color.white;
            playManager.selectedTowerBuild.GetComponent<UIStretch>().relativeSize = new Vector2(1.1f, 1.0f);
        }
        else
        {
            playManager.selectedTowerBuild.GetComponent<UISprite>().color = Color.red;
            playManager.checkOK.GetComponent<UISprite>().color = Color.red;
            playManager.selectedTowerBuild.GetComponent<UIStretch>().relativeSize = new Vector2(1.4f, 1.2f);
        }

        //selected tower build
        playManager.selectedTowerBuild.transform.localPosition = rootTowerBuild.transform.localPosition;
        playManager.selectedTowerBuild.GetComponent<UIStretch>().container = rootTowerBuild;

        //check ok
        if (!playManager.checkOK.activeSelf)
            playManager.checkOK.SetActive(true);
        playManager.checkOK.transform.parent = playManager.panelTowerBuild.transform;
        playManager.checkOK.transform.position = rootTowerBuild.transform.position;

        UIStretch stretch = playManager.checkOK.GetComponent<UIStretch>();
        stretch.container = PlayManager.Instance.panelTowerBuild;
        stretch.relativeSize.y = PlayConfig.AnchorTowerBuildCheckOK;

        // set range for tower when build tower (first)
        playManager.setRangeTower(range, playManager.objectBuild.Target);

        if (ItemManager.Instance.listItemState.Contains(EItemState.RANGE))
        {
            playManager.rangeTowerBonus.transform.position = playManager.rangeTower.transform.position;
            float scale = (float)(range + (int)(range * ItemManager.Instance.BonusRange)) / 100f;
            playManager.rangeTowerBonus.transform.localScale = new Vector3(scale, scale, 0);
            playManager.rangeTowerBonus.SetActive(true);
        }

        if (!playManager.selectedTowerBuild.activeInHierarchy)
        {
            NGUITools.SetActive(playManager.selectedTowerBuild, true);
        }
    }
}
