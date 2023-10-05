using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShipMove : MonoBehaviour
{
    public float hp;
    public float turnSpeed;
    public float flySpeed;
    public float speedTime;

    private Vector3 rotationInput;
    private float flyInput;
    private Rigidbody rb;
    public float speedMultuply = 2f;
    private bool isSpeedActivate;
    private bool isSpeedAvailable = true;
    private float maxHp;
    private float time;
    private bool isEffectPlay = false;
    private Animator anim;
    private bool isDie = false;
    private bool isFly = false;

    public VisualEffect SpeedEffect;
    public VisualEffect MoveEffect;
    float particleSpeedStandart = 3f;
    float particleSpeedAccelerator = 9f;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        maxHp = hp;
        rb = GetComponent<Rigidbody>();    
        time = speedTime;
    }

    private void Update()
    {
        if(isDie) return;

        InputControl();
        Turn();
        Fly();
        Speed();
    }

    void Turn()
    {
        Vector3 rotationTorque = transform.TransformDirection(rotationInput) * turnSpeed;
        rb.AddTorque(rotationTorque, ForceMode.Force);
    }

    void Fly()
    {
        if(flyInput != 0 && !isFly)
        {
            isFly = true;
            Sounds.Default.ShipMove();
            if(flyInput > 0)
                MoveEffect.Play();
            else
                MoveEffect.Stop();
        }
        else if(flyInput == 0 && isFly)
        {
            isFly = false;
            Sounds.Default.ShipMove(false);
            MoveEffect.Stop();
        }

        Vector3 flyForceVector = transform.forward * flyInput * flySpeed * (flyInput > 0 && isSpeedActivate ? speedMultuply : 1);

        rb.AddForce(flyForceVector, ForceMode.Force);
    }

    void InputControl()
    {
        rotationInput = new Vector3(
            Input.GetKey(KeyCode.S) ? 1 : Input.GetKey(KeyCode.W) ? -1 : 0,
            Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
            Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0
        );

        flyInput = Input.GetKey(KeyCode.LeftShift) ? 1 : Input.GetKey(KeyCode.LeftControl) ? -1 : 0; 
    }

    void Speed()
    {
        if(Input.GetKey(KeyCode.Space) && time > 0 && isSpeedAvailable && flyInput > 0)
        {
            isSpeedActivate = true;
            time -= Time.deltaTime;
            SpaceUI.Default.ChangeSpeed(time, speedTime);
            if(!isEffectPlay)
            {
                MoveEffect.SetFloat("Speed", particleSpeedAccelerator);
                isEffectPlay = true;
                Sounds.Default.Accelerator();
                SpeedEffect.Play();
            }
        }
        else
        {
            if(isEffectPlay)
            {
                MoveEffect.SetFloat("Speed", particleSpeedStandart);
                isEffectPlay = false;
                Sounds.Default.Accelerator(false);
                SpeedEffect.Stop();
            }

            isSpeedActivate = false;

            if(time < speedTime)
            {
                time += Time.deltaTime / 4;
                SpaceUI.Default.ChangeSpeed(time, speedTime);
                if(time / speedTime < 0.2)
                {
                    isSpeedAvailable = false;
                }
                else
                {
                    isSpeedAvailable = true;
                }
            }
        }

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.TryGetComponent<SpaceDrift>(out _))
        {
            hp--;

            SpaceUI.Default.ChangeHp(hp, maxHp);
            SpaceUI.Default.Damage();

            Sounds.Default.ShipDamage();
            if(hp <= 0)
            {
                isDie = true;
                anim.SetTrigger("die");
                SpaceUI.Default.Die();
            }
        }    
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
