using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    public float tiltSpeed = 5f;
    public float maxTiltAngle = 20f;

    private float tiltAngle = 0f;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        // Handle camera tilt
        float tiltInput = Input.GetAxis("Horizontal");
        tiltAngle = Mathf.Lerp(tiltAngle, -tiltInput * maxTiltAngle, tiltSpeed * Time.deltaTime);

        Quaternion tiltRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, tiltAngle);
        transform.localRotation = tiltRotation;

        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X");

        // Apply rotation around the Y-axis (left and right)
        transform.Rotate(Vector3.up * mouseX);
    }
}