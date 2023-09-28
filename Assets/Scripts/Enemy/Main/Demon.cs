using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Demon : MonoBehaviour
{
    public DemonStateMachine movementSM;
    public DemonStandingState standing;
    public DemonAttackState attacking;
    public DemonDeathState dying;

    [Header("Combat")]
    public float attackCD = 3f;
    public float attackRange = 1f;
 
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public EnemyHealthSystem healthSystem;

    [HideInInspector]
    public float timePassed;
    [HideInInspector]
    public bool idle = false;
    public EnemyDamageDealer[] damageDealers;
    public VisualEffect DeathEffect;
 
    void Start()
    {
        movementSM = new DemonStateMachine();
        standing = new DemonStandingState(this, movementSM);
        attacking = new DemonAttackState(this, movementSM);
        dying = new DemonDeathState(this, movementSM);

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Character>().gameObject;
        healthSystem = GetComponent<EnemyHealthSystem>();

        movementSM.Initialize(standing);

        healthSystem.Die += Die;
    }
 
    void Update()
    {
        movementSM.currentState.HandleInput();
 
        movementSM.currentState.LogicUpdate();

        timePassed += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public void Die()
    {
        movementSM.ChangeState(dying);
    }

    public void SetIdle()
    {
        idle = true;
    }

    public void StartDealDamage(int num)
    {
        damageDealers[num].StartDealDamage();
    }
    public void EndDealDamage(int num)
    {
        damageDealers[num].EndDealDamage();
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnDestroy() 
    {
        healthSystem.Die -= Die;    
    }
}
