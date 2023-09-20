using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableObject : MonoBehaviour
{

    public bool playerInRange;
    public string ItemName;
 
    public string GetItemName()
    {
        return ItemName;
    }

void Update()
{
    if (playerInRange && Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject)
    {
        if (InventorySystem.Instance.CheckIfFull()) // Add parentheses to call the method
        {
            InventorySystem.Instance.AddToInventory(ItemName); // Access the property without parentheses
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }
}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }

        
    }


}