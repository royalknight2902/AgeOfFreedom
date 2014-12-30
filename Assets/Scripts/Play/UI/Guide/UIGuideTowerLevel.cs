using UnityEngine;
using System.Collections;

public class UIGuideTowerLevel : MonoBehaviour
{
    public int Level;

    void OnClick()
    {
        GuideController.Instance.setLevel(Level);
    }
}
