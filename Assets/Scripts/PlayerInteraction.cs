using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float pickupRange = 2f;
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
            GameObject weaponObject = hit.collider.gameObject; // Get the weapon object
            if (weaponObject.CompareTag("weaponPickup")) // Check the tag
            {
                PlayerController playerController = GetComponent<PlayerController>();
                if (playerController != null)
                {
                    // Call the new method to pick up the weapon
                    playerController.PickUpWeapon(weaponObject);
                }
            }
        }
    }
}
