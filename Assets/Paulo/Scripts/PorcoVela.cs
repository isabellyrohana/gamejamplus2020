using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PorcoVela : MonoBehaviour
{
    public Transform[] positions;
    public Transform porco;

    public Transform positionFinal;

    public int target;

    int nextTargetIndex;

    public float speed;

    public bool isLookLeft = false;

    bool didSprint = false;

    public Transform positionA;

    public Transform positionB;

    public Light2D vela;

    bool sprinting = false;

    public Animator animator;

    private bool _isGamePaused;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        porco.position = positions[0].position;
        target = nextTargetIndex = 0;
    }

    private void Update()
    {
        if (_isGamePaused) return;

        if (PlayerController.Instance.GetPlayerVisible())
        {
            if (!GetLoud)
            {
                AudioSource.PlayClipAtPoint(porcoGrito, transform.position);
                GetLoud = true;
            }

            speed = 5f;
            porco.position =
                    Vector3.MoveTowards(porco.position, positionFinal.position, speed * Time.deltaTime);
            isLookLeft = false;
            vela.transform.position = positionA.position;
            sprinting = true;
            Flip();
        }
        else
        {

            if (!sprinting) {
                speed = 1;
                porco.position =
                    Vector3.MoveTowards(porco.position, positions[target].position, speed * Time.deltaTime);

                if (porco.position == positions[target].position)
                {
                    nextTargetIndex++;

                    target = nextTargetIndex % positions.Length;

                    porco.GetComponent<SpriteRenderer>().flipX = !porco.GetComponent<SpriteRenderer>().flipX;
                }
            }
            else
            {
                speed = 5f;
                porco.position =
                        Vector3.MoveTowards(porco.position, positionFinal.position, speed * Time.deltaTime);
                isLookLeft = false;
                vela.transform.position = positionA.position;
                sprinting = true;
                Flip();
            }
        }


        if (porco.position.x >= positions[target].position.x && isLookLeft && !sprinting)
        {
            isLookLeft = false;
            vela.transform.position = positionA.position;
            Flip();
        }
        else if (porco.position.x <= positions[target].position.x && !isLookLeft && !sprinting)
        {
            isLookLeft = true;
            vela.transform.position = positionB.position;
            Flip();
        }
    }

    private void Flip()
    {
        porco.GetComponent<SpriteRenderer>().flipX = isLookLeft;

    }

    public void SetPause(bool pause)
    {
        _isGamePaused = pause;
        animator.enabled = !pause;
    }


}
