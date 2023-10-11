using UnityEngine;

[System.Serializable]
public class PaletteCollection
{
    [Tooltip("Place the palette Materials you want (by default GameBoyShader)")]
    public Material[] palettes;
}

public class GameBoyEffect : MonoBehaviour
{
    public PaletteCollection paletteCollection;

    [Tooltip("The bigger downSampleSize --> the more pixelated (by default = 2)")]
    public int initialDownsampleSize = 2;

    private int downsampleSize;
    private int currentPaletteIndex = 0;

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
    if (Input.GetKeyDown(KeyCode.Comma)) // Keycode for "<" key
    {
        SwitchPalette(-1);  // Switch to the previous material
    }
    else if (Input.GetKeyDown(KeyCode.Period)) // Keycode for ">" key
    {
        SwitchPalette(1);   // Switch to the next material
    }

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

    private void SwitchPalette(int direction)
    {
        if (paletteCollection.palettes == null || paletteCollection.palettes.Length == 0)
        {
            Debug.LogError("You must assign palette Materials to GameBoy Effect Script");
            return;
        }

        currentPaletteIndex = (currentPaletteIndex + direction + paletteCollection.palettes.Length) % paletteCollection.palettes.Length;
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
        if (paletteCollection.palettes == null || paletteCollection.palettes.Length == 0)
        {
            Debug.LogError("You must assign palette Materials to GameBoy Effect Script");
            return;
        }

        int width = source.width / downsampleSize;
        int height = source.height / downsampleSize;

        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0, source.format);
        temp.filterMode = FilterMode.Point;

        Graphics.Blit(source, temp);

        Graphics.Blit(temp, destination, paletteCollection.palettes[currentPaletteIndex]);

        RenderTexture.ReleaseTemporary(temp);
    }
}
