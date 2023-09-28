using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDeathState : DemonState
{
    bool stop;
    bool off;
    float time;

    public DemonDeathState(Demon _demon, DemonStateMachine _stateMachine) : base(_demon, _stateMachine)
    {
        demon = _demon;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        stop = false;
        off = false;
        time = 0f;

        demon.agent.enabled = false;
        demon.healthSystem.isTakeDamage = false;

        demon.animator.SetTrigger("death");
        demon.DeathEffect.Play();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(time >= 5.15f && !off)
        {
            off = true;
        }
        else if(time >= 3.15 && !stop)
        {
            stop = true;
        }

        time += Time.deltaTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(stop)
        {
            demon.DeathEffect.Stop();
        }
        if(off)
        {
            demon.gameObject.SetActive(false);
        }
    }
}
