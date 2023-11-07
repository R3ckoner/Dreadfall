using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public string tagToCollideWith = "Fear";
    public int damageAmount = 10;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag(tagToCollideWith))
    {
        Debug.Log("Collision with enemy detected.");
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("PlayerHealth.TakeDamage called.");
        }
    }
}

}
