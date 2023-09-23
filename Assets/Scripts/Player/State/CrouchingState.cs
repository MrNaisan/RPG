using UnityEngine;
 
public class CrouchingState : State
{
    bool belowCeiling;
    bool crouchHeld;

    bool grounded;
    float gravityValue;
 
 
    public CrouchingState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        character.animator.SetTrigger("crouch");  
        belowCeiling = false;
        crouchHeld = false;
        gravityVelocity.y = 0;
        inputSystem.Crouch = false;
        
        character.controller.height = character.crouchColliderHeight;
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
 
        
    }
 
    public override void Exit()
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
        if (inputSystem.Crouch && !belowCeiling)
        {
            crouchHeld = true;
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
 
        if (inputSystem.Crouch && crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
            character.animator.SetTrigger("move");
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight);
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
 
    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        int layerMask = character.gameObject.layer;
        RaycastHit hit;
 
        Vector3 direction = targetPositon - character.transform.position;
        if (Physics.Raycast(character.transform.position, direction, out hit, character.normalColliderHeight, layerMask))
        {
            Debug.Log(hit.collider);
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.white);
            return false;
        }       
    }
 
 
}