using UnityEngine;
using System.Collections;

public enum ETargetType
{
    NONE,
    CHOOSSED,
    BUILDED,
}

public class UITarget : MonoBehaviour 
{
    // when implement when build tower
    public GameObject rootTower;
    [HideInInspector]
    public ETargetType type = ETargetType.NONE;

    public void reset()
    {
        type = ETargetType.NONE;
    }

    void OnClick()
    {
        if (type == ETargetType.NONE)
        {
            type = ETargetType.CHOOSSED;

            PlayManager.Instance.resetBuilding();
            //// reset range tower
            PlayManager.Instance.resetRangeTower();
            PlayManager.Instance.selectedTowerBuild.SetActive(false);
            PlayManager.Instance.objectBuild.Target = rootTower;
            PlayManager.Instance.resetUpgrade();

            PlayManager.Instance.chooseTarget.transform.parent = transform;
            PlayManager.Instance.chooseTarget.transform.localScale = Vector3.one;
            PlayManager.Instance.chooseTarget.transform.localPosition = new Vector3(3.5f, -30, 0);
            PlayManager.Instance.chooseTarget.transform.GetChild(0).renderer.material.renderQueue = transform.GetChild(0).renderer.material.renderQueue - 1;
            PlayManager.Instance.chooseTarget.SetActive(true);

            // set tutorial cho lan dau tien
            if (PlayerInfo.Instance.tutorialInfo.tutorial_build == 0 && WaveController.Instance.currentMap == 1 
                && SceneState.Instance.State == ESceneState.ADVENTURE)
            {
                PlayerInfo.Instance.tutorialInfo.tutorial_build = 1;
                PlayerInfo.Instance.tutorialInfo.Save();

                PlayManager.Instance.tutorial.SetActive(true);
                UIButtonTutorialPlay.Instance.StartTutorialBuildTower();
            }
        }
    }
}
