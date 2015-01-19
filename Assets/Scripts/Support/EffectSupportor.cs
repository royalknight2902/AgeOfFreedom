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
    const float TimeValueRun = 0.25f;

    System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();

    public void runSliderValue(UISlider slider, float valueTo)
    {
        if (!list.Contains(slider.gameObject))
        {
            list.Add(slider.gameObject);
            StartCoroutine(moveSliderValue(slider, valueTo));
        }
    }

    IEnumerator moveSliderValue(UISlider slider, float valueTo)
    {
        float value = slider.value - valueTo;
        bool isDown = false;

        if (value > 0)
            isDown = true;
        else
            isDown = false;

        float valueEachFrame = Time.deltaTime * value / TimeValueRun; // 0.5 seconds
        int fps = 0;

        while (true)
        {
			if(slider == null)
				yield return 0;
            if (fps == 60 * TimeValueRun)
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

