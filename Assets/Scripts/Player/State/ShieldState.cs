using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : State
{
    private GameObject shield;
    public ShieldState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    
        shield = character.Shield;
        clipLength = inputSystem.ShieldAnim.length;
        clipSpeed = 2;
    }

    public override void Enter()
    {
        base.Enter();

        character.isShieldAvailable = false;
        character.isTakeDamage = false;

        character.animator.SetTrigger("shield");
        UIManager.Default.SetCDImage(3);
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

    public void Shield()
    {
        character.StartCoroutine(ShieldCour());
    }

    IEnumerator ShieldCour()
    {
        shield.SetActive(true);

        yield return new WaitForSeconds(character.ShieldActiveTime);

        shield.SetActive(false);
        character.isTakeDamage = true;
        UIManager.Default.SkillCD(3, character.ShieldCD);

        yield return new WaitForSeconds(character.ShieldCD);

        character.isShieldAvailable = true;
    }
}
