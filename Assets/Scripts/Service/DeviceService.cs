using UnityEngine;
using System.Collections;

public class DeviceService : Singleton<DeviceService>
{
#if UNITY_ANDROID
    AndroidJavaObject plugin;
#endif

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start()
    {

#if UNITY_ANDROID
#if !UNITY_EDITOR
        using(var pluginClass = new AndroidJavaClass("com.unity.halfwar.HWMain"))
        {
            plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
        }
#endif
#endif
    }

    public void openToast(string text)
    {
#if UNITY_ANDROID
#if !UNITY_EDITOR
        plugin.Call("openToast", text);
#else
        Debug.Log("Toast: " + text);
#endif
#endif
    }
}
