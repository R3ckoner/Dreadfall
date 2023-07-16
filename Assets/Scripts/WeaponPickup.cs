using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon; // Reference to the weapon script of the pickup

    public void PickUp()
    {
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.PickUpWeapon(weapon);
            gameObject.SetActive(false); // Disable the pickup object renderer
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
