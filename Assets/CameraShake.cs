using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 1.0f; // The duration of the shake effect.
    public float shakeIntensity = 0.1f; // The intensity of the shake effect.
    public float shakeSpeed = 5.0f; // The speed at which the camera shakes.

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private float shakeTimer = 0.0f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Generate Perlin noise values for smooth camera shake.
            float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * 2 - 1;
            float offsetY = Mathf.PerlinNoise(0, Time.time * shakeSpeed) * 2 - 1;

            // Apply the camera shake to the camera's local position.
            Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0) * shakeIntensity;
            cameraTransform.localPosition = originalPosition + shakeOffset;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera's position when the shake duration is over.
            shakeTimer = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void StartShake()
    {
        shakeTimer = shakeDuration;
    }
}
