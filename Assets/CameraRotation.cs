using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Adjust this speed value to control the rotation speed
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera around the Y-axis
        RotateCamera();
    }

    void RotateCamera()
    {
        // Calculate the rotation amount based on the speed and time scale
        float rotationAmount = 1f * rotationSpeed * Time.timeScale;

        // Apply the rotation to the camera
        transform.Rotate(0, rotationAmount, 0);
    }
}
