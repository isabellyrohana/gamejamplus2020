using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatePorcoVela
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected PorcoVela porco;
    protected Animator anim;
    protected StatePorcoVela nextState;

    public StatePorcoVela(PorcoVela _porco, Animator _anim)
    {
        porco = _porco;
        anim = _anim;
    }

    public virtual void Enter() { stage = EVENT.ENTER; }
    public virtual void Update() { stage = EVENT.UPDATE;}
    public virtual void Exit() { stage = EVENT.EXIT; }

    public StatePorcoVela Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool IsPlayerVisible()
    {
        if (PlayerController.Instance.GetPlayerVisible())
            return true;

        return false;
    }

}


public class StatePorcoVelaIDLE: StatePorcoVela
{
    public StatePorcoVelaIDLE(PorcoVela _porco, Animator _anim)
        : base(_porco, _anim)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        //TODO Verificar
        anim.SetTrigger("IDLE");

        base.Enter();
    }

    public override void Update()
    {
        if (IsPlayerVisible())
        {
            //TODO Chamar PURSUE
            //nextState = new 
        }
        base.Update();
    }

    public override void Exit()
    {
        //TODO Verificar 2
        anim.ResetTrigger("IDLE");
        base.Exit();
    }
}

public class StatePorcoVelaPATROL: StatePorcoVela
{
    public StatePorcoVelaPATROL
}