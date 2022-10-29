using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Google.XR.Cardboard;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class SceneControl : MonoBehaviour
{
    public int SceneIndex;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().name.Equals("Demo")==false)
        {            
            StopXR();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        SwitchScene();
        //EndWatchingSwitch();
        Timer -= Time.deltaTime;
    }
    public void SwitchScene()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (SceneIndex == 3)
            {
                SceneIndex = 0;
            }
            SceneIndex = SceneIndex + 1;
            Debug.Log(SceneIndex);
            SceneManager.LoadScene(SceneIndex);
        }
    }
    public void ButtonSwitchScene()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneIndex == 3)
        {
            SceneIndex = 0;
        }
        SceneIndex = SceneIndex + 1;
        Debug.Log(SceneIndex);
        SceneManager.LoadScene(SceneIndex);
    }
    void EndWatchingSwitch()
    {
        if (SceneManager.GetActiveScene().name.Equals("Demo"))
        {
            //Timer();
            //Invoke("BackToReception", 15f);
            if (Input.GetKeyDown(KeyCode.M))
            {
                StopXR();
            }
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
