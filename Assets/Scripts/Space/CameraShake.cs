using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // Ссылка на трансформацию камеры
    public bool shake = false;
    public float shakeAmount = 0.1f; // Амплитуда потрясывания
    public float decreaseFactor = 1.0f; // Фактор уменьшения амплитуды потрясывания с течением времени

    private Vector3 originalPosition; // Исходное положение камеры

    private void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        originalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (shake)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        }
        else
        {
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void StartShake(bool isShake)
    {
        shake = isShake;
    }
}
