using UnityEngine;

public class RetroPixelizer : MonoBehaviour
{
    public int retroWidth = 1920; // Retro resolution width in pixels
    public int retroHeight = 1080; // Retro resolution height in pixels
    public bool useNearestNeighborFiltering = true; // Use nearest neighbor filtering for pixelation

    private Camera mainCamera;
    private RenderTexture pixelatedRT;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        InitializePixelation();
    }

    private void InitializePixelation()
    {
        // Create a render texture for pixelation with the specified retro resolution
        pixelatedRT = new RenderTexture(retroWidth, retroHeight, 24);
        pixelatedRT.filterMode = useNearestNeighborFiltering ? FilterMode.Point : FilterMode.Bilinear;

        // Set the camera's target texture to render at the new resolution
        mainCamera.targetTexture = pixelatedRT;
    }

    private void OnPreRender()
    {
        InitializePixelation();
    }

    private void OnPostRender()
    {
        // Reset the camera's target texture to null
        mainCamera.targetTexture = null;
    }
}
