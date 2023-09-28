using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStateMachine
{
    public DemonState currentState;
 
    public void Initialize(DemonState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }
 
    public void ChangeState(DemonState newState)
    {
        currentState.Exit();
 
        currentState = newState;
        newState.Enter();
    }
}
