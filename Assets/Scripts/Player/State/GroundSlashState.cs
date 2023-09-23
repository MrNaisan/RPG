using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlashState : State
{
    bool attack;
    public GroundSlashState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;

        clipLength = inputSystem.GroundSlashAnim.length;
        clipSpeed = 2;
    }

    public override void Enter()
    {
        base.Enter();

        attack = false;
        inputSystem.Attack = false;

        character.isGroundSlashAvailable = false;
        character.animator.SetTrigger("groundSlash");
        UIManager.Default.SetCDImage(5);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if(inputSystem.Attack)
        {
            attack = true;
        }
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(timePassed >= clipLength / clipSpeed && attack)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }
        else if(timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }
    }

    public void Slash()
    {
        var projectile = GameObject.Instantiate(character.groundSlash, character.transform.position, Quaternion.identity);
        projectile.Initialize(character.transform);
        character.StartCoroutine(GroundSlashCour());
    }

    IEnumerator GroundSlashCour()
    {
        UIManager.Default.SkillCD(5, character.GroundSlashCD);
        yield return new WaitForSeconds(character.GroundSlashCD);
        character.isGroundSlashAvailable = true;
    }
}
