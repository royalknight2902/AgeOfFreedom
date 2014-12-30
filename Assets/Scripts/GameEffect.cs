using UnityEngine;
using System.Collections;

public class GameEffect
{
    public static IEnumerator fadeIn(GameObject target, float duration)
    {
        float elapsedTime = 0.0f;
        float valueEachFrame = Time.deltaTime / duration;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= duration)
            {
                foreach (Transform child in target.transform)
                {
                    UIWidget widget = child.GetComponent<UIWidget>();
                    Color color = widget.color;
                    widget.color = new Color(color.r, color.g, color.b, 1);
                }
                yield break;
            }

            foreach(Transform child in target.transform)
            {
                UIWidget widget = child.GetComponent<UIWidget>();
                Color color = widget.color;
                widget.color = new Color(color.r, color.g, color.b, color.a + valueEachFrame);
            }

            yield return 0;
        }
    }

    public static IEnumerator fadeOut(GameObject target, float duration)
    {
        float elapsedTime = 0.0f;
        float valueEachFrame = Time.deltaTime / duration;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                foreach (Transform child in target.transform)
                {
                    UIWidget widget = child.GetComponent<UIWidget>();
                    Color color = widget.color;
                    widget.color = new Color(color.r, color.g, color.b, 0);
                }
                yield break;
            }

            foreach (Transform child in target.transform)
            {
                UIWidget widget = child.GetComponent<UIWidget>();
                Color color = widget.color;
                widget.color = new Color(color.r, color.g, color.b, color.a - valueEachFrame);
            }

            yield return 0;
        }
    }
}
