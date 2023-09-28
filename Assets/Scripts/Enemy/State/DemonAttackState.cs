using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAttackState : DemonState
{
    bool idle;

    public DemonAttackState(Demon _demon, DemonStateMachine _stateMachine) : base(_demon, _stateMachine)
    {
        demon = _demon;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        idle = false;
        
        demon.agent.enabled = false;
        demon.animator.SetFloat("attackNum",Random.Range(1, 4));
        demon.animator.SetTrigger("attack");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(demon.idle)
        {
            idle = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(idle)
        {
            stateMachine.ChangeState(demon.standing);
        }
    }

    public override void Exit()
    {
        base.Exit();

        demon.timePassed = 0f;
    }
}
