using UnityEngine;
using System.Collections;

public class OptionController : MonoBehaviour {
    public UIToggle toggleInstruction;

    void Start()
    {
        toggleInstruction.value = System.Convert.ToBoolean(PlayerInfo.Instance.userInfo.instruction);
    }

	void OnEnable()
	{
		toggleInstruction.value = System.Convert.ToBoolean(PlayerInfo.Instance.userInfo.instruction);
	}
}
