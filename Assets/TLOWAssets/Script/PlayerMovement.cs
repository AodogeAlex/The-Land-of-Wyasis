using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float jumpAirMultiplier;
    public bool readyToJump = true;

    [Header("Key binds")]
    public KeyCode jumpKey = KeyCode.Space;



    [Header("Ground check")]
    public float playerHeight;
    public float halfPlayerHeight = 0.5f;
    public float groundPlus = 0.2f;
    public LayerMask whatIsGround;
    public bool isGrounded;

    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 moveDirection;
    public Rigidbody rb;
    public float rbForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * halfPlayerHeight + groundPlus, whatIsGround);


        MyInput();
        SpeedControl();
        
        //When to jump
        if(Input.GetKey(jumpKey)&&readyToJump&&isGrounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetToJump), jumpCooldown);
        }


        //Handle drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        //Calculate move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //On Ground
        if(isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * rbForce,ForceMode.Force);

        else if(!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * rbForce*jumpAirMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limit ve;pcoyu of needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetToJump()
    {
        readyToJump = true;
    }
}
