using UnityEngine;

public class StatePorcoVela
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, SCARY
    }

    public enum STAGE
    {
        ENTER, UPDATE, EXIT
    }

    #region Attributes

    public STATE name;
    public STAGE stage;
    public IAPorcoVela IAPorcoVela;
    public Animator anim;
    public StatePorcoVela nextState;
    public Transform[] patrolPoints;
    public Transform pursueTarget;

    #endregion

    public StatePorcoVela(GameObject _porcoVela, Animator _anim, Transform[] _patrolPoints, Transform _pursueTarget)
    {
        IAPorcoVela = _porcoVela.GetComponent<IAPorcoVela>();
        anim = _anim;
        patrolPoints = _patrolPoints;
        pursueTarget = _pursueTarget;

        stage = STAGE.ENTER;
    }

    public virtual void Enter() { stage = STAGE.UPDATE; }
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
    public StatePorcoVelaIDLE(GameObject porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        :base(porcoVela, anim, _patrolPoints, _pursueTarget)
    {
        name = STATE.IDLE;
        IAPorcoVela.Speed = 1f;
        Debug.Log("Entrou IDLE");
    }

    public override void Enter()
    {
        base.Enter();
        //TODO verificar
        //anim.SetTrigger("IDLE");
    }

    public override void Update()
    {
        base.Update();

        float valorRandom = Random.Range(0, 1000f);
        if(valorRandom <= 75f)
        {
            stage = STAGE.EXIT;
        }

    }

    public override void Exit()
    {
        //anim.SetTrigger("IDLE");

        //TODO verificar
        nextState = new StatePorcoVelaPATROL(IAPorcoVela.gameObject, anim, patrolPoints, pursueTarget);

        base.Exit();
    }
}

public class StatePorcoVelaPATROL: StatePorcoVela
{
    private int nextTarget = 0;

    public StatePorcoVelaPATROL(GameObject porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
    {
        name = STATE.PATROL;
        IAPorcoVela.Speed = 1f;
        Debug.Log("Entrou PATROL");
    }

    public override void Enter()
    {
        base.Enter();
        nextTarget = 0;
    }

    public override void Update()
    {
        base.Update();

        if (IsPlayerVisible())
        {
            nextState = new StatePorcoVelaPURSUE(IAPorcoVela.gameObject, anim, patrolPoints, pursueTarget);
            stage = STAGE.EXIT;

            return;
        }

        if (Vector2.Distance(IAPorcoVela.transform.position, patrolPoints[nextTarget % patrolPoints.Length].position) > .1f)
        {
            IAPorcoVela.transform.position =
                Vector2.MoveTowards(IAPorcoVela.transform.position, patrolPoints[nextTarget % patrolPoints.Length].position, IAPorcoVela.Speed * Time.deltaTime);
        }
        else 
        {
            int shouldStay = Random.Range(0, 1000);

            if (shouldStay < 800)
            {
                nextState = new StatePorcoVelaIDLE(IAPorcoVela.gameObject, anim, patrolPoints, pursueTarget);
                stage = STAGE.EXIT;
            }
            else
                nextTarget++;

        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class StatePorcoVelaPURSUE: StatePorcoVela
{
    public StatePorcoVelaPURSUE(GameObject porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
    {
        name = STATE.PURSUE;
        Debug.Log("Entrou PURSUE");
    }

    public override void Enter()
    {
        base.Enter();
        IAPorcoVela.Speed = 4f;
    }

    public override void Update()
    {
        base.Update();

        IAPorcoVela.transform.position = 
            Vector2.MoveTowards(
                                IAPorcoVela.transform.position,
                                pursueTarget.position,
                                IAPorcoVela.Speed * Time.deltaTime
                                );

        if (!IsPlayerVisible())
        {
            nextState = new StatePorcoVelaIDLE(IAPorcoVela.gameObject, anim, patrolPoints, pursueTarget);
            stage = STAGE.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class StatePorcoVelaSCARY : StatePorcoVela
{
    public StatePorcoVelaSCARY(GameObject porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
    {
        name = STATE.SCARY;
        Debug.Log("Entrou SCARY");
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        stage = STAGE.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }

}


public class StatePorcoVelaDOUBT: StatePorcoVela
{
    public StatePorcoVelaDOUBT(GameObject porcoVela, Animator anim, Transform[] _patrolPoints, Transform _pursueTarget)
        : base(porcoVela, anim, _patrolPoints, _pursueTarget)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        stage = STAGE.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
        nextState = new StatePorcoVelaPATROL(IAPorcoVela.gameObject, anim, patrolPoints, pursueTarget);
    }

}