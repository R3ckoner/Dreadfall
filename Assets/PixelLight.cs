using UnityEngine;

public class PixelLight : MonoBehaviour
{
    public Light pixelatedLight;
    public int pixelationFactor = 8;

    void Start()
    {
        if (pixelatedLight == null)
        {
            pixelatedLight = GetComponent<Light>();
        }

        if (pixelatedLight.type != LightType.Point && pixelatedLight.type != LightType.Spot)
        {
            Debug.LogWarning("Pixelation effect is only applicable to Point and Spot lights.");
        }
    }

    void Update()
    {
        if (pixelatedLight != null)
        {
            // Adjust shadow resolution to achieve pixelation effect
            pixelatedLight.shadowCustomResolution = pixelationFactor;
        }
    }
}
