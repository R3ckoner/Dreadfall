/*using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public float pickupRange = 2f;
    public Material outlineMaterial;
    public string playerTag = "Player"; // Set the tag of your player clones.
    public List<GameObject> itemsInventory = new List<GameObject>(); // List to store picked up items.

    private bool canPickUp = false;
    private Material originalMaterial;
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        typewriterEffect = FindObjectOfType<TypewriterEffect>();

        // OutlineMaterial reference should be assigned manually in the Inspector.
        if (outlineMaterial == null)
        {
            Debug.LogError("OutlineMaterial reference not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
            if (typewriterEffect != null)
            {
                typewriterEffect.StartTypingEffect("Picked up item");
            }
        }

        if (outlineMaterial != null)
        {
            GetComponent<Renderer>().material = canPickUp ? outlineMaterial : originalMaterial;
        }
    }

    public void PickUp()
    {
        EnableAllItemsWithTag("Item");

        // Add the item to the inventory
        itemsInventory.Add(gameObject);

        // Disable the pickup object renderer
        gameObject.SetActive(false);
    }

    private void EnableAllItemsWithTag(string tag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject item in items)
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

*/