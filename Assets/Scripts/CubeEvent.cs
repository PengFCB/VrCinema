using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PointerEnter()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        //this.transform.localScale += new Vector3(1f, 1f, 1f);
    }
    public void PointerExit()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.green;
        //this.transform.localScale -= new Vector3(-1f, -1f, -1f);
    }
    public void PointerClick()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
        //this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
