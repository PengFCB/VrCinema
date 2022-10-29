using System.Collections;
using System.Collections.Generic;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.XR.Management;

public class Ray : MonoBehaviour
{
    Transform playerBody;
    public Vector3 beforePosition;
    private const float _defaultFieldOfView = 60.0f;

    // Main camera from the scene.
    public Camera mainCamera;
    void Start()
    {
        playerBody = this.gameObject.GetComponent<MouseMovement>().playerBody;

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

        EnterVR();
    }

    void Update()
    {
        Api.UpdateScreenParams();

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        RaycastHit hit;
        // Does the ray intersect any objects of the chair layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            // click for sitting down
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton3)) && this.gameObject.GetComponent<MouseMovement>().sitting == false)
            {
                // update the status
                playerBody.gameObject.GetComponent<PlayerMovement>().sitting = true;
                this.gameObject.GetComponent<MouseMovement>().sitting = true;
                // record the position before sitting
                beforePosition = playerBody.position;
                // rescale the playerbody to suit the chair
                playerBody.transform.localScale = new Vector3(playerBody.transform.localScale.x, 1.0f, playerBody.transform.localScale.z);
                // move the player to the chair position
                playerBody.position = hit.collider.transform.position;
            }
            // draw line for showing
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        // press Q to stand up
        if ((Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton2)) && this.gameObject.GetComponent<MouseMovement>().sitting == true)
        {
            // move the player to the position before sitting
            playerBody.position = beforePosition;
            // rescale the playerbody
            playerBody.transform.localScale = new Vector3(playerBody.transform.localScale.x, 0.8f, playerBody.transform.localScale.z);
            // update status after one frame to achieve player movement in the above line
            StartCoroutine(DelaySetting());
        }
    }

    IEnumerator DelaySetting()
    {
        yield return new WaitForFixedUpdate();
        playerBody.gameObject.GetComponent<PlayerMovement>().sitting = false;
        this.gameObject.GetComponent<MouseMovement>().sitting = false;
    }
    private void EnterVR()
    {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }

    /// <summary>
    /// Exits VR mode.
    /// </summary>
    private void ExitVR()
    {
        StopXR();
    }

    /// <summary>
    /// Initializes and starts the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    ///
    /// <returns>
    /// Returns result value of <c>InitializeLoader</c> method from the XR General Settings Manager.
    /// </returns>
    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }

    /// <summary>
    /// Stops and deinitializes the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");

        mainCamera.ResetAspect();
        mainCamera.fieldOfView = _defaultFieldOfView;
    }
}
