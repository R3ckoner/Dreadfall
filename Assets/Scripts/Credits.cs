using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsPanel;

    void Start()
    {
        // Disable the credits panel initially
        creditsPanel.SetActive(false);
    }

    void Update()
    {
        // Check for the "Escape" key to close the credits panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCredits();
        }
    }

    public void ToggleCredits()
    {
        // Toggle the visibility of the credits panel
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void CloseCredits()
    {
        // Close the credits panel
        creditsPanel.SetActive(false);
    }
}
