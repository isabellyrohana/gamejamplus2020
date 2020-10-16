using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasgaController : Singleton<RasgaController>
{
    public SpriteRenderer rasgaSr;
    public Animator rasgaAnim;
    public Transform[] posRasga;
    public Transform Rasga;
    public Transform posToRun;
    public Transform posOrigin;
    
    public float speed;
    public float speedToAttack;
    public bool isLookLeft = true;

    private Rigidbody2D rasgaControllerRb;
    private Vector2 direction;
    private int idTarget;
    private bool canAttack = true;

    public Animator animator;

    private bool _isGamePaused;

    void Start()
    {
        rasgaControllerRb = GetComponent<Rigidbody2D>();

        Rasga.position = posRasga[0].position;
        idTarget = 1;
    }

    void Update()
    {
        if (_isGamePaused) return;
        
        if (PlayerController.Instance == null) return;

        if(PlayerController.Instance.GetPlayerOnTheLight())
        {
            rasgaAnim.SetBool("attack", true);

            direction = (PlayerController.Instance.transform.position - transform.position).normalized;
            rasgaControllerRb.velocity = direction * speedToAttack;
            
            if (direction.x < 0 && !isLookLeft)
            {
                Flip();
            }
            else if (direction.x > 0 && isLookLeft)
            {
                Flip();
            }
        }
        else
        {
            if (!canAttack) return;

            rasgaAnim.SetBool("attack", false);

            rasgaControllerRb.velocity = Vector2.zero;

            Rasga.position = Vector3.MoveTowards(Rasga.position, posRasga[idTarget].position, speed * Time.deltaTime);

            if (Rasga.position == posRasga[idTarget].position)
            {
                idTarget += 1;
                if (idTarget == posRasga.Length)
                {
                    idTarget = 0;
                }
            }

            if (Rasga.position.x > posRasga[idTarget].position.x && !isLookLeft)
            {
                Flip();
            }
            else if (Rasga.position.x < posRasga[idTarget].position.x && isLookLeft)
            {
                Flip();
            }
        }        
    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        rasgaSr.flipX = !rasgaSr.flipX;
    }

    public void SetPause(bool pause)
    {
        _isGamePaused = pause;
        animator.enabled = !pause;
    }

    public void UpdateCanAttack(bool value)
    {
        canAttack = value;
    }

    public void RasgaAffected()
    {
        StartCoroutine("FlyAway");
    }

    private IEnumerator FlyAway()
    {
        direction = (posToRun.transform.position - transform.position).normalized;
        rasgaControllerRb.velocity = direction * speedToAttack;

        yield return new WaitForSeconds(5f);
        
        rasgaControllerRb.velocity = Vector2.zero;
    }

}
