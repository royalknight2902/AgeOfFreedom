using UnityEngine;
using System.Collections;

public class AutoTower : MonoBehaviour {
    [HideInInspector]
    public GameObject Tower;

    public void showTarget()
    {
        //Visible target
        foreach (Transform child in Tower.transform.parent.transform)
        {

            if (child.name == PlayNameHashIDs.Target)
                child.gameObject.SetActive(true);
        }

        //Delete object tower
        Destroy(Tower);

        //PlayManager.Instance.resetBuilding();
        //PlayManager.Instance.resetUpgrade();
    }
}
