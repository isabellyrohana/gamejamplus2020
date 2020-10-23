using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [Header("Position Object Held")]
    public GameObject targetObject;
    public LayerMask boxMask;

    public float distance = 0.5f;
    public float forceAttack = 20f;

    private PlayerController _playerController;
    private GameObject box;
    private Animator playerAnim;
    private RaycastHit2D hit;
    
    private int direction = 1;
    private float valuePick = -1;
    private float valueInteract = -1;
    private float _horizontalInput = 0f;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.right * transform.localScale.x, distance, boxMask);

        if(hit.collider != null && hit.collider.gameObject.tag=="throwable" && valuePick == 1/*Input.GetButtonDown("Pick")*/)
        {
            box = hit.collider.gameObject;
            box.TryGetComponent(out Rigidbody2D boxRb);
            boxRb.bodyType = RigidbodyType2D.Dynamic;

            playerAnim.SetBool("hold", true);

            StartCoroutine("KeyIsPressed");
        }
        
        if(hit.collider != null && /*Input.GetButtonDown("Interact")*/ valueInteract == 1 && box!=null)
        {
            playerAnim.SetBool("hold", false);
            StopCoroutine("KeyIsPressed");
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.TryGetComponent(out Rigidbody2D boxRb);
            box.TryGetComponent(out ObjectToShoot boxScript);
            boxScript.ChangeToTrigger();

            if(_playerController.IsFacingLeft())
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            boxRb.AddForce(new Vector2(forceAttack * direction, forceAttack) * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y - 1f), new Vector2(transform.position.x, transform.position.y - 1f) + Vector2.right * transform.localScale.x * distance);
    }

    private IEnumerator KeyIsPressed()
    {
        yield return new WaitForSecondsRealtime(0f);
        Hold(box);
        StartCoroutine("KeyIsPressed");
    }

    void Hold(GameObject box){

        box.GetComponent<FixedJoint2D>().enabled = true;
        box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

        if(_horizontalInput != 0)
        {
            box.transform.position = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y);
        }
    }

    private void OnMove(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
        print("move " + _horizontalInput);
    }

    private void OnPick(InputValue value)
    {
        print("pick push click");

        valuePick = value.Get<float>();

        /*if(hit.collider != null && hit.collider.gameObject.CompareTag("throwable"))
        {
            print("rhs");
            box = hit.collider.gameObject;
            box.TryGetComponent(out Rigidbody2D boxRb);
            boxRb.bodyType = RigidbodyType2D.Dynamic;

            playerAnim.SetBool("hold", true);

            //StartCoroutine("KeyIsPressed");
        }*/
    }

    private void OnInteract(InputValue value)
    {
        print("interact push click");
        valueInteract = value.Get<float>();
        /*if(hit.collider != null && box!=null)
        {
            print("rhdsss");
            playerAnim.SetBool("hold", false);
            //StopCoroutine("KeyIsPressed");
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.TryGetComponent(out Rigidbody2D boxRb);
            box.TryGetComponent(out ObjectToShoot boxScript);
            boxScript.ChangeToTrigger();

            if(_playerController.IsFacingLeft())
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            boxRb.AddForce(new Vector2(20 * direction, 25), ForceMode2D.Impulse);
        }*/
    }

}
