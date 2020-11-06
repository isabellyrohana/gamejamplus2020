using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(AudioSource)),
]
public class PlayerController : Singleton<PlayerController>
{
    
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private UiPauseController uiPauseController = null;
    [SerializeField] private Transform rightHandPosition = null;
    [SerializeField] private AudioClip morteAudioPorco = null;

    // Inputs
    private float _horizontalInput = 0f;
    private float _verticalInput = 0f;

    // States
    private bool _isFacingLeft = false;
    private bool _isHiding = false;
    private bool _playerDeath = false;
    private bool _gameIsPaused = false;
    private bool _isOnTheLight = false;
    public bool _hasKey = false;

    // Paulo
    private InventoryObject _currentThrowableObject = null;

    // Triggers Checkers
    private Door _onDoor = null;
    private GameObject _currentSafePlace = null;
    private InventoryObject _currentTouchedObject = null;
    private GameObject _journalReference = null;

    // Internal References
    private Rigidbody2D _rigidbody2D = null;
    private Animator _animator = null;
    private AudioSource _passosSound = null;

    protected new virtual void Awake() 
    {
        base.Awake();
        
        _rigidbody2D = GetComponent<Rigidbody2D>();   
        _passosSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_gameIsPaused) return;
        if (_playerDeath) return;

        if (!_isHiding) Move();

        if (_verticalInput == 1)
        {
            if (_onDoor != null)
            {
                if(GetHasKey())
                {
                    _onDoor.OpenDoor();
                }
            }
            else Hide();
        }
        else if (_verticalInput == -1)
        {
            ShowUp();
        }
    }

    public void UpdateHasKey(bool value)
    {
        _hasKey = value;
    }

    public bool GetHasKey()
    {
        return _hasKey;
    }

    private void Move()
    {
        float speedY = _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = new Vector2(_horizontalInput * playerSpeed, speedY);

        if (_horizontalInput > 0.2f)
        {
            if (_isFacingLeft) Flip();
            if (_passosSound != null && !_passosSound.isPlaying) _passosSound.Play();
        }
        else if (_horizontalInput < -0.2f)
        {
            if (!_isFacingLeft) Flip();
            if (_passosSound != null && !_passosSound.isPlaying) _passosSound.Play();
        }

        if (_horizontalInput == 0)
        {
            _animator.SetBool("walk", false);
        }
        else
        {
            _animator.SetBool("walk", true);
        }
    }

    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }

    public bool GetPlayerVisible()
    {
        return _isHiding;
    }

    //Quando ele estiver visivel na Luz vermelha o inimigo ataca
    public bool GetPlayerOnTheLight()
    {
        return _isOnTheLight;
    }

    public void UpdatePlayerOnTheLight(bool status)
    {
        _isOnTheLight = status;
    }

    public void Pause(bool pause) => GameController.Instance?.SetPause(pause);

    public void SetPause(bool pause)
    {
        _rigidbody2D.velocity *= Vector2.up;
        _gameIsPaused = pause;
    }

    private void PlaySfx()
    {
        SoundFxController.Instance.playFx(5);
    }

    public void ThrowObject()
    {
        if (_currentThrowableObject != null)
        {
            _currentThrowableObject.Throw(PlayerForward(), rightHandPosition.position);
            _currentThrowableObject = null;
        }
    }

    public void Hide()
    {
        if (_currentSafePlace != null && !_isHiding)
        {
            transform.position = new Vector3(_currentSafePlace.transform.position.x, transform.position.y, transform.position.z);
            _rigidbody2D.velocity *= Vector2.up;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _isHiding = true;
        }
    }

    public void ShowUp()
    {
        if (_isHiding)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            _isHiding = false;
        }
    }

    public void PickItem()
    {
        _currentThrowableObject = _currentTouchedObject.GetComponent<InventoryObject>();
        _currentTouchedObject = null;
        _currentThrowableObject.transform.SetParent(transform);
        _currentThrowableObject.transform.position = rightHandPosition.position;
        Rigidbody2D rb = _currentThrowableObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rasga"))
        {
            Destroy(this.gameObject);
            GameController.Instance.ActiveGameOver();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            GameController.Instance.ActiveGameOver();
        }

        if (other.gameObject.CompareTag("Key"))
        {
            UpdateHasKey(true);
        }

        // Enter in Door Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.DOOR))) _onDoor = other.GetComponent<Door>();
        // Enter in Safeplace Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.SAFEPLACE))) _currentSafePlace = other.gameObject;
        // Enter in Throwable Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.THROWABLE))) _currentTouchedObject = other.gameObject.GetComponent<InventoryObject>();
        // Enter in Papelzin Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PAPER))) _journalReference = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Exit in Door Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.DOOR))) _onDoor = null;
        // Exit in Safeplace Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.SAFEPLACE))) _currentSafePlace = null;
        // Exit in Throwable Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.THROWABLE))) _currentTouchedObject = null;
        // Exit in Papelzin Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PAPER))) _journalReference = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.GetTag(Tags.TagsEnum.ENEMY)))
        {
            Debug.Log("MORRE!");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = morteAudioPorco;
            audioSource.Play();
        }
    }

    private Vector3 PlayerForward()
    {
        if (_isFacingLeft) return new Vector3(-1, 1, 0);
        else return new Vector3(1, 1, 0);
    }

    public bool IsFacingLeft() => _isFacingLeft;

    #region New Input System

    private void OnMove(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
        print("move " + _horizontalInput);
    }

    private void OnHideAppear(InputValue value)
    {
        _verticalInput = value.Get<Vector2>().y;
        print("hide " + _verticalInput);
    }

    private void OnDiary()
    {
        Pause(true);
        uiPauseController.ButtonJournals();
    }

    private void OnPick()
    {
        print("pick click");
        // Se tiver encostado, pega o Item
        if (_currentTouchedObject != null) PickItem();
        // Se tiver segurando algum item, arremessa
        else if (_currentThrowableObject != null) ThrowObject();
    }

    private void OnInteract()
    {
        print("interact click");

        if (_journalReference != null)
        {
            string text = "Raira eu queria te falar antes.. mas nao tive como. Cuidado com as sombras!! Tem cois...";
            Journal journal = new Journal(text);
            if (Journals.AddJournal(journal))
            {
                Destroy(_journalReference.gameObject);
                Pause(true);
                uiPauseController.OpenLastJournal();
            }
        }
        else if (uiPauseController.JournalIsOpen())
        {
            Pause(false);
        }
    }

    private void OnPause()
    {
        print("pause click");
        
        if (GameController.Instance != null)
        {
            if (GameController.Instance.IsPause()) uiPauseController.Hide(() => Pause(false));
            else 
            {
                Pause(true);
                uiPauseController.Show();
            }
        }
    }

    private void OnBag()
    {
        print("bag click");
    }

    #endregion

}
