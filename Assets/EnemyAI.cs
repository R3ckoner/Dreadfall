using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float health = 100f;
    public float timeToDestroy = 2f;
    public float followSpeed = 3f;

    private bool isDead = false;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDead)
        {
            FollowPlayer();
        }

        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 targetPosition = transform.position + direction * followSpeed * Time.deltaTime;
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            transform.LookAt(player);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        // Add any death animations, sound effects, or other logic here

        // Disable further interactions and wait for some time before destroying
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }
}
