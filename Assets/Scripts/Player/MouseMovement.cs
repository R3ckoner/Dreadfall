using UnityEngine;

public class SmoothMouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 3;

    void Start()
    {
        // Lock the cursor at the start of the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
    }

void HandleMouseLook()
{
    rotation.y += Input.GetAxis("Mouse X");
    rotation.x += -Input.GetAxis("Mouse Y");
    
    // Clamp the vertical rotation between -45 and 45 degrees
    rotation.x = Mathf.Clamp(rotation.x, -45f, 45f);
    
    // Apply the rotation to the GameObject
    transform.eulerAngles = (Vector2)rotation * speed;

    // Lock the cursor during gameplay
    Cursor.lockState = CursorLockMode.Locked;
}

}
