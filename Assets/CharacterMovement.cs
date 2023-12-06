using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 10.0f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float jumpSpeed = 2.0f;
    public float gravity = 10.0f;
    private Vector3 jumpForce = Vector3.zero;

    public float coyoteTime = 0.2f;
    private float coyoteCurrent = 0;

    [Header("Movimiento")]
    public CharacterController controller;
    public Transform cam;

    [Header("Animaciones")]
    public Animator animator;

    private void Start()
    {
        // Hide cursor
        Cursor.visible = false;
    }

    public void Update()
    {
        // Get input from keyboard
        var H_axis = Input.GetAxis("Horizontal");
        var V_axis = Input.GetAxis("Vertical");
        var dir = new Vector3(H_axis, 0, V_axis).normalized;

        // get jump input
        var jump = Input.GetButtonDown("Jump");
        if ((controller.isGrounded || coyoteCurrent <= coyoteTime) && jump)
        {
            // Set jump force
            jumpForce.y = jumpSpeed;

            // Set animator jump trigger
            animator.SetTrigger("Jumping");
        }

        // Set animator grounded parameter
        animator.SetBool("Grounded", controller.isGrounded);

        coyoteCurrent = 0;

        // Apply gravity
        if (!controller.isGrounded) // si no estoy en el suelo
        {
            coyoteCurrent += Time.deltaTime;

            jumpForce.y -= gravity * Time.deltaTime;
        }
        controller.Move(jumpForce * Time.deltaTime);

        if (dir.magnitude >= 0.1f)
        {
            // Set animator running parameter to true
            animator.SetBool("Running", true);

            // Rotate the character to the direction of movement
            var targetAngle = (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg) + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the character
            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        else
        {
            // Set animator running parameter to false
            animator.SetBool("Running", false);
        }
    }
}
