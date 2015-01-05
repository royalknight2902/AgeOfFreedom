

using UnityEngine;


[AddComponentMenu("Level/DragonItems/UI/Drag and Drop Surface (Dragon Item)")]

public class DragDropDragonItemSurface : MonoBehaviour
{
    public bool rotatePlacedObject = false;

    void OnDrop(GameObject go)
    {
        DragDropDragonItem ddo = go.GetComponent<DragDropDragonItem>();

        if (ddo != null)
        {
            //GameObject child = NGUITools.AddChild(gameObject, ddo.prefab);

            //Transform trans = child.transform;
            //trans.position = UICamera.lastHit.point;
            //if (rotatePlacedObject) trans.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
            Destroy(go);
        }
    }
}
