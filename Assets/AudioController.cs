using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public float maxDistance = 10f; // Maximum distance at which audio is heard at full volume

    void Update()
    {
        if (player == null || audioSource == null)
        {
            Debug.LogError("Player or AudioSource not assigned.");
            return;
        }

        // Calculate the distance between the player and the audio source
        float distance = Vector3.Distance(player.position, transform.position);

        // Calculate the volume based on the distance
        float volume = 1f - Mathf.Clamp01(distance / maxDistance);

        // Set the volume of the audio source
        audioSource.volume = volume;
    }
}
