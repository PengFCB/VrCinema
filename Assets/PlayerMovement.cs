using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The speed of movement")]
    public float maxMoveSpeed = 12.0f;
    public float gravity = -9.81f;
    [Tooltip("The distance used to check ground")]
    public float groundDistance = 0.4f;
    [Tooltip("False for standing, True for sitting")]
    public bool sitting = false;
    [Tooltip("The layer to do the ground check. Choose 'Ground'")]
    public LayerMask groundMask;
    [Tooltip("TextMeshPro used to print out execution")]
    public TextMeshProUGUI hint;

    Transform _groundCheck;
    bool isGrounded;
    Vector3 velocity;
    CharacterController _controller;
    
    void Start()
    {
        _groundCheck = transform.Find("GroundCheck");
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // if the player is sitting, then they cannot move, only show the hint
        if (sitting == true) {
            hint.text = "Press Q to stand up";
        }
        else {
            // update the hint
            hint.text = "Click Left Mouse Button To Sit";
            // check whether the player is on the ground
            isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, groundMask);

            // if the player is not on the ground, then fall down to ground
            if (isGrounded && velocity.y < 0) {
                velocity.y = -2f;
            }

            // check the input and move the player
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 dir = Vector3.ClampMagnitude(-transform.right * x + transform.forward * z, 1);
            _controller.Move(-dir * maxMoveSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            _controller.Move(velocity * Time.deltaTime);   
        }
    }
}
