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
    public bool isLookLeft;
    
    public Transform handToPickPosition;
    public float playerSpeed;

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("throwable"))
        {
            /*collision.gameObject.transform.SetParent(transform);
            collision.gameObject.transform.parent = handToPickPosition.transform;

            collision.gameObject.TryGetComponent(out Rigidbody2D objectRb);
            objectRb.bodyType = RigidbodyType2D.Kinematic;*/
            //objectRb.AddForce(Vector2.one * 10, ForceMode2D.Impulse);
        }
    }

}
