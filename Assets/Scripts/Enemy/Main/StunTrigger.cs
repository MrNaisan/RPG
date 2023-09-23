using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTrigger : MonoBehaviour
{
    Enemy enemy;

    private void Start() 
    {
        enemy = GetComponentInParent<Enemy>();    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<Block>(out _) && enemy.movementSM.currentState == enemy.runAttacking)
        {
            enemy.SetStun();
        }
    }
}
