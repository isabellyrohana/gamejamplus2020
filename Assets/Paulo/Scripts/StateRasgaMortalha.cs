using System.Collections;
using System.Collections.Generic;
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

    #region Attributes
    public STAGE stage;
    public STATE name;
    public Transform player;
    public Transform[] patrolLocal;
    public IARasgaMortalha iARasgaMortalha;
    public StateRasgaMortalha nextState;
    #endregion

    public StateRasgaMortalha(IARasgaMortalha _IARasgaMortalha, Transform _player, Transform[] _patrolLocals)
    {
        iARasgaMortalha = _IARasgaMortalha;
        player = _player;
        patrolLocal = _patrolLocals;

        stage = STAGE.ENTER;
    }

    public virtual void ENTER() { stage = STAGE.UPDATE; }
    public virtual void UPDATE() { stage = STAGE.UPDATE; }
    public virtual void EXIT() { stage = STAGE.EXIT; }

    public StateRasgaMortalha Process()
    {
        if (stage == STAGE.ENTER) { ENTER(); }
        if (stage == STAGE.UPDATE) { UPDATE(); }
        if (stage == STAGE.ENTER)
        {
            EXIT();
            return nextState;
        }
        return this;
    }

    public bool IsPLayerOnLight()
    {
        return PlayerController.Instance.GetPlayerOnTheLight();
    }
}


public class RasgaMortalhaIDLE: StateRasgaMortalha
{
    public RasgaMortalhaIDLE(IARasgaMortalha _IARasgaMortalha, Transform _player, Transform[] _patrolLocals)
        :base(_IARasgaMortalha, _player, _patrolLocals)
    {
        name = STATE.IDLE;
    }

    public override void ENTER()
    {
        base.ENTER();
    }

    public override void UPDATE()
    {
        base.UPDATE();
    }

    public override void EXIT()
    {
        base.EXIT();
    }
}

public class RasgaMortalhaPATROL : StateRasgaMortalha
{
    public RasgaMortalhaPATROL(IARasgaMortalha _IARasgaMortalha, Transform _player, Transform[] _patrolLocals)
        : base(_IARasgaMortalha, _player, _patrolLocals)
    {
        name = STATE.PATROL;
    }

    public override void ENTER()
    {
        base.ENTER();
    }

    public override void UPDATE()
    {
        base.UPDATE();
    }

    public override void EXIT()
    {
        base.EXIT();
    }
}


public class RasgaMortalhaPURSUE : StateRasgaMortalha
{
    public RasgaMortalhaPURSUE(IARasgaMortalha _IARasgaMortalha, Transform _player, Transform[] _patrolLocals)
        : base(_IARasgaMortalha, _player, _patrolLocals)
    {
        name = STATE.PURSUE;
    }

    public override void ENTER()
    {
        base.ENTER();
    }

    public override void UPDATE()
    {
        base.UPDATE();
    }

    public override void EXIT()
    {
        base.EXIT();
    }
}
public class RasgaMortalhaRUNAWAY : StateRasgaMortalha
{
    public RasgaMortalhaRUNAWAY(IARasgaMortalha _IARasgaMortalha, Transform _player, Transform[] _patrolLocals)
        : base(_IARasgaMortalha, _player, _patrolLocals)
    {
        name = STATE.RUNAWAY;
    }

    public override void ENTER()
    {
        base.ENTER();
    }

    public override void UPDATE()
    {
        base.UPDATE();
    }

    public override void EXIT()
    {
        base.EXIT();
    }
}
