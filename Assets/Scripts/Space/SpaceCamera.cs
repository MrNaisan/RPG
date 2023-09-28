using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float height = 5f;
    public float rotationSpeed = 5f;

    private Vector3 offset;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void Start()
    {
        offset = new Vector3(0f, height, -distance);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Confined;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentRotationX += mouseX;
            currentRotationY += mouseY;
        }

        Quaternion rotation = Quaternion.Euler(-currentRotationY , currentRotationX, 0);
        Vector3 rotatedOffset = rotation * offset;

        transform.position = target.position + rotatedOffset;

        transform.LookAt(target.position);
    }
}
