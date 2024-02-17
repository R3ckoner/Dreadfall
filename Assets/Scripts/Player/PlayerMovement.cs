using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public AudioSource footstepAudioSource;
    public AudioClip[] footstepSounds;
    public float footstepInterval = 0.5f;

    private float footstepTimer;
    private Vector3 velocity;

    void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
        HandleFootsteps();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleFootsteps()
    {
        if (IsMoving() && IsGrounded())
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                PlayFootstepSound();
                footstepTimer = 0f;
            }
        }
    }

    bool IsMoving()
    {
        return Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f;
    }

    bool IsGrounded()
    {
        return controller.isGrounded;
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
