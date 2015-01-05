using UnityEngine;
using System.Collections;

[System.Serializable]
public struct SMinMax
{
	public int Min;
	public int Max;

    public SMinMax(int min, int max)
    {
        Min = min;
        Max = max;
    }
}

