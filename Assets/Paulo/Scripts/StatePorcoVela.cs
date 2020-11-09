using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class StatePorcoVela
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE
    }

    public enum STAGE
    {
        ENTER, UPDATE, EXIT
    }

    #region Attributes

    public STATE name;
    public STAGE stage;
    public PorcoVela porcoVela;
    public Animator anim;
    public StatePorcoVela nextState;

    #endregion

    public StatePorcoVela(PorcoVela _porcoVela, Animator _anim)
    {
        porcoVela = _porcoVela;
        anim = _anim;
    }

    public virtual void Enter() { stage = STAGE.ENTER; }
    public virtual void Update() { stage = STAGE.UPDATE; }
    public virtual void Exit() { stage = STAGE.EXIT; }

    public StatePorcoVela Process()
    {
        if(stage == STAGE.ENTER) { Enter(); }
        if(stage == STAGE.UPDATE) { Update(); }
        if(stage == STAGE.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool IsPlayerVisible()
    {
        return PlayerController.Instance.GetPlayerVisible();
    }

}


public class StatePorcoVelaIDLE : StatePorcoVela
{
    public StatePorcoVelaIDLE(PorcoVela porcoVela, Animator anim) 
        :base(porcoVela, anim)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("IDLE");//TODO verificar
    }

    public override void Update()
    {
        base.Update();

        float valorRandom = Random.Range(0, 1f);
        if(valorRandom < .25f)
        {
            stage = STAGE.EXIT;
        }

    }

    public override void Exit()
    {
        anim.SetTrigger("IDLE");

        //TODO verificar
        nextState = new StatePorcoVelaPATROL(porcoVela, anim);

        base.Exit();
    }
}

public class StatePorcoVelaPATROL: StatePorcoVela
{
    public StatePorcoVelaPATROL(PorcoVela porcoVela, Animator anim)
        :base(porcoVela, anim)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        base.Enter();
        porcoVela.speed = 1f;
    }

    public override void Update()
    {
        base.Update();

        if (IsPlayerVisible())
        {
            nextState = new StatePorcoVelaPURSUE(porcoVela, anim);
            stage = STAGE.EXIT;
        }
        else
        {

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class StatePorcoVelaPURSUE: StatePorcoVela
{
    public StatePorcoVelaPURSUE(PorcoVela porcoVela, Animator anim)
        : base(porcoVela, anim)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        base.Enter();
        porcoVela.speed = 4f;   
    }

    public override void Update()
    {
        base.Update();


    }
}