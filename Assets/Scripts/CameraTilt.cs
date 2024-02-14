using UnityEngine;

public class FPTilt : MonoBehaviour
{
    public float tiltAmount = 5f;
    public float tiltSpeed = 5f;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKey(leftButton))
        {
            TiltCamera(tiltAmount);
        }
        else if (Input.GetKey(rightButton))
        {
            TiltCamera(-tiltAmount);
        }
        else
        {
            // Smoothly return to the initial rotation when no keys are pressed
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * tiltSpeed);
        }
    }

    void TiltCamera(float amount)
    {
        // Calculate the target rotation based on the tilt amount
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, amount);

        // Smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}
