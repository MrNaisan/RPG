using UnityEngine;
public class CombatState : State
{
    float gravityValue;
    bool jump;
    bool sprint;
    bool dodge;
    bool grounded;
    bool sheathWeapon;
    bool attack;
    bool buff;
    bool fire;
    bool shield;
 
    Vector3 cVelocity;
 
    public CombatState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }

    public override void Enter()
    {
        base.Enter();

        jump = false;
        sprint = false;
        dodge = false;
        sheathWeapon = false;
        buff = false;
        fire = false;
        shield = false;

        gravityVelocity.y = 0;
        character.attackNum = 0;
        inputSystem.Attack = false;
        attack = false;
        
 
        velocity = character.playerVelocity;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

    }
 
    public override void HandleInput()
    {
        base.HandleInput();

        if(inputSystem.Shield && character.isShieldAvailable)
        {
            shield = true;
        }
        if(inputSystem.Fire && character.isFireAvailable)
        {
            fire = true;
        }
        if(inputSystem.Buff && character.isBuffAvailable)
        {
            buff = true;
        }
        if(inputSystem.Strafe)
        {
            dodge = true;
        }
        if (inputSystem.Jump)
        {
            jump = true;
        }
        if (inputSystem.Sprint)
        {
            sprint = true;
        }
        if (inputSystem.Draw)
        {
            sheathWeapon = true;
        }
        if (inputSystem.Attack)
        {
            attack = true;
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

        if(shield)
        {
            stateMachine.ChangeState(character.shielding);
        }
        if(fire)
        {
            stateMachine.ChangeState(character.shooting);
        }
        if(buff)
        {
            stateMachine.ChangeState(character.buffing);
        }
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
        if (sheathWeapon)
        {
            character.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(character.standing);
        }
        if (attack)
        {
            if(character.direction == 2 && character.isGroundSlashAvailable)
            {
                stateMachine.ChangeState(character.groundSlashing);
            }
            else
            {
                stateMachine.ChangeState(character.attacking);
            }
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

        if(velocity.sqrMagnitude > 0)
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