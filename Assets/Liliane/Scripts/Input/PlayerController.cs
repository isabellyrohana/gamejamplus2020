using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private float horizontalInput;
    private float verticalInput;

    private float speedY;
    public bool isLookLeft = false;
    
    public Transform handToPickPosition;
    public float playerSpeed;

    public bool playerVisible;

    AudioSource PassosSound;

    //Paulo
    [SerializeField]
    InventoryObject currentThrowableObject;

    [SerializeField]
    GameObject currentSafePlace;

    InventoryObject currentTouchedObject;

    public Transform rightHandPosition;

    AudioClip morteAudioPorco;

    Animator anim;

    public GameObject pistaPanel;

    public GameObject papelzin;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        PassosSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        
        verticalInput = Input.GetAxis("HideAppear");

        //f do teclado ou A do controle do xbox
        if (Input.GetButtonDown("Pick"))
        {
            //Se tiver encostado, pega o Item
            if (currentTouchedObject != null)

                PickItem();

            else
                if (currentThrowableObject != null)
                //Se tiver segurando algum item, arremessa
                ThrowObject();
        }

        //r do teclado ou B do controle do xbox
        if (Input.GetButtonDown("Interact"))
        {
            //print("Interact");
            if(papelzin != null)
            {
                papelzin.GetComponent<SpriteRenderer>().enabled = false;
                pistaPanel.SetActive(true);
            }

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
        if (verticalInput > 0)
        {
            if (currentSafePlace != null)
            {
                Hide();
            }
        }
        else if (verticalInput < 0)
        {
            ShowUp();
        }
    }


    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        speedY = playerRb.velocity.y;

        playerRb.velocity = new Vector2(horizontalInput * playerSpeed, speedY);

        if (horizontalInput > 0.5f)
        {
            anim.SetTrigger("TriggerWalkIdle");
            if (isLookLeft) Flip(isLookLeft);
            if (!PassosSound.isPlaying)
                PassosSound.Play();
        }
        else if(horizontalInput < -0.5f)
        {
            anim.SetTrigger("TriggerWalkIdle");
            if (!isLookLeft) Flip(isLookLeft);
            if(!PassosSound.isPlaying)
                PassosSound.Play();
        }
        else
        {
            anim.SetTrigger("Dead");
        }

    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void Flip(bool isLeft)
    {
        isLookLeft = !isLookLeft;
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        transform.Rotate(new Vector3(0, 180, 0), Space.World);
    }


    public bool GetPlayerVisible()
    {
        return playerVisible;
    }

    public void UpdatePlayerVisible(bool status)
    {
        playerVisible = status;
    }

    //Add Paulo - Player

    public void ThrowObject()
    {
        if (currentThrowableObject != null)
        {
            currentThrowableObject.Throw(PlayerForward(), rightHandPosition.position);
            currentThrowableObject = null;
        }

    }

    public void Hide()
    {
        if (currentSafePlace != null)
        {
            transform.position = new Vector3(currentSafePlace.transform.position.x, transform.position.y, transform.position.z);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false; 
            UpdatePlayerVisible(false);
        }
    }

    public void ShowUp()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void PickItem()
    {
        currentThrowableObject = currentTouchedObject.GetComponent<InventoryObject>();
        currentTouchedObject = null;
        currentThrowableObject.transform.SetParent(transform);
        currentThrowableObject.transform.position = rightHandPosition.position;
        Rigidbody2D rb = currentThrowableObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //entrando no local seguro

        if (collision.tag == "safeplace")
            currentSafePlace = collision.gameObject;

        //Guardar itens Arremessáveis
        if (collision.gameObject.tag == "throwable")
            currentTouchedObject = collision.gameObject.GetComponent<InventoryObject>();

        //papelzin
        if(collision.gameObject.tag == "papelzin")
        {
            papelzin = collision.gameObject;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //saindo do local seguro

        if (collision.gameObject == currentSafePlace)
            currentSafePlace = null;

        //Guardar itens Arremessáveis
        if (collision.gameObject == currentThrowableObject)
            currentTouchedObject = null;

        //Papelzin
        if (collision.gameObject == papelzin)
            papelzin = null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("MORRE!");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = morteAudioPorco;
            audioSource.Play();
        }
    }

    Vector3 PlayerForward()
    {
        if (isLookLeft)
            return new Vector3(-1, 1, 0);
        else
            return new Vector3(1, 1, 0);
    }

}
