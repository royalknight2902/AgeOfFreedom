using UnityEngine;
using System.Collections;

public class SPlayInit
{
	public GameObject uiRoot;
	public GameObject cameraRender;
	public GameObject panelDragonInfo;
	public GameObject towerDown;
	public GameObject towerUp;
    public GameObject FooterTowerBuildDragonInfo;

	public void initalize()
	{
		initUIRoot ();
		initCameraRender ();
		initTowerDownUp ();
	}

	public void initalizeAdventureMode()
	{
        initFooterTowerBuildDragonInfo();
	}

	void initUIRoot()
	{
		uiRoot = GameObject.FindGameObjectWithTag ("Root");
	}

	void initCameraRender()
	{
		cameraRender = GameObject.FindGameObjectWithTag ("CameraRender");
	}

	void initTowerDownUp()
	{
		foreach(Transform child_1 in uiRoot.transform)
		{
			if(child_1.name.Equals("UI Render"))
			{
				foreach(Transform child_2 in child_1)
				{
					if(child_2.name.Equals("Towers"))
					{
						foreach(Transform child_3 in child_2)
						{
							if(child_3.name.Equals("Towers Down"))
								towerDown = child_3.gameObject;
							else if(child_3.name.Equals("Towers Up"))
								towerUp = child_3.gameObject;
						}
						break;
					}
				}
				break;
			}
		}
	}

    void initFooterTowerBuildDragonInfo()
    {
        FooterTowerBuildDragonInfo = PlayManager.Instantiate(Resources.Load<GameObject>("Prefab/Play/FooterTowerBuildDragonInfo")) as GameObject;
        FooterTowerBuildDragonInfo.transform.parent = PlayManager.Instance.towerInfoController.transform;
        FooterTowerBuildDragonInfo.transform.localScale = Vector3.one;
        FooterTowerBuildDragonInfo.name = "Dragon Info";

        FooterTowerBuildDragonInfo.GetComponent<UIStretch>().container = PlayManager.Instance.towerInfoController.gameObject;

        foreach (Transform child in PlayManager.Instance.towerInfoController.transform)
        {
            if (child.name.Equals("Upgrade"))
            {
                FooterTowerBuildDragonInfo.GetComponent<UIAnchor>().container = child.gameObject;
                break;
            }
        }
    }
}

