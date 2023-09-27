using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float turnSpeed;
    public float flySpeed;

    private Vector3 rotationInput;
    private float flyInput;
    private Rigidbody rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        InputControl();
        Turn();
        Fly();
    }

    void Turn()
    {
        Vector3 rotationTorque = transform.TransformDirection(rotationInput) * turnSpeed;
        rb.AddTorque(rotationTorque, ForceMode.Force);
    }

    void Fly()
    {
        Vector3 flyForceVector = transform.forward * flyInput * flySpeed;

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
}
