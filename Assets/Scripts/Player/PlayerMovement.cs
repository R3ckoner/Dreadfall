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

    public AudioSource footstepAudioSource;
    public AudioClip[] footstepSounds;
    public float footstepInterval = 0.5f;

    private float footstepTimer;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset falling velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Use the local right and forward directions of the player
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the player with the calculated velocity
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
