using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoarState : EnemyState
{
    bool runAttack;
    bool stun;

    public EnemyRoarState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        runAttack = false;
        stun = false;

        enemy.agent.enabled = false;
        enemy.healthSystem.isTakeDamage = false;
        Sounds.Default.GolemRoar();
        enemy.animator.SetTrigger("roar");
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(enemy.runAttack)
        {
            runAttack = true;
        }
        if(enemy.stun)
        {
            stun = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(runAttack)
        {
            stateMachine.ChangeState(enemy.runAttacking);
        }
        if(stun)
        {
            stateMachine.ChangeState(enemy.stunning);
            enemy.timePassedRunAttack = 0f;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 targetDirection = enemy.player.transform.position - enemy.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);
        desiredRotation.x = desiredRotation.z = 0f;
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, desiredRotation, 5 * Time.deltaTime);
    }
}
