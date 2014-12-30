using UnityEngine;
using System.Collections;

public class NextWaveController : MonoBehaviour
{
    public UISprite background;
    public UISprite border;
    public UILabel label;
    public UILabel countdown;
    public UISprite boss;

    TweenPosition tweenPosition;

    void Start()
    {
        tweenPosition = GetComponent<TweenPosition>();
    }

    public void setColor(bool isBoss)
    {
        if (!isBoss)
        {
            background.color = PlayConfig.ColorNextWaveBackground;
            border.color = PlayConfig.ColorNextWaveOutline;
            label.effectColor = PlayConfig.ColorNextWaveLabelOutline;
            countdown.color = PlayConfig.ColorNextWaveCountdownForeground;
            countdown.effectColor = PlayConfig.ColorNextWaveCountdownOutline;
            boss.gameObject.SetActive(false);
        }
        else
        {
            background.color = PlayConfig.ColorBossWaveBackground;
            border.color = PlayConfig.ColorBossWaveOutline;
            label.effectColor = PlayConfig.ColorBossWaveLabelOutline;
            countdown.color = PlayConfig.ColorBossWaveCountdownForeground;
            countdown.effectColor = PlayConfig.ColorBossWaveCountdownOutline;
            boss.gameObject.SetActive(true);
        }
    }
    public void tween(bool isEnable)
    {
        if (isEnable)
        {
            tweenPosition.enabled = true;
            tweenPosition.PlayForward();
        }
        else
        {
            tweenPosition.enabled = false;
            tweenPosition.PlayReverse();
        }
    }
}
