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

    protected STATE name;
    protected STAGE stage;
    protected PorcoVela porcoVela;
    protected Animator anim;
    protected StatePorcoVela nextState;
    protected Transform[] patrolPoints;
    protected Transform pursueTarget;

    #endregion

    public StatePorcoVela(PorcoVela _porcoVela, Animator _anim, Transform[] _patrolPoints, Transform _pursueTarget)
    {
        porcoVela = _porcoVela;
        anim = _anim;
        patrolPoints = _patrolPoints;
        pursueTarget = _pursueTarget;
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
    public StatePorcoVelaIDLE(PorcoVela porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget) 
        :base(porcoVela, anim, _patrolPoints, _pursueTarget)
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
        nextState = new StatePorcoVelaPATROL(porcoVela, anim, patrolPoints, pursueTarget);

        base.Exit();
    }
}

public class StatePorcoVelaPATROL: StatePorcoVela
{
    public StatePorcoVelaPATROL(PorcoVela porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
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
            nextState = new StatePorcoVelaPURSUE(porcoVela, anim, patrolPoints, pursueTarget);
            stage = STAGE.EXIT;
        }
        else
        {
            porcoVela.transform.position =
                Vector2.MoveTowards(porcoVela.transform.position,
                                    pursueTarget.position,
                                    porcoVela.speed * Time.deltaTime
                                    );

            if (porcoVela.transform.position == pursueTarget.position)
            {
                nextState = new StatePorcoVelaIDLE(porcoVela, anim, patrolPoints, pursueTarget);
            }
            
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class StatePorcoVelaPURSUE: StatePorcoVela
{
    public StatePorcoVelaPURSUE(PorcoVela porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
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

        porcoVela.transform.position = 
            Vector2.MoveTowards(porcoVela.transform.position,
                                pursueTarget.position,
                                porcoVela.speed * Time.deltaTime
                                );

        if (porcoVela.transform.position == pursueTarget.position)
            stage = STAGE.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }
}