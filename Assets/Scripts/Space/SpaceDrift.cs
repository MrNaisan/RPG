using UnityEngine;

public class SpaceDrift : MonoBehaviour
{
    public float driftForce = 10f; // Сила дрейфа
    public float repulsionForce = 10f;
    public float rotationSpeed = 2f; // Скорость вращения

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Отключаем гравитацию, чтобы объект дрейфовал в космосе
        rb.useGravity = false;
        // Задаем случайную начальную скорость объекту
        rb.velocity = Random.insideUnitSphere * driftForce;
    }

    void Update()
    {
        // Вращение объекта вокруг своей оси (необязательно)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Рассчитываем направление отталкивания
        Vector3 repulsionDirection = transform.position - collision.contacts[0].point;
        repulsionDirection.Normalize();

        if(rb != null)
            rb.AddForce(repulsionDirection * repulsionForce, ForceMode.Impulse);
    }
}
