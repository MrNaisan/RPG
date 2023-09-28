using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandState : EnemyState
{
    bool idle;
    bool stun;

    public EnemyStandState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        idle = false;
        stun = false;

        enemy.animator.SetTrigger("stand");
        enemy.healthSystem.isTakeDamage = true;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(enemy.idle)
        {
            idle = true;
        }
        if(enemy.stun)
        {
            stun = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(idle)
        {
            stateMachine.ChangeState(enemy.standing);
        }
        if(stun)
        {
            stateMachine.ChangeState(enemy.stunning);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.timePassedRunAttack = 0f;
    }
}
