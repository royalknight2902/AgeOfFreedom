using UnityEngine;
using System.Collections;

public class SkillState : FSMState<SkillController>
{
    protected SkillController controller;

    public bool hasDamage;
    public int collisionNum;

    public EBulletEffect effectType = EBulletEffect.NONE;
    public object effectValue;
    public object effectObjectID;

    public bool hasCollider;
    public object colliderValue;
    public object colliderMoveTo;
    public ESkillCollider colliderType;

    public override void Enter(SkillController obj)
    {
        if (controller == null)
            controller = obj;
    }

    public override void Execute(SkillController obj)
    {
    }

    public override void Exit(SkillController obj)
    {
        if (hasCollider)
            MonoBehaviour.Destroy(obj.transform.GetChild(0).GetComponent<Collider>());
    }

    public void goNextState()
    {
        var enumerator = controller.listState.Keys.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (controller.StateAction == enumerator.Current)
            {
                enumerator.MoveNext();
                controller.StateAction = enumerator.Current;
                break;
            }
        }
    }

    public IEnumerator runNextState(float time)
    {
        yield return new WaitForSeconds(time);
        goNextState();
        yield return 0;
    }

    public IEnumerator runDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        controller.GetComponentInChildren<AutoDestroy>().destroyParent();
        yield return 0;
    }

    public void initCollider(int frameLength, float timeFrame)
    {
        if (!hasCollider)
            return;

        Vector2 spritePivot = GameSupportor.getPivotSpriteRenderer(controller.gameObject);

        switch (colliderType)
        {
            #region BOX
            case ESkillCollider.BOX:
                BoxCollider boxCollider = controller.skillAnimation.gameObject.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;

                if (!colliderValue.ToString().Trim().Equals("full"))
                {
                    string[] ss = colliderValue.ToString().Split('/');

                    boxCollider.isTrigger = true;
                    boxCollider.center = new Vector2(0.5f - spritePivot.x + float.Parse(ss[0]), 0.5f - spritePivot.y + float.Parse(ss[1]));
                    boxCollider.size = new Vector2(float.Parse(ss[2]), float.Parse(ss[3]));
                }
                else
                {
                    boxCollider.size = new Vector2(15.0f, 15.0f);
                    //if (controller.StateAction == ESkillAction.ARMAGGEDDON)
                    //{
                    //    UIWidget widget = controller.skillAnimation.gameObject.AddComponent<UIWidget>();

                    //    foreach(Transform child in PlayManager.Instance.tempInit.cameraRender.transform)
                    //    {
                    //        if(child.name.Equals("Background"))
                    //        {
                    //            UITexture texture = child.transform.GetChild(0).GetComponent<UITexture>();
                    //            widget.SetDimensions(texture.width, texture.height);
                    //            break;
                    //        }
                    //    }
                    //    widget.autoResizeBoxCollider = true;
                    //}
                }

                if (colliderMoveTo != null)
                {
                    string[] ss = colliderMoveTo.ToString().Split('/');
                    Vector2 centerTo = new Vector2(float.Parse(ss[0].ToString()), float.Parse(ss[1].ToString()));
                    Vector2 sizeTo = new Vector2(float.Parse(ss[2].ToString()), float.Parse(ss[3].ToString()));
                    controller.skillAnimation.StartCoroutine(runBoxCollider(boxCollider, centerTo, sizeTo, frameLength, timeFrame));
                }

                break;
            #endregion
            #region CAPSULE
            case ESkillCollider.CAPSULE:
                break;
            #endregion
            #region SPHERE
            case ESkillCollider.SPHERE:
                string[] values = colliderValue.ToString().Split('/');

                SphereCollider sphereCollider = controller.skillAnimation.gameObject.AddComponent<SphereCollider>();
                sphereCollider.isTrigger = true;
                sphereCollider.center = Vector3.zero;
                sphereCollider.radius = float.Parse(values[0]);
                sphereCollider.center = new Vector2(0.5f - spritePivot.x, 0.5f - spritePivot.y);

                controller.skillAnimation.StartCoroutine(runSphereCollider(sphereCollider, float.Parse(values[1]), frameLength, timeFrame));
                break;
            #endregion
        }

        controller.skillAnimation.gameObject.AddComponent<SkillDamage>();
    }

    public IEnumerator runSphereCollider(SphereCollider sphereCollider, float valueTo, int frameLength, float timeFrame)
    {
        float elapsedTime = timeFrame;

        float f = valueTo - sphereCollider.radius;
        float valueEachFrame = f / frameLength;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeFrame)
            {
                elapsedTime = 0.0f;
                sphereCollider.radius += valueEachFrame;

                if (sphereCollider.radius >= valueTo)
                    yield break;
            }
            yield return 0;
        }
    }

    public IEnumerator runBoxCollider(BoxCollider boxCollider, Vector2 valueCenterTo, Vector2 valueSizeTo, int frameLength, float timeFrame)
    {
        float elapsedTime = timeFrame;
        float existTime = 0.0f;

        float valueEachFrameX = 0.0f, valueEachFrameY = 0.0f, valueEachFrameW = 0.0f, valueEachFrameH = 0.0f;
        bool xPlus = false, yPlus = false, wPlus = false, hPlus = false;

        #region Center X
        if (valueCenterTo.x >= 0 && boxCollider.center.x >= 0)
        {
            valueEachFrameX = (valueCenterTo.x - boxCollider.center.x) / frameLength;
            xPlus = true;
        }
        else if (valueCenterTo.x < 0 && boxCollider.center.x < 0)
        {
            valueEachFrameX = (Mathf.Abs(valueCenterTo.x) - Mathf.Abs(boxCollider.center.x)) / frameLength;
            if (valueCenterTo.x >= boxCollider.center.x)
                xPlus = true;
            else
                xPlus = false;
        }
        else if (valueCenterTo.x < 0 || boxCollider.center.x < 0)
        {
            valueEachFrameX = (((valueCenterTo.x < 0) ? Mathf.Abs(valueCenterTo.x) : valueCenterTo.x)
                + ((boxCollider.center.x < 0) ? Mathf.Abs(boxCollider.center.x) : boxCollider.center.x)) / frameLength;

            if (valueCenterTo.x < 0)
                xPlus = false;
            else if (boxCollider.center.x < 0)
                xPlus = true;
        }
        #endregion
        #region Center Y
        if (valueCenterTo.y >= 0 && boxCollider.center.y >= 0)
        {
            valueEachFrameY = (valueCenterTo.y - boxCollider.center.y) / frameLength;
            yPlus = true;
        }
        else if (valueCenterTo.y < 0 && boxCollider.center.y < 0)
        {
            valueEachFrameY = (Mathf.Abs(valueCenterTo.y) - Mathf.Abs(boxCollider.center.y)) / frameLength;
            if (valueCenterTo.y >= boxCollider.center.y)
                yPlus = true;
            else
                yPlus = false;
        }
        else if (valueCenterTo.y < 0 || boxCollider.center.y < 0)
        {
            valueEachFrameY = (((valueCenterTo.y < 0) ? Mathf.Abs(valueCenterTo.y) : valueCenterTo.y)
                + ((boxCollider.center.y < 0) ? Mathf.Abs(boxCollider.center.y) : boxCollider.center.y)) / frameLength;

            if (valueCenterTo.y < 0)
                yPlus = false;
            else if (boxCollider.center.y < 0)
                yPlus = true;
        }
        #endregion
        #region Size X
        if (valueSizeTo.x >= 0 && boxCollider.size.x >= 0)
        {
            valueEachFrameW = (valueSizeTo.x - boxCollider.size.x) / frameLength;
            wPlus = true;
        }
        else if (valueSizeTo.x < 0 && boxCollider.size.x < 0)
        {
            valueEachFrameW = (Mathf.Abs(valueSizeTo.x) - Mathf.Abs(boxCollider.size.x)) / frameLength;
            if (valueSizeTo.x >= boxCollider.size.x)
                wPlus = true;
            else
                wPlus = false;
        }
        else if (valueSizeTo.x < 0 || boxCollider.size.x < 0)
        {
            valueEachFrameW = (((valueSizeTo.x < 0) ? Mathf.Abs(valueSizeTo.x) : valueSizeTo.x)
                + ((boxCollider.size.x < 0) ? Mathf.Abs(boxCollider.size.x) : boxCollider.size.x)) / frameLength;

            if (valueSizeTo.x < 0)
                wPlus = false;
            else if (boxCollider.size.x < 0)
                wPlus = true;
        }
        #endregion
        #region Size Y
        if (valueSizeTo.y >= 0 && boxCollider.size.y >= 0)
        {
            valueEachFrameH = (valueSizeTo.y - boxCollider.size.y) / frameLength;
            hPlus = true;
        }
        else if (valueSizeTo.y < 0 && boxCollider.size.y < 0)
        {
            valueEachFrameH = (Mathf.Abs(valueSizeTo.y) - Mathf.Abs(boxCollider.size.y)) / frameLength;
            if (valueSizeTo.y >= boxCollider.size.y)
                hPlus = true;
            else
                hPlus = false;
        }
        else if (valueSizeTo.y < 0 || boxCollider.size.y < 0)
        {
            valueEachFrameH = (((valueSizeTo.y < 0) ? Mathf.Abs(valueSizeTo.y) : valueSizeTo.y)
                + ((boxCollider.size.y < 0) ? Mathf.Abs(boxCollider.size.y) : boxCollider.size.y)) / frameLength;

            if (valueSizeTo.y < 0)
                hPlus = false;
            else if (boxCollider.size.y < 0)
                hPlus = true;
        }
        #endregion

        while (true)
        {
            if (elapsedTime >= timeFrame)
            {
                elapsedTime = 0.0f;
                boxCollider.center = new Vector2(boxCollider.center.x + (xPlus ? valueEachFrameX : -(valueEachFrameX)),
                    boxCollider.center.y + (yPlus ? valueEachFrameY : -(valueEachFrameY)));
                boxCollider.size = new Vector2(boxCollider.size.x + (wPlus ? valueEachFrameW : -(valueEachFrameW)),
                    boxCollider.size.y + (hPlus ? valueEachFrameH : -(valueEachFrameH)));

                if (existTime >= (frameLength * timeFrame))
                    yield break;
            }
            elapsedTime += Time.deltaTime;
            existTime += Time.deltaTime;

            yield return 0;
        }
    }
}