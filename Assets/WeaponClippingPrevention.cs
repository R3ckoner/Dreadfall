using UnityEngine;

public class WeaponClippingPrevention : MonoBehaviour
{
    public Camera fpsCamera; // Reference to the first-person camera
    public float clippingDistance = 0.01f; // Adjust the distance to check for clipping

    void Update()
    {
        // Cast a ray from the camera in the forward direction
        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits something within the clipping distance
        if (Physics.Raycast(ray, out hit, clippingDistance))
        {
            // Move the weapon to the point where the ray hits to prevent clipping
            transform.position = hit.point - fpsCamera.transform.forward * 0.05f; // You can adjust the offset
        }
    }
}
