using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyHealthSystem : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 3;
    public string Name;
    float maxHealt;
    [HideInInspector]
    public bool isTakeDamage = true;
    public VisualEffect DamageEffect;
    public delegate void Death();
    public event Death Die;

    private void Start() 
    {
        maxHealt = health;    
    }

    public void TakeDamage(float damageAmount)
    {
        if(!isTakeDamage) return;

        health -= damageAmount;
        DamageEffect.Play();
        UIManager.Default.ChangeEnemyHP(health, maxHealt, Name);
 
        if (health <= 0)
        {
            Die?.Invoke();
        }
    }

}
