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
        SpriteRenderer sprite = null;

        foreach (Transform child in this.transform)
        {
            if (child.name.Equals("Collider"))
                widget = child.GetComponent<UIWidget>();
            if (child.name.Equals("Animation"))
                sprite = child.GetComponent<SpriteRenderer>();
        }

        switch (controller.ID.Level)
        {
            case 1:
                widget.SetDimensions((int)(PlayConfig.SizeDragonHouse1.x * sprite.transform.localScale.x / 100),
                    (int)(PlayConfig.SizeDragonHouse1.y * sprite.transform.localScale.y / 100));
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse1;
                break;
            case 2:
                widget.SetDimensions((int)(PlayConfig.SizeDragonHouse2.x * sprite.transform.localScale.x / 100),
                    (int)(PlayConfig.SizeDragonHouse2.y * sprite.transform.localScale.y / 100));
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse2;
                break;
            case 3:
                widget.SetDimensions((int)(PlayConfig.SizeDragonHouse3.x * sprite.transform.localScale.x / 100),
                    (int)(PlayConfig.SizeDragonHouse3.y * sprite.transform.localScale.y / 100));
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse3;
                break;
            case 4:
                widget.SetDimensions((int)(PlayConfig.SizeDragonHouse4.x * sprite.transform.localScale.x / 100),
                    (int)(PlayConfig.SizeDragonHouse4.y * sprite.transform.localScale.y / 100));
                widget.transform.localPosition = PlayConfig.PositionColliderDragonHouse4;
                break;
        }
    }
}