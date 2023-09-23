using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BuffState : State
{

    public BuffState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;

        clipLength = inputSystem.BuffAnim.length;
        clipSpeed = 1;
    }

    public override void Enter()
    {
        base.Enter(); 

        character.isBuffAvailable = false;
        character.animator.SetTrigger("buff");
        character.Buff.Play();
        UIManager.Default.SetCDImage(1);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        character.SetBuff();
        character.StartCoroutine(BuffCour());
    }

    IEnumerator BuffCour()
    {
        UIManager.Default.SkillCD(1, character.BuffCD);
        yield return new WaitForSeconds(character.BuffCD);
        character.isBuffAvailable = true;
    }
}
