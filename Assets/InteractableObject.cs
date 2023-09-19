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
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && SelectionManager.Instance.onTarget)
        {
            Debug.Log("Item added to Inventory");

            Destroy(gameObject);
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