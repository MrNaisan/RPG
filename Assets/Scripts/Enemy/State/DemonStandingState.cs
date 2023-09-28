using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStandingState : DemonState
{
    bool attack;

    float newDestinationCD = 0.5f;

    public DemonStandingState(Demon _demon, DemonStateMachine _stateMachine) : base(_demon, _stateMachine)
    {
        demon = _demon;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        demon.agent.enabled = true;
        demon.healthSystem.isTakeDamage = true;

        attack = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(demon.timePassed >= demon.attackCD && Vector3.Distance(demon.player.transform.position, demon.transform.position) <= demon.attackRange)
        {
            attack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        demon.animator.SetFloat("speed", demon.agent.velocity.magnitude / demon.agent.speed);

        if (demon.player == null)
        {
            return;
        }

        if(attack)
        {  
            stateMachine.ChangeState(demon.attacking);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (newDestinationCD <= 0)
        {
            newDestinationCD = 0.5f;
            demon.agent.SetDestination(demon.player.transform.position);
        }

        newDestinationCD -= Time.deltaTime;

        Vector3 targetDirection = demon.player.transform.position - demon.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);
        desiredRotation.x = desiredRotation.z = 0f;
        demon.transform.rotation = Quaternion.Slerp(demon.transform.rotation, desiredRotation, 5 * Time.deltaTime);
    }
}
