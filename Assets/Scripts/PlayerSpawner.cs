using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Load the player's inventory from the GameManager.
            playerController.LoadInventory(GameManager.instance.playerInventory);

            // Set the current weapon index directly.
            playerController.currentWeaponIndex = GameManager.instance.currentWeaponIndex;
        }
        else
        {
            Debug.LogWarning("PlayerController component not found on the player prefab.");
        }
    }
}
