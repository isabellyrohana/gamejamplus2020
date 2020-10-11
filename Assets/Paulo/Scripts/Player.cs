using UnityEngine;

public enum PlayerRotationSprite
{
    Left,
    Right
}

public class Player : MonoBehaviour
{
    [Range(.5f, 10f)]
    [SerializeField]
    private float speed = 2.5f;

    [SerializeField]
    InventoryObject currentThrowableObject;

    [SerializeField]
    GameObject currentSafePlace;

    InventoryObject currentTouchedObject;

    public Transform rightHandPosition;

    private PlayerRotationSprite currentPlayerRotation;

    private float horizontalInput;
    private float verticalInput;


    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("HideAppear");

        #region Movement

        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        if(horizontalInput < 0)
        {
            currentPlayerRotation = PlayerRotationSprite.Left;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        if (horizontalInput > 0)
        {
            currentPlayerRotation = PlayerRotationSprite.Right;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        #endregion

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


        if (verticalInput > 0)
        {
            if (currentSafePlace != null)
            {
                transform.position = currentSafePlace.transform.position;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else if (verticalInput < 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }


    private void Rotate(PlayerRotationSprite side)
    {
        switch (side)
        {
            case PlayerRotationSprite.Left:
                transform.Rotate(new Vector3(0, 0, 0), Space.World);
                break;

            case PlayerRotationSprite.Right:
                transform.Rotate(new Vector3(0, 180, 0), Space.World);
                break;

            default:
                transform.Rotate(new Vector3(0, 0, 0), Space.World);
                break;
        }
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
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ShowUp()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = currentSafePlace.transform.position;
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
        if (collision.gameObject.tag == "throwable" )
            currentTouchedObject = collision.gameObject.GetComponent<InventoryObject>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //saindo do local seguro

        if (collision.gameObject == currentSafePlace)
            currentSafePlace = null;

        //Guardar itens Arremessáveis
        if (collision.gameObject == currentThrowableObject)
        {
            currentTouchedObject = null;
        }
    }

    Vector3 PlayerForward()
    {
        switch (currentPlayerRotation)
        {
            case PlayerRotationSprite.Left:
                return new Vector3(-1, 1, 0);

            case PlayerRotationSprite.Right:
                return new Vector3(1, 1, 0);

            default: return new Vector3(-1, 1, 0);

        }
    }

}
