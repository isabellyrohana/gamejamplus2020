using UnityEngine;

public class StateRasgaMortalha
{
    
    public enum STATE
    {
        IDLE, PATROL, PURSUE, RUNAWAY
    }

    public enum STAGE
    {
        ENTER, UPDATE, EXIT
    }

    protected STATE name;
    protected STAGE stage;
    protected Animator Anim;
    protected GameObject RasgaMortalha;
    protected float Speed = 1f;
    protected StateRasgaMortalha nextState;
    protected Transform[] patrolPoints;

    public StateRasgaMortalha(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
    {
        RasgaMortalha = _rasgaMortalha;
        Anim = _anim;
        patrolPoints = _patrolPoints;
    }

    public virtual void Enter() { stage = STAGE.UPDATE; }
    public virtual void Update() { stage = STAGE.UPDATE; }
    public virtual void Exit() { stage = STAGE.EXIT; }

    public StateRasgaMortalha Process()
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

    public bool IsplayerVisible()
    {
        return PlayerController.Instance.GetPlayerOnTheLight();
    }
}


public class RasgaMostalhaIDLE: StateRasgaMortalha
{
    public RasgaMostalhaIDLE(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
        :base(_rasgaMortalha, _anim, _patrolPoints)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        nextState = new RasgaMostalhaPATROL(RasgaMortalha, Anim, patrolPoints);
    }

}

public class RasgaMostalhaPATROL : StateRasgaMortalha
{
    private int NextGoal = 0;
    public RasgaMostalhaPATROL(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
        : base(_rasgaMortalha, _anim, _patrolPoints)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
        Speed = 1f;
    }

    public override void Update()
    {
        base.Update();

        RasgaMortalha.transform.position =
            Vector2.MoveTowards(RasgaMortalha.transform.position,
                                patrolPoints[NextGoal].position,
                                Speed * Time.deltaTime
                                );

        if (RasgaMortalha.transform.position == patrolPoints[NextGoal % patrolPoints.Length].position)
        {
            int chance = Random.Range(0, 1000);
            if(chance < 300)
            {
                nextState = new RasgaMostalhaIDLE(RasgaMortalha, Anim, patrolPoints);
                stage = STAGE.EXIT;
            }
            else
                NextGoal++;

        }

        if (IsplayerVisible())
        {
            nextState = new RasgaMostalhaPURSUE(RasgaMortalha, Anim, patrolPoints);
            stage = STAGE.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}

//TODO PURSUE!!!!!!!!!!!!!!

public class RasgaMostalhaPURSUE : StateRasgaMortalha
{
    public RasgaMostalhaPURSUE(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
        : base(_rasgaMortalha, _anim, _patrolPoints)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}

public class RasgaMostalhaRUNAWAY : StateRasgaMortalha
{
    public RasgaMostalhaRUNAWAY(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
        : base(_rasgaMortalha, _anim, _patrolPoints)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
