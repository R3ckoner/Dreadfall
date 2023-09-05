using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Reference to your TextMeshPro Text element

    private float updateInterval = 1f; // Update every 0.5 seconds
    private float lastUpdateTime = 0f;
    private int frameCount = 0;

    void Update()
    {
        frameCount++;
        float currentTime = Time.realtimeSinceStartup;

        if (currentTime - lastUpdateTime >= updateInterval)
        {
            float deltaTime = currentTime - lastUpdateTime;
            float fps = frameCount / deltaTime;

            fpsText.text = "fps: " + Mathf.Ceil(fps).ToString();

            frameCount = 0;
            lastUpdateTime = currentTime;
        }
    }
}
