using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField balance;
    // Start is called before the first frame update
    void Start()
    {
        UpdateProfile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateProfile()
    {
        username.text = Global.user.GetName();
        email.text = Global.user.GetEmail();
        balance.text = "Balance: " + Global.user.GetBalance();
    }

}
