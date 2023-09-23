using UnityEngine;
using UnityEngine.InputSystem;
 
public class State
{
    public Character character;
    public StateMachine stateMachine;
    public InputSystem inputSystem;
 
    protected Vector3 gravityVelocity;
    protected Vector3 velocity;

    protected float timePassed;
    protected float clipLength;
    protected float clipSpeed;
 
    public State(Character _character, StateMachine _stateMachine, InputSystem _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    }
 
    public virtual void Enter()
    {
        timePassed = 0f;
    }
 
    public virtual void HandleInput()
    {
    }
 
    public virtual void LogicUpdate()
    {
        timePassed += Time.deltaTime;
    }
 
    public virtual void PhysicsUpdate()
    {
    }
 
    public virtual void Exit()
    {
    }
}