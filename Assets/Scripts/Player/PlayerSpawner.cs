using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform spawnPoint; // Reference to the spawn point transform

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            // Instantiate the player prefab at the spawn point's position and rotation
            GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // You can add any other player initialization logic here if needed
        }
        else
        {
            Debug.LogError("Player prefab or spawn point is not assigned in the Inspector.");
        }
    }
}
