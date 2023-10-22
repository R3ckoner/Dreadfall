using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public string taggedObjectTag = "Fear";
    public float maxDistance = 10.0f; // Adjust as needed.

    private CameraShake cameraShake;
    private Transform player;

    private void Start()
    {
        player = Camera.main.transform; // Assuming the camera is attached to the player.
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        // Check the distance to the closest tagged object.
        GameObject closestTaggedObject = FindClosestTaggedObject();
        if (closestTaggedObject != null)
        {
            float distance = Vector3.Distance(player.position, closestTaggedObject.transform.position);

            // Check if the player is within the specified range of the tagged object.
            if (distance <= maxDistance)
            {
                // Trigger the camera shake effect when the player is within range.
                cameraShake.StartShake();
            }
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
}
