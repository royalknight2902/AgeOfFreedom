using UnityEngine;
using System.Collections;

public struct STowerBuildConfig
{
    public int EnableFontSize;
    public Vector2 EnableAnchorOffset;
    public Color EnableLabelCost;

    public int UnableFontSize;
    public Vector2 UnableAnchorOffset;
    public Color UnableLabelCost;

    public STowerBuildConfig(int enableFontSize, Vector2 enableAnchorOffset, Color enableLabelCost,
        int unableFontSize, Vector2 unableAnchorOffset, Color unableLabelCost)
    {
        EnableFontSize = enableFontSize;
        EnableAnchorOffset = enableAnchorOffset;
        UnableFontSize = unableFontSize;
        UnableAnchorOffset = unableAnchorOffset;
        EnableLabelCost = enableLabelCost;
        UnableLabelCost = unableLabelCost;
    }
}
