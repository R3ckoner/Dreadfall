using UnityEngine;
using TMPro;

public class NowPlaying : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component playing the music
    public TextMeshProUGUI textMeshPro; // The TextMeshPro component displaying the song name
    public float pitchChangeSpeed = 0.1f; // Adjust the speed of pitch change
    public float maxPitch = 1.5f; // Maximum pitch during acceleration
    public float originalPitch; // Variable to store the original pitch

    // An array of AudioClips. You should replace this with your own songs.
    public AudioClip[] songs;
    // An array of song names. You should replace this with your own song names.
    public string[] songNames;

    private int currentSongIndex = 0;
    private float targetPitch = 1.0f; // Default pitch

    void Start()
    {
        if (songs.Length != songNames.Length)
        {
            Debug.LogError("Songs and songNames arrays must be the same length!");
            return;
        }

        originalPitch = audioSource.pitch; // Store the original pitch
        PlaySong(currentSongIndex);
    }

    void Update()
    {
        // Smoothly adjust the pitch based on player speed
        float playerSpeed = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
        targetPitch = Mathf.Lerp(originalPitch, maxPitch, playerSpeed); // Adjust the pitch range as needed

        // Ensure the pitch doesn't exceed the original pitch or maxPitch
        targetPitch = Mathf.Clamp(targetPitch, originalPitch, maxPitch);

        // Apply smooth pitch change
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * pitchChangeSpeed);

        if (!audioSource.isPlaying)
        {
            // Move to the next song
            currentSongIndex = (currentSongIndex + 1) % songs.Length;
            PlaySong(currentSongIndex);
        }

        // Update the TextMeshPro text
        textMeshPro.text = "NOW PLAYING: " + songNames[currentSongIndex];

        // Check for key presses to change song
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            // Move to the next song
            currentSongIndex = (currentSongIndex + 1) % songs.Length;
            PlaySong(currentSongIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            // Move to the previous song
            currentSongIndex--;
            if (currentSongIndex < 0) currentSongIndex = songs.Length - 1;
            PlaySong(currentSongIndex);
        }
    }

    void PlaySong(int index)
    {
        audioSource.clip = songs[index];
        audioSource.Play();
    }
}
