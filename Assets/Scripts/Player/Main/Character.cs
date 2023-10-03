using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f; 
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;
 
    [Header("Animation Smoothing")]
    [Range(0, 1)]
    public float speedDampTime = 0.1f;
    [Range(0, 1)]
    public float velocityDampTime = 0.9f;
    [Range(0, 1)]
    public float rotationDampTime = 0.2f;
    [Range(0, 1)]
    public float airControl = 0.5f;
 
    public StateMachine movementSM;
    public StandingState standing;
    public JumpingState jumping;
    public CrouchingState crouching;
    public LandingState landing;
    public SprintState sprinting;
    public SprintJumpState sprintjumping;
    public CombatState combatting;
    public AttackState attacking;
    public StrafeState dodging;
    public BuffState buffing;
    public FireState shooting;
    public ShieldState shielding;
    public GroundSlashState groundSlashing;
    public SlashState slashing;
    public DeathState dying;
 
    [HideInInspector]
    public float gravityValue = -9.81f;
    [HideInInspector]
    public float normalColliderHeight;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public InputSystem inputSystem;
    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Vector3 playerVelocity;
    [HideInInspector]
    public float direction = 0f;
    [HideInInspector]
    public bool isDirectionSetable = true;

    HealthSystem health;
    DamageDealer weapon;

    [Header("Buff")]
    public VisualEffect Buff;
    public float BuffCD = 15f;
    public float BuffTime = 5f;
    public float RegenPerSecond = 1f;
    public float AttackMultiply = 2f;
    [HideInInspector]
    public bool isBuffAvailable = true;

    [Header("Fire")]
    public GameObject Projectile;
    public Transform FirePoint;
    public float FireCD = 5f;
    [HideInInspector]
    public bool isFireAvailable = true;

    [Header("Shield")]
    public GameObject Shield;
    public float ShieldCD = 15f;
    public float ShieldActiveTime = 10f;
    [HideInInspector]
    public bool isTakeDamage = true;
    [HideInInspector]
    public bool isShieldAvailable = true;

    [Header("Slash")]
    public VisualEffect Slash;
    public float SlashCD = 5f;
    public float SlashDamage = 2f;
    [HideInInspector]
    public bool isSlashAvailable = true;
    [HideInInspector]
    public int attackNum = 0;
    DamageDealer slashDamageDealer;

    [Header("GroundSlash")]
    public GroundSlash groundSlash;
    public float GroundSlashDamage = 3f;
    public float GroundSlashCD = 20f;
    [HideInInspector]
    public bool isGroundSlashAvailable = true;
    [Header("Death")]
    public VisualEffect DeathEffect;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputSystem = GetComponent<InputSystem>();
        cameraTransform = Camera.main.transform;
        health = GetComponent<HealthSystem>();
        weapon = GetComponent<EquipmentSystem>().weapon.GetComponentInChildren<DamageDealer>();
        slashDamageDealer = Slash.GetComponentInParent<DamageDealer>();
        Shield.GetComponentInChildren<VisualEffect>().SetFloat("ShieldDuration", ShieldActiveTime);
 
        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM, inputSystem);
        jumping = new JumpingState(this, movementSM, inputSystem);
        crouching = new CrouchingState(this, movementSM, inputSystem);
        landing = new LandingState(this, movementSM, inputSystem);
        sprinting = new SprintState(this, movementSM, inputSystem);
        sprintjumping = new SprintJumpState(this, movementSM, inputSystem);
        combatting = new CombatState(this, movementSM, inputSystem);
        attacking = new AttackState(this, movementSM, inputSystem);
        dodging = new StrafeState(this, movementSM, inputSystem);
        buffing = new BuffState(this, movementSM, inputSystem);
        shooting = new FireState(this, movementSM, inputSystem);
        shielding = new ShieldState(this, movementSM, inputSystem);
        groundSlashing = new GroundSlashState(this, movementSM, inputSystem);
        slashing = new SlashState(this, movementSM, inputSystem);
        dying = new DeathState(this, movementSM, inputSystem);
 
        movementSM.Initialize(standing);
 
        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

    }
 
    private void Update()
    {
        movementSM.currentState.HandleInput();
 
        movementSM.currentState.LogicUpdate();

        Direction();
    }
 
    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }

    public void AttackReady()
    {
        animator.SetBool("attackReady", true);
    }

    public void AttackEnd()
    {
        animator.SetBool("attackReady", false);
    }

    public void Strafe()
    {
        dodging.Strafe();
    }

    public void SetBuff()
    {
        health.RegenHP();
        weapon.MultiplyDamage(AttackMultiply, BuffTime);
    }

    public void GroundSlash()
    {
        Sounds.Default.GroundSlash();
        groundSlashing.Slash();
    }

    public void SlashAttack()
    {
        Slash.Play();
        SlashStartDealDamage();
    }

    public void SlashStartDealDamage()
    {
        slashDamageDealer.StartDealDamage();
    }
    public void SlashEndDealDamage()
    {
        slashDamageDealer.EndDealDamage();
    }

    public void ShieldActivate()
    {
        shielding.Shield();
    }

    public void Direction()
    {
        if(inputSystem.W)
        {
            direction = 1;
        }
        else if(inputSystem.S)
        {
            direction = 2;
        }
        else if(inputSystem.A)
        {
            direction = 3;
        }
        else if(inputSystem.D)
        {
            direction = 4;
        }
        else
            direction = 0;

        if(isDirectionSetable)
            animator.SetFloat("direction", direction);
    }
}