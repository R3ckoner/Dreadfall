using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    private Light spotlight;
    private bool isLightOn = true; // Initial state: light is on

    private void Start()
    {
        // Find the child Spotlight component
        spotlight = GetComponentInChildren<Light>();

        // Make sure the spotlight is initially enabled
        spotlight.enabled = isLightOn;
    }

    private void Update()
    {
        // Check for mouse click (left mouse button)
        //change the code below to use f instead of mouse click {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the light state
            isLightOn = !isLightOn;

            // Enable or disable the spotlight accordingly
            spotlight.enabled = isLightOn;
        }
}
}