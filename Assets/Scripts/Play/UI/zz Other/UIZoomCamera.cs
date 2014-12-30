using UnityEngine;
using System.Collections;

public class UIZoomCamera : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.01f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.01f;        // The rate of change of the orthographic size in orthographic mode.

    int wCameraDefault;
    int hCameraDefault;
    float rateZoom;
    bool checkSetLayer = false;

    void Start()
    {
        wCameraDefault = PlayManager.Instance.uiWidgetCamera.width;
        hCameraDefault = PlayManager.Instance.uiWidgetCamera.height;

        if ((float)PlayManager.Instance.uiTextureMap.width / wCameraDefault >= (float)PlayManager.Instance.uiTextureMap.height / hCameraDefault)
        {
            rateZoom = (float)PlayManager.Instance.uiTextureMap.height / hCameraDefault - 0.03f;
        }
        else
        {
            rateZoom = (float)PlayManager.Instance.uiTextureMap.width / wCameraDefault - 0.03f;
        }
    }

    void Update()
    {
        if (!checkSetLayer)
        {
            PlayManager.Instance.uiWidgetCamera.gameObject.layer = PlayManager.Instance.uiTextureMap.gameObject.layer;
            checkSetLayer = true;
        }

        if (Input.touchCount == 2 && PlayManager.Instance.isZoom)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).sqrMagnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).sqrMagnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (camera.isOrthoGraphic)
            {
                //camera.gameObject.transform.localPosition = Vector3.zero;
                if (deltaMagnitudeDiff >= 0 && camera.orthographicSize <= rateZoom)
                {
                    camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed / 500;
                    camera.orthographicSize = Mathf.Min(camera.orthographicSize, rateZoom);

                    PlayManager.Instance.uiWidgetCamera.width = (int)(wCameraDefault * camera.orthographicSize);
                    PlayManager.Instance.uiWidgetCamera.height = (int)(hCameraDefault * camera.orthographicSize);
                    //checkZoomCamera();
                    camera.transform.localPosition = new Vector3(0, 0, 0);
                }
                else if (deltaMagnitudeDiff < 0 && camera.orthographicSize >= 1.0f)
                {
                    camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed /500;
                    camera.orthographicSize = Mathf.Max(camera.orthographicSize, 1.0f);

                    PlayManager.Instance.uiWidgetCamera.width = (int)(wCameraDefault * camera.orthographicSize);
                    PlayManager.Instance.uiWidgetCamera.height = (int)(hCameraDefault * camera.orthographicSize);
                    //checkZoomCamera();
                }
                else
                {
                    return;
                }
            }
            else
            {
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }

    private void checkZoomCamera()
    {
        float dx = 0;
        float dy = 0;

        if (camera.gameObject.transform.localPosition.x + PlayManager.Instance.uiWidgetCamera.width / 2 > PlayManager.Instance.uiTextureMap.width / 2)
        {
            dx = camera.gameObject.transform.localPosition.x + PlayManager.Instance.uiWidgetCamera.width / 2 - PlayManager.Instance.uiTextureMap.width / 2;
        }
        else if (camera.gameObject.transform.localPosition.x - PlayManager.Instance.uiWidgetCamera.width / 2 < -PlayManager.Instance.uiTextureMap.width / 2)
        {
            dx = camera.gameObject.transform.localPosition.x - PlayManager.Instance.uiWidgetCamera.width / 2 + PlayManager.Instance.uiTextureMap.width / 2;
        }

        if (camera.gameObject.transform.localPosition.y + PlayManager.Instance.uiWidgetCamera.height / 2 > PlayManager.Instance.uiTextureMap.height / 2)
        {
            dy = camera.gameObject.transform.localPosition.y + PlayManager.Instance.uiWidgetCamera.height / 2 - PlayManager.Instance.uiTextureMap.height / 2;
        }
        else if (camera.gameObject.transform.localPosition.y - PlayManager.Instance.uiWidgetCamera.height / 2 < -PlayManager.Instance.uiTextureMap.height / 2)
        {
            dy = camera.gameObject.transform.localPosition.y - PlayManager.Instance.uiWidgetCamera.height / 2 + PlayManager.Instance.uiTextureMap.height / 2;
        }
        camera.gameObject.transform.localPosition = camera.gameObject.transform.localPosition - new Vector3(dx, dy, 0);
    }
}