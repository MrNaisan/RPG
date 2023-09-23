using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    bool stop;
    bool off;

    public DeathState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }

    public override void Enter()
    {
        base.Enter();

        stop = false;
        off = false;

        character.animator.SetTrigger("death");
        character.DeathEffect.Play();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(timePassed >= 6f && !off)
        {
            off = true;
        }
        else if(timePassed >= 4 && !stop)
        {
            stop = true;
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(stop)
        {
            character.DeathEffect.Stop();
        }
        if(off)
        {
            character.gameObject.SetActive(false);
        }
    }
}
