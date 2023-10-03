using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine movementSM;
    public EnemyStandingState standing;
    public EnemyStunState stunning;
    public EnemyAttackState attacking;
    public EnemyRunAttackState runAttacking;
    public EnemyRoarState roaring;
    public EnemyStandState stand;
    public EnemyDeathState dying;
 
    [Header("Combat")]
    public float attackCD = 3f;
    public float attackRange = 1f;
    public float runAttackCD = 10f;
    public float runAttackTime = 5f;
    public float aggroRange = 4f;
    public float StunTime = 5f;

    public EnemyDamageDealer[] damageDealers;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public EnemyHealthSystem healthSystem;

    [HideInInspector]
    public bool stun = false;
    [HideInInspector]
    public bool runAttack = false;
    [HideInInspector]
    public bool idle = false;
    [HideInInspector]
    public bool isSkillStun = false;

    [Header("Effects")]
    public VisualEffect DeathEffect;

    [HideInInspector]
    public float timePassedAttack;
    [HideInInspector]
    public float timePassedRunAttack;
 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Character>().gameObject;
        healthSystem = GetComponent<EnemyHealthSystem>();

        movementSM = new EnemyStateMachine();
        standing = new EnemyStandingState(this, movementSM);
        stunning = new EnemyStunState(this, movementSM);
        attacking = new EnemyAttackState(this, movementSM);
        runAttacking = new EnemyRunAttackState(this, movementSM);
        roaring = new EnemyRoarState(this, movementSM);
        stand = new EnemyStandState(this, movementSM);
        dying = new EnemyDeathState(this, movementSM);

        movementSM.Initialize(standing);

        healthSystem.Die += Die;
    }
 
    void Update()
    {
        movementSM.currentState.HandleInput();
 
        movementSM.currentState.LogicUpdate();

        timePassedAttack += Time.deltaTime;
        timePassedRunAttack += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public void Die()
    {
        movementSM.ChangeState(dying);
    }

    public void SetRunAttack()
    {
        runAttack = true;
    }

    public void SetStun(bool isPlayerSkill = false)
    {
        if(isPlayerSkill)
            isSkillStun = true;
        else
            isSkillStun = false;
        stun = true;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void OnDestroy() 
    {
        healthSystem.Die -= Die;    
    }

    public void Step()
    {
        Sounds.Default.GolemRun();
    }
}