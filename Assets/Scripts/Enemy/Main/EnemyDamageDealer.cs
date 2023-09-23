using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
 
    [SerializeField] float weaponDamage;
    HealthSystem player;

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }
 
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            if (player != null)
            {
                player.TakeDamage(weaponDamage);
                hasDealtDamage = true;
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent(out HealthSystem health))
            player = health;    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.TryGetComponent(out HealthSystem health))
            player = null;   
    }
}