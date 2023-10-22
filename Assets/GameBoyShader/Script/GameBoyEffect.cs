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
    public string taggedObjectTag = "TaggedObject";
    public float maxDistance = 10.0f; // Adjust as needed.
    public int minDownsampleSize = 1;
    public int maxDownsampleSize = 10; // Adjust as needed.

    private int currentPaletteIndex = 0;
    private Transform player;
    private Camera mainCamera;
    private int downsampleSize = 2; // Initial downsample size

    private void Start()
    {
        player = Camera.main.transform; // Assuming the camera is attached to the player.
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckKeyPress();

        // Check the distance to the closest tagged object.
        GameObject closestTaggedObject = FindClosestTaggedObject();
        if (closestTaggedObject != null)
        {
            float distance = Vector3.Distance(player.position, closestTaggedObject.transform.position);

            // Map distance to downsample size within the specified range, but reverse it to make the resolution drop.
            downsampleSize = Mathf.Clamp(Mathf.RoundToInt(Mathf.Lerp(maxDownsampleSize, minDownsampleSize, distance / maxDistance)), minDownsampleSize, maxDownsampleSize);
        }
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

    private GameObject FindClosestTaggedObject()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(taggedObjectTag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in taggedObjects)
        {
            float distance = Vector3.Distance(player.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (paletteCollection.palettes == null || paletteCollection.palettes.Length == 0)
        {
            Debug.LogError("You must assign palette Materials to GameBoy Effect Script");
            return;
        }

        int width = mainCamera.pixelWidth / downsampleSize;
        int height = mainCamera.pixelHeight / downsampleSize;

        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0, source.format);
        temp.filterMode = FilterMode.Point;

        Graphics.Blit(source, temp);

        Graphics.Blit(temp, destination, paletteCollection.palettes[currentPaletteIndex]);

        RenderTexture.ReleaseTemporary(temp);
    }
}
