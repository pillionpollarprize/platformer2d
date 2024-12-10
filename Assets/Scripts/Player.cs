using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MPE;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10f;
    public float jumpHeight = 3f;
    public float dashSpeed = 30;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    [Header("Jump")]
    public Transform groundCheck; // player legs
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    [Header("Jump Mechanics")]
    public float coyoteTime = 0.3f;
    public float jumpBufferTime = 0.2f;
    public int maxJumps = 2;
    public AudioClip jumpSnd;

    private int jumpsLeft;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    private float dashTime;
    private float dashCooldownTime;
    private bool dashReady;
    
    private bool isGrounded;

    public Rigidbody2D rb;
    private AudioSource audSrc;
    private float inputX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            jumpsLeft = maxJumps;
        }
        else coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else jumpBufferCounter -= Time.deltaTime;

        // dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0)
        {
            dashReady = true;
            dashTime = dashDuration;
            dashCooldownTime = dashCooldown;
        }
        if (dashReady)
        {
            rb.velocity = new Vector2(inputX * dashSpeed, rb.velocity.y);
            dashTime -= Time.deltaTime;

            if(dashTime <= 0)
            {
                dashReady = false;
            }
            dashCooldownTime -= Time.deltaTime;
        }
        // simple jump
        if ((coyoteTimeCounter > 0 || jumpsLeft > 0) && jumpBufferCounter > 0) 
        {
            audSrc.PlayOneShot(jumpSnd);
            jumpBufferCounter = 0; //prevent infinite jump
            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            if (!isGrounded)
            {
                jumpsLeft--;
            }
        }
    }
    private void FixedUpdate()
    {
        if (!dashReady) rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }
}
