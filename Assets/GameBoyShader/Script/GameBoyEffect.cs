using UnityEngine;

public class GameBoyEffect : MonoBehaviour
{
    [Tooltip("Place the palette Material you want (by default GameBoyShader)")]
    public Material palette;

    [Tooltip("The bigger downSampleSize --> the more pixelated (by default = 2)")]
    public int initialDownsampleSize = 2;

    private int downsampleSize;

    private void Start()
    {
        downsampleSize = initialDownsampleSize;
    }

    private void Update()
    {
        CheckKeyPress();
    }

    private void CheckKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetDownsampleSize(2);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SetDownsampleSize(5);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SetDownsampleSize(10);
        }
    }

    private void SetDownsampleSize(int newSize)
    {
        downsampleSize = newSize;
    }

    private (int, int) Downsample(RenderTexture source, int downsampleSize)
    {
        int width = source.width / downsampleSize;
        int height = source.height / downsampleSize;
        return (width, height);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (palette == null)
        {
            Debug.LogError("You must assign a palette Material to GameBoy Effect Script");
        }

        int width = source.width / downsampleSize;
        int height = source.height / downsampleSize;

        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0, source.format);

        // Avoid interpolate (thanks for the tip: /xhero)
        temp.filterMode = FilterMode.Point;

        // Obtain a smaller version of the source input
        Graphics.Blit(source, temp);
        Graphics.Blit(temp, destination, palette);
        RenderTexture.ReleaseTemporary(temp);
    }
}
