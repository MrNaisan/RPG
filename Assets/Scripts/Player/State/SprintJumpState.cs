using UnityEngine;
public class SprintJumpState:State
{
    float jumpTime;
 
    public SprintJumpState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();

        character.animator.SetTrigger("sprintJump");
        inputSystem.Jump = false;

        jumpTime = 1f;
    }
 
    public override void Exit()
    {
        base.Exit();
    }
 
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        if (timePassed> jumpTime)
        {
            if(inputSystem.Sprint)
            {
                stateMachine.ChangeState(character.sprinting);
                character.animator.SetTrigger("move");
            }
            else
            {
                character.animator.SetTrigger("move");
                if(character.animator.GetBool("attackReady"))
                    stateMachine.ChangeState(character.combatting);
                else
                    stateMachine.ChangeState(character.standing);
            }
        }
    }
 
 
 
}