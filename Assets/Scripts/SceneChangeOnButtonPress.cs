using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnButtonPress : MonoBehaviour
{
    // Attach an AudioSource component to the GameObject with this script
    public AudioSource buttonClickSound;

    // Function to be called when the button is clicked
    public void NextScene()
    {
        // Check if an AudioSource is attached
        if (buttonClickSound != null)
        {
            // Play the sound
            buttonClickSound.Play();
        }

        // Load the next scene
        SceneManager.LoadScene("Desert");
    }
}
