using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Global.GetUserFromDatabase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
