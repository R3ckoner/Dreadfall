using UnityEngine;

public class SmoothMouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 3;

    void Start()
    {
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

        rotation.x = Mathf.Clamp(rotation.x, -45f, 45f);

        transform.eulerAngles = (Vector2)rotation * speed;

        Cursor.lockState = CursorLockMode.Locked;
    }
}
