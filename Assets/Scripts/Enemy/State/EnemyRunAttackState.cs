using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunAttackState : EnemyState
{
    bool stun;
    bool stand;
    float runTime;

    public EnemyRunAttackState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.runAttack = false;

        stun = false;
        stand = false;
        runTime = 0f;

        enemy.StartDealDamage(2);

        enemy.animator.SetTrigger("runAttack");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(enemy.stun)
        {
            stun = true;
        }
        if(runTime >= enemy.runAttackTime)
        {
            stand = true;
        }

        runTime += Time.deltaTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(stun)
        {
            stateMachine.ChangeState(enemy.stunning);
        }
        if(stand)
        {
            stateMachine.ChangeState(enemy.stand);
        }
    }

    public override void Exit()
    {
        base.Exit();
 
        enemy.EndDealDamage(2);
    }
}
