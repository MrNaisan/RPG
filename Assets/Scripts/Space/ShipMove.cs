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

    public VisualEffect SpeedEffect;

    private void Start() 
    {
        maxHp = hp;
        rb = GetComponent<Rigidbody>();    
        time = speedTime;
    }

    private void Update()
    {
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
        Vector3 flyForceVector = transform.forward * flyInput * flySpeed * (flyInput > 0 && isSpeedActivate ? speedMultuply : 1);

        rb.AddForce(flyForceVector, ForceMode.Force);
    }

    void InputControl()
    {
        rotationInput = new Vector3(
            Input.GetKey(KeyCode.LeftControl) ? 1 : Input.GetKey(KeyCode.LeftShift) ? -1 : 0,
            Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
            Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0
        );

        flyInput = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0; 
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
                isEffectPlay = true;
                SpeedEffect.Play();
            }
        }
        else
        {
            if(isEffectPlay)
            {
                isEffectPlay = false;
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
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }    
    }
}
