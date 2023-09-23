using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandingState : EnemyState
{
    bool stun;
    bool attack;
    bool roar;

    float newDestinationCD = 0.5f;

    public EnemyStandingState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.idle = false;
        enemy.agent.enabled = true;
        enemy.isTakeDamage = true;

        stun = false;
        attack = false;
        roar = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(enemy.stun)
        {
            stun = true;
        }
        if(enemy.timePassedAttack >= enemy.attackCD && Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= enemy.attackRange)
        {
            attack = true;
        }
        if(enemy.timePassedRunAttack >= enemy.runAttackCD && Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= enemy.aggroRange)
        {
            roar = true;
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.animator.SetFloat("speed", enemy.agent.velocity.magnitude / enemy.agent.speed);

        if (enemy.player == null)
        {
            return;
        }

        if(stun)
        {
            stateMachine.ChangeState(enemy.stunning);
        }
        if(roar)
        {
            stateMachine.ChangeState(enemy.roaring);
        }
        else if(attack)
        {  
            stateMachine.ChangeState(enemy.attacking);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (newDestinationCD <= 0 && Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= enemy.aggroRange)
        {
            newDestinationCD = 0.5f;
            enemy.agent.SetDestination(enemy.player.transform.position);
        }

        newDestinationCD -= Time.deltaTime;

        Vector3 targetDirection = enemy.player.transform.position - enemy.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);
        desiredRotation.x = desiredRotation.z = 0f;
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, desiredRotation, 5 * Time.deltaTime);
    }
}
