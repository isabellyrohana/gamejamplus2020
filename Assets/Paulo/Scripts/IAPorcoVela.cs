using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        CurrentState = CurrentState.Process();
    }
}
