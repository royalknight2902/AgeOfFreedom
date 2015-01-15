using UnityEngine;
using System.Collections;

public class FixUIStretch : MonoBehaviour
{
    int count = 0;
    bool check;

    void Awake()
    {
        if (gameObject.GetComponent<UIWidget>() != null)
        {
            gameObject.GetComponent<UIWidget>().alpha = 0;
            check = true;
        }
        else
        {
            check = false;
            return;
        }
    }

    void Update()
    {
        if (check == true)
        {
            if (count < 4)
            {
                count++;
            }
            else if (count == 4)
            {
                gameObject.GetComponent<UIWidget>().alpha = 1;
                check = false;
                Destroy(this);
            }
            else
            {
                check = false;
                Destroy(this);
                return;
            }
        }
    }
}