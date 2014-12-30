using UnityEngine;
using System.Collections;

public enum EDirection
{
    LEFT,
    RIGHT,
    BOTTOM,
    UP,
    NONE,
}

public class PathController : MonoBehaviour
{
    public EDirection Direction;
}