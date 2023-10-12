using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 1.0f;

    private float randomFlicker;

    void Start()
    {
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }

        // Set an initial random flicker value
        randomFlicker = Random.Range(0f, 1f);
    }

    void Update()
    {
        // Flicker the light intensity based on a random value
        float flicker = Mathf.Lerp(minIntensity, maxIntensity, randomFlicker);
        flickeringLight.intensity = flicker;

        // Update the random flicker value over time
        randomFlicker += Time.deltaTime * flickerSpeed;

        // If randomFlicker exceeds 1, generate a new random flicker value
        if (randomFlicker > 1f)
        {
            randomFlicker = Random.Range(0f, 1f);
        }
    }
}
