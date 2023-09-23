using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    bool stop;
    bool off;
    float time;

    public EnemyDeathState(Enemy _enemy, EnemyStateMachine _stateMachine) : base(_enemy, _stateMachine)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        stop = false;
        off = false;
        time = 0f;

        enemy.agent.enabled = false;
        enemy.isTakeDamage = false;

        enemy.animator.SetTrigger("death");
        enemy.DeathEffect.Play();
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
            enemy.DeathEffect.Stop();
        }
        if(off)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
