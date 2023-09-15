using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public Material outlineMaterial;
    public string sceneToLoad;
    public float interactionDistance = 2f; // Max distance for interaction

    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure you have a camera tagged as 'MainCamera' in your scene.");
            return;
        }

        // Cast a ray from the main camera's position forward
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == transform)
            {
                GetComponent<Renderer>().material = outlineMaterial;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Load the specified scene
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
            else
            {
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
