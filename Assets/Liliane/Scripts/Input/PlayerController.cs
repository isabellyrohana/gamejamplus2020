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
    private bool playerDeath;

    public UiPauseController uiPauseController;
    private bool _gameIsPaused = false;

    AudioSource PassosSound;

    //Paulo
    [SerializeField]
    InventoryObject currentThrowableObject;

    [SerializeField]
    GameObject currentSafePlace;

    InventoryObject currentTouchedObject;

    public Transform rightHandPosition;

    public AudioClip morteAudioPorco;

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
        if (_gameIsPaused) return;
        if (playerDeath) return;

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

        //esc teclado ou pause do controle do xbox
        if (Input.GetButtonDown("Pause"))
        {
            if (GameController.Instance != null)
            {
                if (GameController.Instance.IsPause()) 
                {
                    uiPauseController.Hide(() => Pause(false));
                }
                else 
                {
                    uiPauseController.Show(() => Pause(true));
                }
            }
        }

        //r do teclado ou B do controle do xbox
        if (Input.GetButtonDown("Interact"))
        {
            if (papelzin != null)
            {
                string text = "Raira eu queria te falar antes.. mas nao tive como. Cuidado com as sombras!! Tem cois...";
                Journal journal = new Journal(text);
                if (Journals.AddJournal(journal))
                {
                    Destroy(papelzin.gameObject);
                    Pause(true);
                    uiPauseController.OpenLastJournal();
                }
            }
            else if (uiPauseController.JournalIsOpen())
            {
                Pause(false);
            }
        }

        //shift do teclado ou Y do controle do xbox
        if (Input.GetButtonDown("Diary"))
        {
            Pause(false);
            uiPauseController.ButtonJournals();
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
            //anim.SetTrigger("TriggerWalkIdle");
            if (isLookLeft) Flip(isLookLeft);
            if (!PassosSound.isPlaying) PassosSound.Play();
        }
        else if (horizontalInput < -0.5f)
        {
            //anim.SetTrigger("TriggerWalkIdle");
            if (!isLookLeft) Flip(isLookLeft);
            if (!PassosSound.isPlaying) PassosSound.Play();
        }

        if (horizontalInput == 0)
        {
            playerAnim.SetBool("walk", false);
        }
        else
        {
            playerAnim.SetBool("walk", true);
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

    public void Pause(bool pause) => GameController.Instance?.SetPause(pause);

    public void SetPause(bool pause)
    {
        playerRb.velocity *= Vector2.up;
        _gameIsPaused = pause;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rasga"))
        {
            Destroy(this.gameObject);
            GameController.Instance.ActiveGameOver();
        }

        if (other.gameObject.CompareTag("Door"))
        {
            Destroy(this.gameObject);
            GameController.Instance.ToFinal();
        }

        if (other.tag == "safeplace")
            currentSafePlace = other.gameObject;

        //Guardar itens Arremessáveis
        if (other.gameObject.tag == "throwable")
            currentTouchedObject = other.gameObject.GetComponent<InventoryObject>();

        //papelzin
        if (other.gameObject.tag == "papelzin")
        {
            papelzin = other.gameObject;
        }
    }

    private void PlaySfx()
    {
        SoundFxController.Instance.playFx(5);
    }

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

    private void OnTriggerExit2D(Collider2D other)
    {
        //saindo do local seguro

        if (other.gameObject == currentSafePlace)
            currentSafePlace = null;

        //Guardar itens Arremessáveis
        if (other.gameObject == currentThrowableObject)
            currentTouchedObject = null;

        //Papelzin
        if (other.gameObject == papelzin)
            papelzin = null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
