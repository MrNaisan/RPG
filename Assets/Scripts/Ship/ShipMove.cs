using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float turnSpeed;
    public float flySpeed;

    private Vector3 rotationInput;
    private Quaternion targetRotation;
    private Vector3 rotationVelocity;

    private void Update()
    {
        InputControl();
        Turn();
        Fly();
    }

    void Turn()
    {
        // Затухание поворота
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Fly()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime * Input.GetAxis("Fly");
    }

    void InputControl()
    {
        rotationInput = new Vector3(
            Input.GetKey(KeyCode.S) ? 1 : Input.GetKey(KeyCode.W) ? -1 : 0,
            Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
            Input.GetKey(KeyCode.Q) ? 1 : Input.GetKey(KeyCode.E) ? -1 : 0
        );

        // Обновляем целевой поворот на основе входных данных
        if (rotationInput != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles + (rotationInput * turnSpeed * Time.deltaTime));
            targetRotation = newRotation;
        }
        else
        {
            // Если кнопка не нажата, продолжаем вращение с затуханием
            Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles + (rotationVelocity * Time.deltaTime));
            targetRotation = newRotation;
            rotationVelocity = Vector3.Lerp(rotationVelocity, Vector3.zero, 2.0f * Time.deltaTime);
        }

        // Обновляем скорость поворота
        if (rotationInput != Vector3.zero)
            rotationVelocity = rotationInput * turnSpeed;
    }
}
