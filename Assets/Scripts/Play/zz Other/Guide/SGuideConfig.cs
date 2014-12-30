using UnityEngine;
using System.Collections;

public struct SGuideConfig
{
    public int MaxPerLine;
    public float CellWidth;
    public float CellHeight;

    public SGuideConfig(int maxPerLine, float cellWidth, float cellHeight)
    {
        MaxPerLine = maxPerLine;
        CellWidth = cellWidth;
        CellHeight = cellHeight;
    }
}
