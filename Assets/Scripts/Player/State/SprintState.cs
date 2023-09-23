using UnityEngine;
public class SprintState : State
{
    float gravityValue;
 
    bool grounded;
    bool sprint;
    bool sprintJump;
    
    public SprintState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        sprint = false;
        sprintJump = false;
        velocity = Vector3.zero;
        gravityVelocity.y = 0;
 
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;       
    }
 
    public override void HandleInput()
    {
        base.Enter();
        velocity = inputSystem.Velocity;

        if(velocity.magnitude > 1)
        {
            velocity.Normalize();
        }
        
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if (inputSystem.Sprint)
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }

        if (inputSystem.Jump)
        {
            sprintJump = true;
 
        }
 
    }
 
    public override void LogicUpdate()
    {
        if (sprint)
        {
            character.animator.SetFloat("speed", inputSystem.Velocity.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
        }
        else
        {
            if(character.animator.GetBool("attackReady"))
                stateMachine.ChangeState(character.combatting);
            else
                stateMachine.ChangeState(character.standing);
        }
        if (sprintJump)
        {
            stateMachine.ChangeState(character.sprintjumping);
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
 
        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
}