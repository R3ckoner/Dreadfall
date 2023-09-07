using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon; // Reference to the weapon script of the pickup
    public Camera playerCamera; // Reference to the player's camera
    public float pickupRange = 2f; // The maximum distance at which the player can pick up the item
    public Material outlineMaterial; // Reference to the outline material
    private bool canPickUp = false; // Flag to check if the player can pick up the weapon
    private Material originalMaterial; // Store the original material of the pickup object
    private TypewriterEffect typewriterEffect; // Reference to the TypewriterEffect script

    private void Start()
    {
        // Store the original material of the pickup object
        originalMaterial = GetComponent<Renderer>().material;

        // Find the TypewriterEffect script in the scene
        typewriterEffect = FindObjectOfType<TypewriterEffect>();
        if (typewriterEffect == null)
        {
            Debug.LogError("TypewriterEffect script not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is looking at the pickup and is within pickupRange, and presses 'E'
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();

            // Queue up a pickup message
            if (typewriterEffect != null)
            {
                typewriterEffect.StartTypingEffect("Picked up " + weapon.weaponName);
            }
        }

        // Control the outline effect
        if (outlineMaterial != null)
        {
            if (canPickUp)
            {
                GetComponent<Renderer>().material = outlineMaterial;
            }
            else
            {
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }

    public void PickUp()
    {
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.PickUpWeapon(weapon);
            gameObject.SetActive(false); // Disable the pickup object renderer
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is looking at the pickup object using a raycast
        if (playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.distance <= pickupRange)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    canPickUp = true; // The player is looking at the pickup object and is within pickupRange
                }
                else
                {
                    canPickUp = false; // The player is not looking at the pickup object or is out of pickupRange
                }
            }
            else
            {
                canPickUp = false; // The raycast did not hit anything, or the hit object is out of pickupRange
            }
        }
        else
        {
            Debug.LogError("Player camera reference is not set in the Inspector.");
        }
    }
}
