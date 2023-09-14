using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponObject;
    public Camera playerCamera;
    public float pickupRange = 2f;
    public Material outlineMaterial;

    private bool canPickUp = false;
    private Material originalMaterial;
    private TypewriterEffect typewriterEffect;
    private PlayerInventory playerInventory;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        typewriterEffect = FindObjectOfType<TypewriterEffect>();
        playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory script not found in the scene.");
        }
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
            if (typewriterEffect != null)
            {
                typewriterEffect.StartTypingEffect("Picked up " + weaponObject.name);
            }
        }

        if (outlineMaterial != null)
        {
            GetComponent<Renderer>().material = canPickUp ? outlineMaterial : originalMaterial;
        }
    }

public string PickUp()
{
    PlayerController playerController = FindObjectOfType<PlayerController>();
    if (playerController != null)
    {
        // Enable the weapon object
        weaponObject.SetActive(true);

        // Add the weapon to the player's inventory
        playerController.AddToInventory(weaponObject.name, weaponObject.GetComponent<Weapon>());

        // Disable the pickup object renderer
        gameObject.SetActive(false);

        // Return the name of the picked-up weapon
        return weaponObject.name;
    }
    
    // Return an empty string or an error message in case of failure
    return "Failed to pick up the weapon.";
}


    private void FixedUpdate()
    {
        if (playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.distance <= pickupRange)
            {
                canPickUp = (hit.collider.gameObject == gameObject);
            }
            else
            {
                canPickUp = false;
            }
        }
        else
        {
            Debug.LogError("Player camera reference is not set in the Inspector.");
        }
    }
}
