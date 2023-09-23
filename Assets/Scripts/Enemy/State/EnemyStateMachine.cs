using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState;
 
    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }
 
    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
 
        currentState = newState;
        newState.Enter();
    }
}
