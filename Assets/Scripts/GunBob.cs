using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBob : MonoBehaviour
{
    public float bobSpeed = 1f;          // Speed of the gun bobbing
    public float bobAmount = 0.1f;       // Amount of gun bobbing
    public float moveThreshold = 0.1f;   // Movement input threshold for bobbing activation
    public float returnSpeed = 2f;       // Speed of returning to the original position

    private float originalY;             // Original Y position of the gun
    private float targetY;               // Target Y position of the gun
    private float timer;                 // Timer for the bobbing motion
    private CharacterController controller;  // Reference to the CharacterController component
    private bool isMoving;               // Flag indicating if the player is currently moving

    void Start()
    {
        originalY = transform.localPosition.y;
        targetY = originalY;
        controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        // Check if the player's movement input exceeds the threshold
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        isMoving = new Vector2(moveHorizontal, moveVertical).sqrMagnitude > moveThreshold * moveThreshold;

        // Calculate the vertical position offset based on time and movement speed
        float yOffset = isMoving ? Mathf.Sin(timer) * bobAmount : 0f;

        // Smoothly interpolate the gun's Y position towards the target position
        targetY = isMoving ? originalY + yOffset : originalY;
        float newY = Mathf.Lerp(transform.localPosition.y, targetY, returnSpeed * Time.deltaTime);

        // Apply the interpolated Y position to the gun's local position
        Vector3 newPosition = transform.localPosition;
        newPosition.y = newY;
        transform.localPosition = newPosition;

        // Increment the timer based on time and bobbing speed only when moving
        if (isMoving)
        {
            timer += bobSpeed * Time.deltaTime;

            // Wrap the timer within the 2*pi range to prevent overflow
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }
    }
}
