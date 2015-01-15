using UnityEngine;
using System.Collections;

public class HouseAction : MonoBehaviour 
{
    public GameObject countdown;
    public UISprite countdownForeground;
    public UILabel countdownLabel;

    HouseController controller;

    public bool isActivity { get; set; }

    void Start()
    {
        controller = GetComponent<HouseController>();
        setBoxCollider();
    }

    void setBoxCollider()
    {
        UIWidget widget = null;
        SpriteRenderer rend = null;
        

        foreach (Transform child in this.transform)
        {
            if (child.name.Equals("Collider"))
                widget = child.GetComponent<UIWidget>();
            if (child.name.Equals("Animation"))
                rend = child.GetComponent<SpriteRenderer>();
        }

        widget.SetDimensions((int)(rend.sprite.texture.width * rend.transform.localScale.x / 100),
                   (int)(rend.sprite.texture.width * rend.transform.localScale.y / 100));

        switch (controller.ID.Level)
        {
            case 1:
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse1;
                break;
            case 2:
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse2;
                break;
            case 3:
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse3;
                break;
            case 4:
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse4;
                break;
        }
    }
}