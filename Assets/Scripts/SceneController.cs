using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject weaponPrefab; // Reference to the weapon prefab

    private void Start()
    {
        // Instantiate the weapon prefab if it doesn't exist
        if (GameObject.Find(weaponPrefab.name) == null)
        {
            Instantiate(weaponPrefab);
        }
    }
}
