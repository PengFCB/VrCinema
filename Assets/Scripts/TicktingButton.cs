using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicktingButton : MonoBehaviour
{
    public GameObject OpenButton;
    // Start is called before the first frame update
    void Start()
    {
        OpenButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTicktingButton()
    {
        OpenButton.SetActive(true);
    }
    public void HideTicktingButton()
    {
        OpenButton.SetActive(false);
    }
}
