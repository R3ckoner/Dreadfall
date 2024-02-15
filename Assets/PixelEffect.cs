using UnityEngine;

public class PixelEffect : MonoBehaviour
{
    public int pixelSize = 10; // Adjust as needed.
    public string taggedObjectTag = "TaggedObject";
    public float maxDistance = 10.0f; // Adjust as needed.

    private Transform player;
    private Camera mainCamera;

    private void Start()
    {
        player = Camera.main.transform; // Assuming the camera is attached to the player.
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GameObject closestTaggedObject = FindClosestTaggedObject();
        if (closestTaggedObject != null)
        {
            float distance = Vector3.Distance(player.position, closestTaggedObject.transform.position);
            pixelSize = Mathf.RoundToInt(Mathf.Lerp(1, 10, distance / maxDistance));
        }
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
        int width = mainCamera.pixelWidth / pixelSize;
        int height = mainCamera.pixelHeight / pixelSize;

        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0, source.format);
        temp.filterMode = FilterMode.Point;

        Graphics.Blit(source, temp);
        Graphics.Blit(temp, destination);

        RenderTexture.ReleaseTemporary(temp);
    }
}
