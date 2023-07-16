using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public Transform firePoint;
    public float fireRate = 1f;
    public float maxFireDistance = 100f;

    private float fireTimer;

    private void Update()
    {
        if (fireTimer > 0f)
            fireTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1")) // Check for Fire1 input (e.g., left mouse button)
            Fire();
    }

    public void Fire()
    {
        if (fireTimer <= 0f)
        {
            // Perform a raycast to detect hits
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxFireDistance))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);

                // Apply damage or effects to the hit object here

                // Example: Destroy the hit object
                Destroy(hit.collider.gameObject);
            }

            // Reset the fire timer
            fireTimer = 1f / fireRate;
        }
    }
}
