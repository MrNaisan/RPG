using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashState : State
{
    bool attack;

    public SlashState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;

        clipLength = inputSystem.SlashAnim.length;
        clipSpeed = 2;
    }

    public override void Enter()
    {
        base.Enter();

        inputSystem.Attack = false;
        character.isSlashAvailable = false;
        character.animator.SetTrigger("slash");
        UIManager.Default.SetCDImage(4);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (inputSystem.Attack)
        {
            attack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (timePassed >= clipLength / clipSpeed && attack)
        {
            character.StartCoroutine(SlashCour());
            stateMachine.ChangeState(character.attacking);
        }
        else if (timePassed >= clipLength / clipSpeed)
        {
            character.StartCoroutine(SlashCour());
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.combatting);
        }
 
    }

    IEnumerator SlashCour()
    {
        UIManager.Default.SkillCD(4, character.SlashCD);
        yield return new WaitForSeconds(character.SlashCD);
        character.isSlashAvailable = true;
    }
}
