using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private float horizontalInput;
    private float verticalInput;

    private float speedY;
    private bool isGrounded;
    
    public float playerSpeed;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("HideAppear");

        //f do teclado ou B do controle do xbox
        if(Input.GetButtonDown("Pick"))
        {
            print("Pegou");
        }

        //r do teclado ou B do controle do xbox
        if(Input.GetButtonDown("Interact"))
        {
            print("Interact");
        }

        if(Input.GetButtonDown("Diary"))
        {
            print("Diary");
        }

        if(Input.GetButtonDown("Pause"))
        {
            print("Pause");
        }

        if(verticalInput > 0)
        {
            print("Appear");
        } 
        else if (verticalInput < 0)
        {
            print("Hide");
        }

        Move();
    }


    private void Move()
    {
        
        speedY = playerRb.velocity.y;

        playerRb.velocity = new Vector2(horizontalInput * playerSpeed, speedY);

    }

}
