using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] 
    private Transform rightHandPosition = null;
    private Animator playerAnim;
    private RaycastHit2D hit;
    private bool isThrowable;
    private int direction;
    
    public LayerMask layerNotThrowable;
    public float distance = 0.5f;
    public float throwForce;

    private bool isThrow;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(PlayerController.Instance.IsFacingLeft())
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        if (isThrowable)
        {
            hit.collider.gameObject.transform.position = rightHandPosition.position;
        }
    }

    private void OnPick(/*InputValue value*/)
    {
        Debug.Log("OnPick");
        if (!isThrowable /*&& value.Get<float>() == 1*/)
        {
            Physics2D.queriesStartInColliders = false;
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.right * direction, distance);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("throwable"))
            {
                isThrowable = true;
                playerAnim.SetBool("hold", true);
            }
        }
        else if (!Physics2D.OverlapPoint(rightHandPosition.position, layerNotThrowable) /*&& value.Get<float>() == 1*/)
        {
            isThrowable = false;

            if(hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                hit.collider.gameObject.TryGetComponent(out Rigidbody2D hitRb);

                hitRb.bodyType = RigidbodyType2D.Dynamic;
                hitRb.mass = .5f;
                hitRb.velocity = Vector2.zero;

                Vector2 vectorThrow = new Vector2(direction, 1f) * throwForce;
                hitRb.AddForce(vectorThrow);
                Debug.Log(vectorThrow);
                
                hit.collider.gameObject.TryGetComponent(out ObjectToShoot hitScript);
                hitScript.ChangeToTrigger();
                
                playerAnim.SetBool("hold", false);
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y - 1f), new Vector2(transform.position.x, transform.position.y - 1f) + Vector2.right * direction * distance);
    }
}
