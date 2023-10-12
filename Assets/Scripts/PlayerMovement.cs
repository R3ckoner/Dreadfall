using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource footstepAudioSource; // Reference to the AudioSource component for footstep sounds
    public AudioClip[] footstepSounds; // Array of footstep sound clips
    public float footstepInterval = 0.5f; // Time interval between footstep sounds

    private float footstepTimer;

    Vector3 velocity;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // checking if we hit the ground to reset our falling velocity, otherwise, we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // right is the red Axis, forward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Footstep sound logic
        if (isGrounded && (x != 0f || z != 0f))
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                PlayFootstepSound();
                footstepTimer = 0f;
            }
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0 && footstepAudioSource != null)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            footstepAudioSource.clip = footstepSounds[randomIndex];
            footstepAudioSource.Play();
        }
    }
}
