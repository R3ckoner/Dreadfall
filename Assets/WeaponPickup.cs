using UnityEngine;
using System.Collections.Generic;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Material outlineMaterial;

    private bool canPickUp = false;
    private Material originalMaterial;
    private TypewriterEffect typewriterEffect;

    // Specify the tags you want to enable on pickup
    public List<string> tagsToEnable = new List<string>();

    private List<GameObject> itemsWithTag = new List<GameObject>();
    private List<GameObject> disabledItems = new List<GameObject>();

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        typewriterEffect = FindObjectOfType<TypewriterEffect>();

        if (typewriterEffect == null)
        {
            Debug.LogError("TypewriterEffect script not found in the scene.");
        }

        // Find and disable all objects tagged with "Item" at the start.
        DisableItemsWithTags();

        // Add them to the list of disabled items.
        foreach (string tag in tagsToEnable)
        {
            GameObject[] allObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in allObjectsWithTag)
            {
                if (!disabledItems.Contains(obj))
                {
                    disabledItems.Add(obj);
                }
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
        EnableItemsWithTags(); // Enable objects with the specified tags.
        Debug.Log("Item pickup collected and tagged items enabled.");

        // Disable the pickup object renderer
        gameObject.SetActive(false);
    }

    private void DisableItemsWithTags()
    {
        foreach (string tag in tagsToEnable)
        {
            GameObject[] allObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in allObjectsWithTag)
            {
                if (obj != gameObject && !IsChildOfCamera(obj))
                {
                    obj.SetActive(false); // Disable the object.
                }
            }
        }
    }

    private void EnableItemsWithTags()
    {
        foreach (GameObject item in disabledItems)
        {
            if (IsChildOfCamera(item))
            {
                item.SetActive(true); // Enable the object.
            }
        }
    }

    private bool IsChildOfCamera(GameObject obj)
    {
        Transform parent = obj.transform.parent;

        while (parent != null)
        {
            if (parent == Camera.main.transform)
            {
                return true;
            }

            parent = parent.parent;
        }

        return false;
    }
}
