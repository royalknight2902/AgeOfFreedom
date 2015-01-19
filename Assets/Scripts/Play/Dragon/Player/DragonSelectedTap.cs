using UnityEngine;
using System.Collections;

public class DragonSelectedTap : MonoBehaviour
{
    DragonController controller;

    void Start()
    {
        controller = GetComponent<DragonController>();
    }

    void OnClick()
    {
        if (!controller.selected.activeSelf)
        {
            if (!controller.isSelected)
            {
                controller.selected.SetActive(true);
                controller.isSelected = true;

                if (!controller.selected.transform.GetChild(0).GetComponent<Animator>().enabled)
                    controller.selected.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            }
        }
        else
        {
            if(controller.isSelected)
            {
                controller.selected.SetActive(false);
                controller.isSelected = false;
            }
        }
    }
}
