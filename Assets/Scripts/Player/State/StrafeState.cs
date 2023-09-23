using UnityEngine;

public class StrafeState : State
{
    public StrafeState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;

        clipSpeed = 1;
    }

    public override void Enter()
    {
        base.Enter();

        velocity = Vector3.zero;
        gravityVelocity.y = 0;   
 

        if (inputSystem.Strafe && character.direction != 0)
        {
            character.isDirectionSetable = false;
            character.animator.SetFloat("direction", character.direction);
            clipLength = inputSystem.StrafeAnims[(int)character.direction - 1].length;
            character.animator.SetTrigger("dodge");
        }     
        else
        {
            if(character.animator.GetBool("attackReady"))
                stateMachine.ChangeState(character.combatting);
            else
                stateMachine.ChangeState(character.standing);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(timePassed >= clipLength / clipSpeed)
        {
            character.isDirectionSetable = true;
            if(character.animator.GetBool("attackReady"))
                stateMachine.ChangeState(character.combatting);
            else
                stateMachine.ChangeState(character.standing);
        }
    }

    public void Strafe()
    {
        if(character.direction == 1)
        {
            character.controller.Move(character.transform.forward * 5);
        }
        else if(character.direction == 2)
        {
            character.controller.Move(-character.transform.forward * 5);
        }
        else if(character.direction == 3)
        {
            character.controller.Move(-character.transform.right * 5);
        }
        else if(character.direction == 4)
        {
            character.controller.Move(character.transform.right * 5);
        }
    }
}
