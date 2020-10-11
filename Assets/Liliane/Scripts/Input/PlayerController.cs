﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private float horizontalInput;
    private float verticalInput;

    private float speedY;
    public bool isLookLeft;
    
    public Transform handToPickPosition;
    public float playerSpeed;

    public bool playerVisible;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        
        verticalInput = Input.GetAxis("HideAppear");

        //f do teclado ou A do controle do xbox
        /*if(Input.GetButtonDown("Pick"))
        {
            print("Pegou");
        }*/

        //r do teclado ou B do controle do xbox
        if(Input.GetButtonDown("Interact"))
        {
            print("Interact");
        }

        //shift do teclado ou Y do controle do xbox
        if(Input.GetButtonDown("Diary"))
        {
            print("Diary");
        }

        //esc teclado ou pause do controle do xbox
        if(Input.GetButtonDown("Pause"))
        {
            print("Pause");
        }

        //direcional para cima ou para baixo ou stick up/down do xbox
        if(verticalInput > 0)
        {
            print("Appear");
        } 
        else if (verticalInput < 0)
        {
            print("Hide");
        }
    }


    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        speedY = playerRb.velocity.y;

        playerRb.velocity = new Vector2(horizontalInput * playerSpeed, speedY);

        if(horizontalInput > 0 && isLookLeft)
        {
            Flip();
        }
        else if(horizontalInput < 0 && !isLookLeft)
        {
            Flip();
        }

    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    public bool GetPlayerVisible()
    {
        return playerVisible;
    }

    public void UpdatePlayerVisible(bool status)
    {
        playerVisible = status;
    }

}
