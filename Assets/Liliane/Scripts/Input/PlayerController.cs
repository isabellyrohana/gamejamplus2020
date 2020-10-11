using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private float horizontalInput;
    private float speedY;
    private bool isGrounded;
    

    public float playerSpeed;
    public float forceJump;
    public float distance = 1;
    public bool doubleJump;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        speedY = playerRb.velocity.y;

        playerRb.velocity = new Vector2(horizontalInput * playerSpeed, speedY);

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && doubleJump)
        {
            if (!isGrounded)
            {
                doubleJump = false;
            }

            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(Vector2.up * forceJump);
        }

        Debug.DrawRay(groundCheckLeft.position, Vector2.down * distance, isGrounded ? Color.yellow : Color.red);
        Debug.DrawRay(groundCheckRight.position, Vector2.down * distance, isGrounded ? Color.yellow : Color.red);

    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, distance, 1 << LayerMask.NameToLayer("Ground"))
        || Physics2D.Raycast(groundCheckRight.position, Vector2.down, distance, 1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded && !doubleJump)
        {
            doubleJump = true;
        }
    }

}
