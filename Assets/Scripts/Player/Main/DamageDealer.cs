using System.Collections.Generic;
using System.Collections;
using UnityEngine;
 
public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;
 
    [SerializeField] float weaponDamage;
    EnemyHealthSystem enemy;
    public bool isAlreadyAttack = false;
    public bool isDealStun = false;
    float damageMultiply = 1f;

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();

        if(isAlreadyAttack)
            canDealDamage = true;
    }
 
    void Update()
    {
        if (canDealDamage)
        {
            if (enemy != null && !hasDealtDamage.Contains(enemy.transform.gameObject))
            {
                enemy.TakeDamage(weaponDamage * damageMultiply);
                hasDealtDamage.Add(enemy.transform.gameObject);
                if(isDealStun && enemy.gameObject.TryGetComponent<Enemy>(out Enemy en))
                {
                    en.SetStun(true);
                }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
 
    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent(out EnemyHealthSystem enemy))
            this.enemy = enemy;    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.TryGetComponent(out EnemyHealthSystem enemy) && this.enemy == enemy)
            this.enemy = null;   
    }

    public void MultiplyDamage(float multiplaer, float buffTime)
    {
        StartCoroutine(Multiply(multiplaer, buffTime));
    }

    IEnumerator Multiply(float multiplaer, float buffTime)
    {
        damageMultiply = multiplaer;
        yield return new WaitForSeconds(buffTime);
        damageMultiply = 1f;
    }
}