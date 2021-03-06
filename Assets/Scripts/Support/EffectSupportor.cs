using UnityEngine;
using System.Collections;

public enum ESpriteType
{
    UI_SPRITE,
    UI_TEXTURE,
    SPRITE_RENDERER,
}

public class EffectSupportor : Singleton<EffectSupportor>
{
    #region RUN SLIDER VALUE

    public const float TimeValueRunHP = 0.35f;
    public const float TimeValueRunMP = 0.5f;

    System.Collections.Generic.Dictionary<GameObject, string> list = new System.Collections.Generic.Dictionary<GameObject, string>();

    public void runSliderValue(UISlider slider, float valueTo, float valueRun)
    {
        if (slider != null)
        {
            if (!list.ContainsKey(slider.gameObject))
            {
                string strValue = valueTo + "_" + valueRun;

                list.Add(slider.gameObject, strValue);
                StartCoroutine(moveSliderValue(slider, valueTo, valueRun));
            }
            else
            {
                string[] ss = list[slider.gameObject].Split('_');

                StopCoroutine(moveSliderValue(slider, float.Parse(ss[0]), float.Parse(ss[0])));
                StartCoroutine(moveSliderValue(slider, valueTo, valueRun));
            }
        }
    }

    IEnumerator moveSliderValue(UISlider slider, float valueTo, float valueRun)
    {
        float value = slider.value - valueTo;
        bool isDown = false;

        if (value > 0)
            isDown = true;
        else
            isDown = false;

        float valueEachFrame = Time.deltaTime * value / valueRun; // 0.5 seconds
        int fps = 0;

        while (true && slider != null)
        {
            if (fps == (int)(60 * valueRun))
            {
                list.Remove(slider.gameObject);
                yield break;
            }

            if (isDown)
                slider.value -= valueEachFrame;
            else
                slider.value += valueEachFrame;

            fps++;

            yield return 0;
        }
    }
    #endregion

    #region FADE OUT WITH CALLBACK EVENT
    public void fadeOutWithEvent(GameObject target, ESpriteType type, float time, EventDelegate callback)
    {
        StartCoroutine(runfadeOutWithEvent(target, type, time, callback));
    }

    IEnumerator runfadeOutWithEvent(GameObject target, ESpriteType type, float time, EventDelegate callback)
    {
        if (type == ESpriteType.SPRITE_RENDERER)
        {
            SpriteRenderer render = target.GetComponent<SpriteRenderer>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                {
                    callback.Execute();
                    yield break;
                }
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_SPRITE)
        {
            UISprite render = target.GetComponent<UISprite>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                {
                    callback.Execute();
                    yield break;
                }
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_TEXTURE)
        {
            UITexture render = target.GetComponent<UITexture>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                {
                    callback.Execute();
                    yield break;
                }
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
    }
    #endregion

    #region FADE OUT AND DESTROY OBJECT
    public void fadeOutAndDestroy(GameObject target, ESpriteType type, float time)
    {
        StartCoroutine(runfadeOutAndDestroy(target, type, time));
    }

    IEnumerator runfadeOutAndDestroy(GameObject target, ESpriteType type, float time)
    {
        if (type == ESpriteType.SPRITE_RENDERER)
        {
            SpriteRenderer render = target.GetComponent<SpriteRenderer>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_SPRITE)
        {
            UISprite render = target.GetComponent<UISprite>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_TEXTURE)
        {
            UITexture render = target.GetComponent<UITexture>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a <= 0)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a - ValueEachFrame);

                yield return 0;
            }
        }
    }
    #endregion

    #region FADE IN AND DESTROY OBJECT
    public void fadeInAndDestroy(GameObject target, ESpriteType type, float time)
    {
        StartCoroutine(runfadeInAndDestroy(target, type, time));
    }

    IEnumerator runfadeInAndDestroy(GameObject target, ESpriteType type, float time)
    {
        if (type == ESpriteType.SPRITE_RENDERER)
        {
            SpriteRenderer render = target.GetComponent<SpriteRenderer>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a >= 1)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a + ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_SPRITE)
        {
            UISprite render = target.GetComponent<UISprite>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a >= 1)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a + ValueEachFrame);

                yield return 0;
            }
        }
        else if (type == ESpriteType.UI_TEXTURE)
        {
            UITexture render = target.GetComponent<UITexture>();
            float ValueEachFrame = (float)Time.deltaTime / time;

            while (true)
            {
                if (render == null)
                    yield break;

                Color color = render.color;

                if (color.a >= 1)
                    yield break;
                else
                    render.color = new Color(color.r, color.g, color.b, color.a + ValueEachFrame);

                yield return 0;
            }
        }
    }
    #endregion
}

