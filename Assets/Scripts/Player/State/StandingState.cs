using UnityEditor.Experimental.GraphView;
using UnityEngine;
 
public class StandingState: State
{
    float gravityValue;
    bool jump;   
    bool crouch;
    bool grounded;
    bool sprint;
    bool dodge;
    bool drawWeapon;
 
    Vector3 cVelocity;
 
    public StandingState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        jump = false;
        crouch = false;
        sprint = false;
        drawWeapon = false;
        dodge = false;

        velocity = Vector3.zero;
        gravityVelocity.y = 0;
 
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

    }
 
    public override void HandleInput()
    {
        base.HandleInput();

        if(inputSystem.Strafe)
        {
            dodge = true;
        }
        if (inputSystem.Jump)
        {
            jump = true;
        }
        if (inputSystem.Crouch)
        {
            crouch = true;
        }
        if (inputSystem.Sprint)
        {
            sprint = true;
        }
        if (inputSystem.Draw)
        {
            drawWeapon = true;
        }

        velocity = inputSystem.Velocity;

        if(velocity.magnitude > 1)
        {
            velocity.Normalize();
        }

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(character.direction == 1f)
            character.animator.SetFloat("speed", inputSystem.Velocity.magnitude, character.speedDampTime, Time.deltaTime);
        else
            character.animator.SetFloat("speed", 0, character.speedDampTime, Time.deltaTime);

        if (dodge)
        {
            stateMachine.ChangeState(character.dodging);
        }
        if (sprint)
        {
            stateMachine.ChangeState(character.sprinting);
        }    
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
        if (crouch)
        {
            stateMachine.ChangeState(character.crouching);
        }
        if (drawWeapon)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
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
            Vector3 direction;
            
            if(character.direction > 1.01f)
                direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            else
                direction = velocity;
            
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(direction), character.rotationDampTime);
        }
        
    }
 
    public override void Exit()
    {
        base.Exit();
 
        gravityVelocity.y = 0f;
    }
 
}