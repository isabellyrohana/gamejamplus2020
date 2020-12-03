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

    protected bool IsplayerVisible()
    {
        return PlayerController.Instance.GetPlayerOnTheLight();
    }

    protected Vector2 BezierCurve(Vector2 b0, Vector2 b1, Vector2 b2, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        return 
            (1 - t) * (1 - t) * b0
            + 2 * t * (1 - t) * b1
            + t * t * b2;
    }

    protected Vector2 PlayerPosition()
    {
        return PlayerController.Instance.transform.position;
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

public class RasgaMostalhaPURSUE : StateRasgaMortalha
{
    float startTime = 0f;
    float distance = 0f;
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 finalPosition;
    Vector2 lastPosition;

    public RasgaMostalhaPURSUE(GameObject _rasgaMortalha, Animator _anim, Transform[] _patrolPoints)
        : base(_rasgaMortalha, _anim, _patrolPoints)
    {
        name = STATE.IDLE;
        Speed = 5f;
        targetPosition = PlayerPosition();
        startPosition = RasgaMortalha.transform.position;

        float r = 0;
        if (startPosition.x > targetPosition.x)
        {
            r = startPosition.x - targetPosition.x;
            finalPosition = new Vector2(targetPosition.x - r, targetPosition.y);
        }
        else
        {
            r = targetPosition.x - startPosition.x;
            finalPosition = new Vector2(targetPosition.x + r, targetPosition.y);
        }
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
        distance = Vector2.Distance(RasgaMortalha.transform.position, finalPosition);
        lastPosition = BezierCurve(startPosition, targetPosition, finalPosition, 0);
        RasgaMortalha.transform.position = lastPosition;

    }

    public override void Update()
    {
        base.Update();
        
        float distCovered = (Time.time - startTime) * Speed;

        float percent = distCovered / distance;

        Vector2 currentPosition = BezierCurve(startPosition, targetPosition, finalPosition, 1f);

        RasgaMortalha.transform.position = Vector2.Lerp(
            lastPosition,
            currentPosition,
            percent
            );

        lastPosition = currentPosition;
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
