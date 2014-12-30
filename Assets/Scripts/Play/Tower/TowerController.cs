using UnityEngine;
using System.Collections;

public enum EBulletChaseType
{
	CHASE,
	DROP,
	EXPLOSION,
	BOMB,
	ARCHITECT,
    POISON,
}

public enum EBulletAttackType
{
	SINGLE_LAND,
	SINGLE_AIR,
	SINGLE_ALL,
	MULTIPLE_LAND,
}

public enum EShootAmount
{
	SINGLE,
	DOUBLE,
	TRIPLE,

}

public enum EBranchGame
{

	IRON, //Kim
	PLANT, //Moc
	ICE, //Thuy
	FIRE, //Hoa
    EARTH, //Tho
}

[System.Serializable]
public class TowerController : MonoBehaviour
{
	public STowerID ID;
	public STowerAttribute attribute;
	public GameObject bullet;
	public GameObject appearBullet;
	public TowerController nextLevel;
	public EBulletChaseType chaseType;
	public EBulletAttackType attackType;
	public EShootAmount shootType;
    public EBranchGame Branch;

	public int totalMoney {get; set;}
    [HideInInspector]
    public EBranchGame Type;

	[HideInInspector] public STowerBonus Bonus;
	[HideInInspector] protected UISlider uiSlider;

	void Awake()
	{
		uiSlider = GetComponentInChildren<UISlider>();
		uiSlider.value = 0;

		Bonus = new STowerBonus ();
	}

	public void updateTotalMoney(int money)
	{
		totalMoney += money;
	}

	public void updateBuilding(float value)
	{
		uiSlider.value += value;
	}

	public float getBuildingValue()
	{
		return uiSlider.value;
	}

	public void enableHealth(bool enable)
	{
		uiSlider.gameObject.SetActive(enable);
	}
}
