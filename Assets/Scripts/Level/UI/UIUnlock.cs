using UnityEngine;
using System.Collections;

public class UIUnlock : MonoBehaviour
{
    [HideInInspector]
    public int starSuccess;

    void OnClick()
    {
        if (!LevelManager.Instance.mapInfoController.gameObject.activeInHierarchy)
        {
            LevelManager.Instance.mapInfoController.gameObject.SetActive(true);
            MockController controller = transform.parent.GetComponent<MockController>();

            MapInfoController mapInfoController = LevelManager.Instance.mapInfoController;
            mapInfoController.initalize(controller.mapName, controller.mapID, starSuccess);
        }
    }
}
