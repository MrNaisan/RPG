using UnityEngine;
public class AttackState : State
{
    bool attack;
    bool groundSlash;

    public AttackState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;

        clipSpeed = 2;
    }
 
    public override void Enter()
    {
        base.Enter();

        inputSystem.Attack = false;
        groundSlash = false;
        attack = false;

        timePassed = 0f;
        character.attackNum++;

        if(character.attackNum >= 3)
        {
            character.attackNum = 0;
            if(character.isSlashAvailable)   
                stateMachine.ChangeState(character.slashing);
            else
            {
                character.attackNum++;
                character.animator.SetTrigger("attack");
                Sounds.Default.PlayerAttack();
            }
        }
        else
        {
            character.animator.SetTrigger("attack");
            Sounds.Default.PlayerAttack();
        }
        
        character.animator.SetFloat("speed", 0, character.speedDampTime, Time.deltaTime);
    }
 
    public override void HandleInput()
    {
        base.HandleInput();

        if (inputSystem.Attack)
        {
            attack = true;
        }
        if(character.direction == 2 && character.isGroundSlashAvailable)
        {
            groundSlash = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        clipLength = inputSystem.AttackAnims[character.attackNum-1].length;

        if (timePassed >= clipLength / clipSpeed && attack)
        {
            if(groundSlash)
            {
                groundSlash = false;
                character.animator.SetFloat("direction", 2);
                stateMachine.ChangeState(character.groundSlashing);
            }
            else
                stateMachine.ChangeState(character.attacking);
        }
        else if (timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("move");
        }
 
    }

     public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        var direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(direction), character.rotationDampTime);
    }
}