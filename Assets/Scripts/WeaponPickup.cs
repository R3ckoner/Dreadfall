using UnityEngine;
using System.Collections.Generic;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Material outlineMaterial;

    private bool canPickUp = false;
    private Material originalMaterial;
    private TypewriterEffect typewriterEffect;

    // Specify the GameObjects you want to enable on pickup
    public List<GameObject> objectsToEnable = new List<GameObject>();

    private List<GameObject> disabledObjects = new List<GameObject>();

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        typewriterEffect = FindObjectOfType<TypewriterEffect>();

        if (typewriterEffect == null)
        {
            Debug.LogError("TypewriterEffect script not found in the scene.");
        }

        // Add the associated disabled objects to the list
        foreach (GameObject obj in objectsToEnable)
        {
            if (!disabledObjects.Contains(obj))
            {
                disabledObjects.Add(obj);
                obj.SetActive(false); // Disable the associated objects initially
            }
        }
    }

    void Update()
    {
        // Check if the player is looking at the object and presses "E"
        if (CanPickUp() && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (outlineMaterial != null)
        {
            GetComponent<Renderer>().material = canPickUp ? outlineMaterial : originalMaterial;
        }
    }

    private bool CanPickUp()
    {
        Camera mainCamera = Camera.main; // Get the main camera

        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.distance <= pickupRange)
            {
                return hit.collider.gameObject == gameObject;
            }
        }

        return false;
    }

    public void PickUp()
    {
        EnableObjectsOnPickup(); // Enable associated objects on pickup
        Debug.Log("Objects pickup collected and associated objects enabled.");

        // Optionally play a pickup sound, effects, etc.

        // Disable the pickup object renderer
        gameObject.SetActive(false);

        // Destroy the pickup object if needed
        Destroy(gameObject);
    }

    private void EnableObjectsOnPickup()
    {
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(true); // Enable the associated object.
        }
    }
}
