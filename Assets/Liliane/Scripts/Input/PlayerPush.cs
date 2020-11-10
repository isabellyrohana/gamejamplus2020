using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] 
    private Transform rightHandPosition = null;
    private Animator playerAnim = null;
    private RaycastHit2D hit;
    private bool isThrowable = false;
    private int direction = 0;
    
    public LayerMask layerNotThrowable;
    public float distance = 1.5f;
    public float throwForce = 400f;

    private void Awake()
    {
        isThrowable = false;
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        direction = PlayerController.Instance.IsFacingLeft() ? -1 : 1;
    }

    private void OnPick(InputValue value)
    {
        bool pressPick = value.Get<float>() == 1;

        if (!isThrowable && pressPick)
        {
            int layerThrowable = 1 << LayerMask.NameToLayer(Tags.GetTag(Tags.TagsEnum.THROWABLE));
            Vector3 startPoint = transform.position + Vector3.down;
            Vector3 rayDirection = Vector3.right * direction;
            hit = Physics2D.Raycast(startPoint, rayDirection, distance, layerThrowable);

            if (hit.collider != null && 
                hit.collider.CompareTag(Tags.GetTag(Tags.TagsEnum.THROWABLE)))
            {
                isThrowable = true;

                GameObject vase = hit.transform.gameObject;
                vase.transform.position = rightHandPosition.position;
                vase.transform.SetParent(rightHandPosition);

                playerAnim.SetBool("hold", true);
                Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);
            }
        }
        else if (!Physics2D.OverlapPoint(rightHandPosition.position, layerNotThrowable) && pressPick)
        {
            isThrowable = false;

            if(hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                rightHandPosition.DetachChildren();

                GameObject vase = hit.transform.gameObject;
                vase.TryGetComponent(out Rigidbody2D hitRb);

                hitRb.bodyType = RigidbodyType2D.Dynamic;
                hitRb.velocity = Vector2.zero;

                Vector2 vectorThrow = new Vector2(direction, 2.5f) * throwForce;
                hitRb.AddForce(vectorThrow);
                
                vase.TryGetComponent(out ObjectToShoot hitScript);
                hitScript.ChangeToTrigger();
                
                playerAnim.SetBool("hold", false);
            }
        }
    }

    public bool IsInteracting() => rightHandPosition.childCount > 0;

    void OnDrawGizmos() {
        Vector3 startPoint = transform.position + Vector3.down;
        Vector3 endPoint = startPoint + Vector3.right * direction * distance;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, endPoint);
    }
}
