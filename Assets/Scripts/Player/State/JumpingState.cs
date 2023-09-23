using UnityEngine;
 
public class JumpingState:State
{
    bool grounded;
 
    float gravityValue;
    float jumpHeight;
    float playerSpeed;
 
    Vector3 airVelocity;
 
    public JumpingState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;
        inputSystem.Jump = false;
 
        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!grounded)
        {
 
            velocity = character.playerVelocity;
            airVelocity = inputSystem.Velocity;

            if(velocity.magnitude > 1)
            {
                velocity.Normalize();
            }

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;
            character.controller.Move(gravityVelocity * Time.deltaTime+ (airVelocity*character.airControl+velocity*(1- character.airControl))*playerSpeed*Time.deltaTime);
        }
 
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }
 
    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
 
}