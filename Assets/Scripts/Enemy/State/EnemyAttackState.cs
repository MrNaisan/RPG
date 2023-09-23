using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    bool idle;
    bool stun;

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        idle = false;
        stun = false;
        
        enemy.agent.enabled = false;
        enemy.animator.SetTrigger("attack");
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

        enemy.timePassedAttack = 0f;
    }
}
