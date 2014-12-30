using UnityEngine;
using System.Collections;

public class EffectPanelController : MonoBehaviour {
    public UISprite icon;
    public UISprite nameForeground;
    public UISprite nameOutline;
    public UILabel nameLabel;
    public UILabel effectText;

    public void setColor(EBulletEffect type)
    {
        Color[] colors = PlayConfig.getColorEffectPanel(type);
        nameForeground.color = colors[0];
        nameOutline.color = colors[1];
        nameLabel.gradientTop = colors[2];
        nameLabel.gradientBottom = colors[3];
        nameLabel.effectColor = colors[4];
    }

    public void setText(BulletController controller)
    {
        switch(controller.effect)
        {
            case EBulletEffect.SLOW:
                BulletSlowEffect slowEffect = controller as BulletSlowEffect;
                effectText.text = "- Slow " + (slowEffect.slowValue * 100) + "% of enemy's speed\n" +
                    "- Duration: " + slowEffect.existTime + "s";

                nameLabel.text = "SLOW";
                break;
            case EBulletEffect.BURN:
                BulletBurnEffect burnEffect = controller as BulletBurnEffect;
                effectText.text = "- Burn " + burnEffect.damageEachFrame + " HP every " + burnEffect.timeFrame + "s\n"
                    + "- Duration: " + burnEffect.existTime + "s";

                nameLabel.text = "BURN";
                break;
        }
    }
}
