using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
//using UnityEngine.XR.OpenXR;
using UnityEngine.XR.Management;


public class ChangeXRManager : MonoBehaviour
{
    public bool isVRModel = true;
    XRLoader m_SelectedXRLoader;
    public int loadIndex = 0;
    public GameObject defultCamera;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isVRModel = !isVRModel;
            if (isVRModel) StartXR(loadIndex);
            else StopXR();
        }

    }
    void StartXR(int loaderIndex)
    {
        if (m_SelectedXRLoader == null)
        {
            m_SelectedXRLoader = XRGeneralSettings.Instance.Manager.activeLoaders[1];
        }
        StartCoroutine(StartXRCoroutine());
        defultCamera.SetActive(false);
    }

    IEnumerator StartXRCoroutine()
    {
        var initSuccess = m_SelectedXRLoader.Initialize();
        if (!initSuccess)
        {
            Debug.LogError("Error initializing selected loader.");
        }
        else
        {
            yield return null;
            var startSuccess = m_SelectedXRLoader.Start();
            if (!startSuccess)
            {
                yield return null;
                m_SelectedXRLoader.Deinitialize();
            }
        }
    }

    void StopXR()
    {
        if (m_SelectedXRLoader == null)
        {
            m_SelectedXRLoader = XRGeneralSettings.Instance.Manager.activeLoaders[2];
        }
        m_SelectedXRLoader.Stop();
        m_SelectedXRLoader.Deinitialize();
        m_SelectedXRLoader = null;
        defultCamera.SetActive(true);
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }
}
