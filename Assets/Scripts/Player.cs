using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10f;
    public float jumpHeight = 3f;
    [Header("Jump")]
    public Transform groundCheck; // player legs
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    [Header("Jump Mechanics")]
    public float coyoteTime = 0.3f;
    public float jumpBufferTime = 0.2f;

    private bool doubleJumpReady = false;
    private bool dashReady = false;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;

    private bool isGrounded;

    public Rigidbody2D rb;
    private float inputX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            doubleJumpReady = true;
        }
        else coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            dashReady = true;
        }
        else jumpBufferCounter -= Time.deltaTime;

        // dash
        if (Input.GetButtonDown("Fire3") && coyoteTimeCounter > 0 && jumpBufferCounter < 0 && dashReady)
        {
            print("Dash");
            rb.velocity = new Vector2(5, rb.velocity.y);
            dashReady = false;
        }
        // double jump
        if (coyoteTimeCounter < 0 && jumpBufferCounter > 0 && doubleJumpReady)
        {
            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            doubleJumpReady = false;    
        }

        // simple jump
        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0) 
        {
            jumpBufferCounter = 0; //prevent infinite jump
            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }
}
