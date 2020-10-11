using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasgaController : MonoBehaviour
{
    public Transform[]      posRasga;
    public Transform        Rasga;
    
    public float            speed;

    private int idTarget;
    private bool isLookLeft = true;

    public SpriteRenderer rasgaSr;
    public Transform player;

    public Animator animator;

    private bool _isGamePaused;

    void Start()
    {
        //rasgaSr = GetComponent<SpriteRenderer>();

        Rasga.position = posRasga[0].position;
        idTarget = 1;
    }

    void Update()
    {
        if (_isGamePaused) return;

        if(PlayerController.Instance.GetPlayerVisible())
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, 0.1f);
        }
        else
        {
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

}
