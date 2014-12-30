using UnityEngine;
using System.Collections;

public enum EObjectUpgradeType
{
    NONE,
    SELL,
    UPGRADE,
}

public struct SObjectUpgrade
{
    public GameObject Tower;
    public EObjectUpgradeType type;
}
