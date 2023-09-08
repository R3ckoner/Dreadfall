using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public Material outlineMaterial;
    public string sceneToLoad;
    public Camera playerCamera; // Reference to the player camera
    public float interactionDistance = 2f; // Max distance for interaction

    private Material originalMaterial;
    private bool isLookingAtDoor = false;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Cast a ray from the player camera's position forward
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                // The player is looking at the door
                isLookingAtDoor = true;
                GetComponent<Renderer>().material = outlineMaterial;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Load the specified scene
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                // The player is not looking at the door
                isLookingAtDoor = false;
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
        else
        {
            // The player is not looking at the door
            isLookingAtDoor = false;
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
