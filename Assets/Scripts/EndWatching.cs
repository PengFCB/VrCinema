using Google.XR.Cardboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class EndWatching : MonoBehaviour
{
    // Field of view value to be used when the scene is not in VR mode. In case
    // XR isn't initialized on startup, this value could be taken from the main
    // camera and stored.
    private const float _defaultFieldOfView = 60.0f;

    // Main camera from the scene.
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Saves the main camera from the scene.
        //_mainCamera = Camera.main;

        // Configures the app to not shut down the screen and sets the brightness to maximum.
        // Brightness control is expected to work only in iOS, see:
        // https://docs.unity3d.com/ScriptReference/Screen-brightness.html.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        // Checks if the device parameters are stored and scans them if not.
        // This is only required if the XR plugin is initialized on startup,
        // otherwise these API calls can be removed and just be used when the XR
        // plugin is started.
        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
    }

    // Update is called once per frame
    void Update()
    {
        EndWatchingSwitch();
    }
    void EndWatchingSwitch()
    {
        if (SceneManager.GetActiveScene().name.Equals("Demo"))
        {
            //Timer();
            //Invoke("BackToReception", 10f);
        }
        if (SceneManager.GetActiveScene().name.Equals("reception_scene"))
        {
            StopXR();
        }
    }
    void BackToReception()
    {
        StopXR();
        SceneManager.LoadScene("reception_scene");
    }
    void SetTimer()
    {

    }
    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");

        //_mainCamera.ResetAspect();
        //_mainCamera.fieldOfView = _defaultFieldOfView;
    }
}
