using UnityEngine;
using System.Collections;

public struct SBulletAnchor
{
    public Vector2 Anchor;
    public Vector2 Dimension;
    public float Stretch;

    public SBulletAnchor(Vector2 anchor, Vector2 dimension, float stretch)
    {
        Anchor = anchor;
        Dimension = dimension;
        Stretch = stretch;
    }
}
