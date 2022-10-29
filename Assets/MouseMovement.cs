using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Tooltip("The sensitivity of mouse")]
    public float mouseSensitivity = 100f;
    [Tooltip("The height of the camera when the player is sitting")]
    public float sittingHeight = -0.44f;
    [Tooltip("The height of the camera when the player is standing")]
    public float standingHeight = 0.928f;

    public Transform playerBody;
    [Tooltip("False for standing, true for sitting")]
    public bool sitting = false;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // get the input of mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // clamp the rotation 
        xRotation -= mouseY;
        if (sitting == false) {
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);    
        }
        else {
            xRotation = Mathf.Clamp(xRotation, -45f, 20f);
        }
        
        // for left n right rotation, rotate the camera instead
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
