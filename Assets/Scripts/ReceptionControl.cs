using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionControl : MonoBehaviour
{
    public TicktingButton TicketBotton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TicketBotton.ShowTicktingButton();
    }
    private void OnTriggerExit(Collider other)
    {
        TicketBotton.HideTicktingButton();
    }
}
