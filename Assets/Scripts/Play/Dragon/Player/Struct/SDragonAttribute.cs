using UnityEngine;
using System.Collections;

[System.Serializable]
public class SDragonAttribute
{
	public string Name;
	public SUnitHP HP;
	public SUnitHP MP;
	public SMinMax ATK;
	public int DEF;
	public float Speed;

    public int Level;
    public SUnitHP EXP;
}

public struct SDragonAttributeBonus
{
    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public float ATKSpeed;
    public float MoveSpeed;
}

