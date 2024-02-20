using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public float pickupRange = 2f;
    public List<string> pickupTags = new List<string> { "weaponPickup1", "weaponPickup2" }; // Tags for the pickup objects
    public List<string> weaponTags = new List<string> { "weapon1", "weapon2" }; // Tags for the weapons

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
            if (pickupTags.Contains(pickupObject.tag)) // Check if the tag is in the list
            {
                // Enable the weapon which is a child of the camera
                foreach (Transform child in playerCamera.transform)
                {
                    if (weaponTags.Contains(child.gameObject.tag))
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
