using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 100;

    float maxHP;
    Character character;
    Animator animator;
    bool isDied;

    void Start()
    {
        maxHP = health;
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        UIManager.Default.maxHP = maxHP;
    }

    public void RegenHP()
    {
        StartCoroutine(RegenCour());
    }

    IEnumerator RegenCour()
    {
        float time = 0f;
        while(time < character.BuffTime)
        {
            yield return new WaitForSeconds(1f);
            health += character.RegenPerSecond;
            if(health > maxHP)
                health = maxHP;
            UIManager.Default.ChangeHP(health);
            time += 1f;
        }
    }
 
    public void TakeDamage(float damageAmount)
    {
        if(!character.isTakeDamage || isDied) return;

        health -= damageAmount;
        animator.SetTrigger("damage");
        UIManager.Default.Damage();
        UIManager.Default.ChangeHP(health);
 
        if (health <= 0)
        {
            Die();
        }
    }
 
    void Die()
    {
        isDied = true;
        character.movementSM.ChangeState(character.dying);
    }
}