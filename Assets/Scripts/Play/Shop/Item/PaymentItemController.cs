using UnityEngine;
using System.Collections;

public class PaymentItemController : MonoBehaviour 
{
	public UIToggle toggleDiamond;
	public UIToggle toggleGold;
	public GameObject selectedPackage;
	public GameObject[] packages;

	[HideInInspector]
	public GameObject target;

	public EPaymentType paymentType { get; set; }

	void OnEnable()
	{
		//Enable diamond pay
		toggleDiamond.value = true;
		toggleGold.value = false;
		
		paymentType = EPaymentType.DIAMOND;

		//set target
		target = packages [0];

		if(this.gameObject.activeSelf)
		{
			target.GetComponentInChildren<UIPaymentItemShop> ().setSelectedPackage ();
		}
	}

    void Start()
    {
        selectedPackage.transform.position = packages[0].transform.position;
    }

	public void enableDiamond()
	{
		if (toggleDiamond.value)
		{
			toggleGold.value = false;
			
			paymentType = EPaymentType.DIAMOND;
		}
		else if(!toggleDiamond.value && !toggleGold.value)
			paymentType =EPaymentType.NONE;
	}

	public void enableGold()
	{
		if (toggleGold.value)
		{
			toggleDiamond.value = false;
			
			paymentType = EPaymentType.GOLD;
		}
		else if(!toggleDiamond.value && !toggleGold.value)
			paymentType =EPaymentType.NONE;
	}
}
