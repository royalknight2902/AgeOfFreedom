using UnityEngine;
using System.Collections;

public class UITimeSpeed : MonoBehaviour
{
	public UILabel labelText;
	public UISlider sliderTimeSpeed;
	//public UILabel label;

	TweenPosition tweenPosition;

	void Start()
	{
		//label.text = transform.localPosition.ToString();

		tweenPosition = gameObject.GetComponent<TweenPosition>();
		tweenPosition.from = transform.localPosition;
		tweenPosition.to = transform.localPosition - new Vector3(340, 0, 0) + new Vector3(130, 0, 0);

        if (PlayerInfo.Instance.userInfo.timeScale - 1 >= 0)
        {
            sliderTimeSpeed.value = PlayerInfo.Instance.userInfo.timeScale - 1;
        }
        else
        {
            sliderTimeSpeed.value = 0;
        }

        if (sliderTimeSpeed.numberOfSteps == 0)
        {
            sliderTimeSpeed.numberOfSteps = 5;
        }
	}
}