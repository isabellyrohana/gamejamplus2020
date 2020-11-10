using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator)),
]
public class PlayerController : Singleton<PlayerController>
{

    [Range(0.5f, 3f)] [SerializeField] private float playerWalkSoundSfx = 1f;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private UiPauseController uiPauseController = null;
    [SerializeField] private Transform rightHandPosition = null;
    [SerializeField] private AudioClip morteAudioPorco = null;
    [SerializeField] private AudioClip armarioEntrandoAudioClip = null;
    [SerializeField] private AudioClip armarioSaindoAudioClip = null;
    [SerializeField] private Sprite armarioPortaAberta = null;
    [SerializeField] private Sprite armarioPortaFechada = null;

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
    private GameObject _lastSafePlace = null;
    private InventoryObject _currentTouchedObject = null;
    private GameObject _journalReference = null;
    private Picture _picture = null;

    // Internal References
    private Rigidbody2D _rigidbody2D = null;
    private Animator _animator = null;
    private PlayerPush _playerPush = null;

    // Timers
    private float timeSoundWalkSfx = 0f;

    public override void Init()
    {
        base.Init();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerPush = GetComponent<PlayerPush>();
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
                if (!_onDoor.NeedKey())
                    _onDoor.OpenDoor();
                else if (GetHasKey())
                    _onDoor.OpenDoor();
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

    public void GetKey()
    {
        _hasKey = true;
    }

    private void Move()
    {
        if (_horizontalInput != 1 && _horizontalInput != -1)
        {
            timeSoundWalkSfx = playerWalkSoundSfx;
            _animator.SetBool("walk", false);
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            Debug.Log("Entrou ELSE");
            float speed = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(_horizontalInput * playerSpeed, speed);

            if (_horizontalInput > 0f && _isFacingLeft) Flip();
            else if (_horizontalInput < 0f && !_isFacingLeft) Flip();

            timeSoundWalkSfx -= Time.deltaTime;
            if (timeSoundWalkSfx <= 0f)
            {
                SoundFxController.Instance.playFx(5);
    	        timeSoundWalkSfx = playerWalkSoundSfx;
            }
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
            SpriteRenderer currentSafePlaceSprite = _currentSafePlace.GetComponent<SpriteRenderer>();
            _lastSafePlace = _currentSafePlace;
            //transform.position = new Vector3(_currentSafePlace.transform.position.x, transform.position.y, transform.position.z);
            _rigidbody2D.velocity *= Vector2.up;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _isHiding = true;
            currentSafePlaceSprite.sprite = armarioPortaFechada;

            AudioSource.PlayClipAtPoint(armarioEntrandoAudioClip, Camera.main.transform.position, 1f);
        }
    }

    public void ShowUp()
    {
        if (_isHiding)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            _isHiding = false;
            SpriteRenderer currentSafePlaceSprite = _lastSafePlace.GetComponent<SpriteRenderer>();
            currentSafePlaceSprite.sprite = armarioPortaAberta;
            _lastSafePlace = null;
            AudioSource.PlayClipAtPoint(armarioSaindoAudioClip, Camera.main.transform.position, 1f);
        }
    }

    public bool IsInteracting() => _playerPush.IsInteracting();

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

        // Enter in Door Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.DOOR))) _onDoor = other.GetComponent<Door>();
        // Enter in Safeplace Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.SAFEPLACE))) _currentSafePlace = other.gameObject;
        // Enter in Throwable Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.THROWABLE))) _currentTouchedObject = other.gameObject.GetComponent<InventoryObject>();
        // Enter in Papelzin Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PAPER))) _journalReference = other.gameObject;
        // Enter in Picture Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PICTURE))) _picture = other.gameObject.GetComponent<Picture>();
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
        // Exit in Picture Trigger
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PICTURE))) _picture = null;
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
        Debug.Log("OnMove: " + _horizontalInput);
    }

    private void OnHideAppear(InputValue value)
    {
        _verticalInput = value.Get<Vector2>().y;
    }

    private void OnDiary()
    {
        Pause(true);
        uiPauseController.ButtonJournals();
    }

    private void OnInteract()
    {
        if (_journalReference != null)
        {
            string text = LocalizationManager.Instance.GetLocalizationValue("diary01");
            Journal journal = new Journal(text);
            Journals.AddJournal(journal);
            Pause(true);
            uiPauseController.OpenLastJournal();
        }
        else if (_picture != null)
        {
            Pause(true);
            uiPauseController.OpenPicture();
        }
        else if (uiPauseController.JournalIsOpen())
        {
            uiPauseController.CloseJournal(() => Pause(false));
        }
    }

    private void OnPause()
    {
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
        Debug.Log("OnBag");
    }

    #endregion

}
