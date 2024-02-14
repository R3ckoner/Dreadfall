/* using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    public int damageAmount = 10; // Adjust the damage amount as needed.

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assumes the player has the "Player" tag.
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Set the destination of the NavMeshAgent to follow the player's position.
        navMeshAgent.SetDestination(player.position);

        // Rotate the enemy to always face the player
        Vector3 lookAtDirection = player.position - transform.position;
        lookAtDirection.y = 0; // Ensure the enemy doesn't tilt up or down
        if (lookAtDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookAtDirection);
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the collided object has the "Player" tag
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Damage the player's health
                playerHealth.TakeDamage(damageAmount);
            }
        }
    } */
//}

