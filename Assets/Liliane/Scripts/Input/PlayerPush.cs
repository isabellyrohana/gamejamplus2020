using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPush : MonoBehaviour
{
    private GameObject box;

    [Header("Position Object Held")]
    public GameObject targetObject;

    public LayerMask boxMask;
    public float distance = 1f;

    private PlayerController _playerController;
    private float delayToShoot = 3f;
    private int direction = 1;

    private bool canShoot;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {

        Physics2D.queriesStartInColliders = false; //Edit>Physics2D>Uncheck QueriesHitTriggers se quiser usar com trigger, ou IgnoreLayer :D
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, distance, boxMask);

        if(hit.collider != null && hit.collider.gameObject.tag=="throwable" && Input.GetButton("Pick"))
        {
            box = hit.collider.gameObject;
            StartCoroutine("KeyIsPressed");
            StartCoroutine("CanShoot");
        }
        
        if(hit.collider != null && Input.GetButtonUp("Pick") && box!=null) //testar a condição do box, e do hit para tentar retirar
        {
            canShoot = false;
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.TryGetComponent(out Rigidbody2D boxRb);
            if(_playerController.isLookLeft)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            boxRb.AddForce(new Vector2(20 * direction,10), ForceMode2D.Impulse);
            
            StopCoroutine("KeyIsPressed");
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y) + Vector2.right * transform.localScale.x * distance);
    }

    IEnumerator KeyIsPressed()
    {
        yield return new WaitForSecondsRealtime(0f);
        Hold(box);
        StartCoroutine("KeyIsPressed");
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(delayToShoot);
        canShoot = true;
    }

    void Hold(GameObject box){

        box.GetComponent<FixedJoint2D>().enabled = true;
        box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

        if(Input.GetAxisRaw("Horizontal")!=0)
        {
            box.transform.position = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y);
        }
    }
}
