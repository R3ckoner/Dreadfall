using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float pickupRange = 2f;
    public string pickupTag = "weaponPickup"; // Tag for the pickup objects
    public string weaponTag = "weapon"; // Tag for the weapon

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera == null)
        {
            Debug.LogError("Player camera reference is not set in the Inspector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            GameObject pickupObject = hit.collider.gameObject; // Get the pickup object
            if (pickupObject.CompareTag(pickupTag)) // Check the tag
            {
                // Enable the weapon which is a child of the camera
                foreach (Transform child in playerCamera.transform)
                {
                    if (child.gameObject.CompareTag(weaponTag))
                    {
                        child.gameObject.SetActive(true);
                        break;
                    }
                }

                // Make the pickup object disappear
                Destroy(pickupObject);
            }
        }
    }
}
