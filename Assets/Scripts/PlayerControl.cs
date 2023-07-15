using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float tiltSpeed = 5f;
    public float maxTiltAngle = 20f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private Quaternion initialRotation;
    private float targetTiltAngle = 0f;
    private float currentTiltAngle = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        initialRotation = playerCamera.transform.localRotation;

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        movement.y = 0f; // Disable vertical movement

        characterController.Move(movement * movementSpeed * Time.deltaTime);

        // Handle player rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Handle camera tilt
        float tiltInput = Input.GetAxis("Horizontal");
        targetTiltAngle = -tiltInput * maxTiltAngle;

        currentTiltAngle = Mathf.Lerp(currentTiltAngle, targetTiltAngle, tiltSpeed * Time.deltaTime);

        Quaternion tiltRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, currentTiltAngle);
        playerCamera.transform.localRotation = tiltRotation * playerCamera.transform.localRotation;

        // Jumping
        if (isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f; // Ensures character is grounded after falling
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpForce;
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Check if the player is grounded
        isGrounded = characterController.isGrounded;
    }
}
