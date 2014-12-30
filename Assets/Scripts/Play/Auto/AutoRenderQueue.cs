using UnityEngine;
using System.Collections;

public class AutoRenderQueue : MonoBehaviour
{
    public int queue;
    public bool autoOnChildren = false;

    void Awake()
    {
        if (autoOnChildren)
        {
            SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer render in renders)
            {
                render.material.renderQueue = GameConfig.RenderQueueDefault + queue;
            }
        }
        else
            GetComponent<SpriteRenderer>().material.renderQueue = GameConfig.RenderQueueDefault + queue;
    }
}
