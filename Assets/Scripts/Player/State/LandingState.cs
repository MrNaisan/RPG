using UnityEngine;
 
public class LandingState:State
{
    float landingTime;
 
    public LandingState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();
        
        character.animator.SetTrigger("land");
        landingTime = 0.5f;
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (timePassed> landingTime)
        {
            character.animator.SetTrigger("move");
            if(character.animator.GetBool("attackReady"))
                stateMachine.ChangeState(character.combatting);
            else
                stateMachine.ChangeState(character.standing);
        }
    }
 
 
 
}