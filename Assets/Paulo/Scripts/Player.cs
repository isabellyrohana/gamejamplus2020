using UnityEngine;

public enum PlayerRotationSrite
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

    public Transform rightHandPosition;

    private PlayerRotationSrite currentPlayerRotation;


    // Update is called once per frame
    void Update()
    {
        #region Movement

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentPlayerRotation = PlayerRotationSrite.Left;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
            
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentPlayerRotation = PlayerRotationSrite.Right;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        #endregion

        #region ThrowObject
        if (Input.GetKey(KeyCode.S))
        {
            if (currentThrowableObject == null)
                return;
            ThrowObject();
        }
        #endregion
    }


    private void Rotate(PlayerRotationSrite side)
    {
        switch (side)
        {
            case PlayerRotationSrite.Left:
                transform.Rotate(new Vector3(0, 0, 0), Space.World);
                break;

            case PlayerRotationSrite.Right:
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
            currentThrowableObject.Throw(PlayerForward(), rightHandPosition.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Guardar itens Arremessáveis
        if (collision.gameObject.tag == "throwable")
        {
            currentThrowableObject = collision.gameObject.GetComponent<InventoryObject>();
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.transform.position = rightHandPosition.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        //Guardar itens


    }


    Vector3 PlayerForward()
    {
        switch (currentPlayerRotation)
        {
            case PlayerRotationSrite.Left:
                return new Vector3(-1, 0, 0);

            case PlayerRotationSrite.Right:
                return new Vector3(1, 0, 0);

            default: return new Vector3(-1, 0, 0);

        }
    }


}
