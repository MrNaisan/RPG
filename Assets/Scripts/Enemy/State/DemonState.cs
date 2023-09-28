using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonState
{
    public Demon demon;
    public DemonStateMachine stateMachine;
 
    public DemonState(Demon _demon, DemonStateMachine _stateMachine)
    {
        demon = _demon;
        stateMachine = _stateMachine;
    }
 
    public virtual void Enter()
    {
        
    }
 
    public virtual void HandleInput()
    {
    }
 
    public virtual void LogicUpdate()
    {
    }
 
    public virtual void PhysicsUpdate()
    {
    }
 
    public virtual void Exit()
    {
    }
}
