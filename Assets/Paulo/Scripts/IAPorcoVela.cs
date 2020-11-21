using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookAt
{
    Right,
    Left
}

public class IAPorcoVela : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Transform pursueTarget;
    [SerializeField] float speed = 1f;
    public float Speed { get; set; }

    Animator anim;
    
    StatePorcoVela CurrentState;

    void Start()
    {
        anim = GetComponent<Animator>();
        CurrentState = new StatePorcoVelaIDLE(gameObject, anim, patrolPoints, pursueTarget);
    }

    void Update()
    {
        CurrentState = CurrentState.Process();
    }

    public void Flip(LookAt look)
    {
        if (look == LookAt.Right)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);
            

    }
}
