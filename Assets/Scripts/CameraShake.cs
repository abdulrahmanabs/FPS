using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeAmount = 0.2f;
    [SerializeField] private float decreaseFactor = 1.0f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
            transform.localPosition = originalPosition + randomOffset;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        currentShakeDuration = shakeDuration;
    }
}