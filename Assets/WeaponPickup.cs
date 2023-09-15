using UnityEngine;
using System.Collections.Generic;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Material outlineMaterial;

    private bool canPickUp = false;
    private Material originalMaterial;
    private TypewriterEffect typewriterEffect;

    private List<GameObject> itemsWithTag = new List<GameObject>();

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        typewriterEffect = FindObjectOfType<TypewriterEffect>();

        if (typewriterEffect == null)
        {
            Debug.LogError("TypewriterEffect script not found in the scene.");
        }

        // Collect all objects with the "Item" tag and add them to the list.
        CollectItemsWithTag("Item");
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (outlineMaterial != null)
        {
            GetComponent<Renderer>().material = canPickUp ? outlineMaterial : originalMaterial;
        }
    }

    public void PickUp()
    {
        // Check if the pickup itself is tagged "weaponPickup"
        if (gameObject.CompareTag("weaponPickup"))
        {
            // Enable all objects with the "Item" tag
            EnableAllItemsWithTag();
            Debug.Log("Item pickup collected and tagged items enabled.");
        }

        // Disable the pickup object renderer
        gameObject.SetActive(false);
    }

    private void CollectItemsWithTag(string tag)
    {
        // Collect all objects with the specified tag and add them to the list.
        GameObject[] allObjectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in allObjectsWithTag)
        {
            if (!itemsWithTag.Contains(obj))
            {
                itemsWithTag.Add(obj);

                // Deactivate them initially.
                obj.SetActive(false);
            }
        }
    }

    private void EnableAllItemsWithTag()
    {
        // Enable all objects in the list.
        foreach (GameObject item in itemsWithTag)
        {
            item.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Camera mainCamera = Camera.main; // Get the main camera

        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
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
            Debug.LogError("Main camera not found in the scene.");
        }
    }
}
