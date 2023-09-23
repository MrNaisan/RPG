using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    bool idle;
    float stunTime;

    public EnemyStunState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        stunTime = 0f;
        idle = false;

        enemy.agent.enabled = false;
        enemy.stun = false;
        enemy.isTakeDamage = true;
        
        if(enemy.isSkillStun)
            enemy.animator.SetTrigger("skillStun");
        else
        {
            enemy.timePassedRunAttack = 0f;
            enemy.animator.SetTrigger("stun");
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(stunTime >= enemy.StunTime)
        {
            idle = true;
        }

        stunTime += Time.deltaTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(idle)
        {
            stateMachine.ChangeState(enemy.standing);
            enemy.animator.SetTrigger("stunEnd");
        }
    }
}
