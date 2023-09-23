using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public Enemy enemy;
    public EnemyStateMachine stateMachine;
 
    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine)
    {
        enemy = _enemy;
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
