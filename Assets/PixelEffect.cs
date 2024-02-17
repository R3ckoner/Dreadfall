using UnityEngine;

public class PixelEffect : MonoBehaviour
{
    public int pixelSize = 10; // Adjust as needed.

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // You can add any logic here if needed.
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = mainCamera.pixelWidth / pixelSize;
        int height = mainCamera.pixelHeight / pixelSize;

        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0, source.format);
        temp.filterMode = FilterMode.Point;

        Graphics.Blit(source, temp);
        Graphics.Blit(temp, destination);

        RenderTexture.ReleaseTemporary(temp);
    }
}
