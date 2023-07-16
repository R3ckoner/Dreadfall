using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpWeapon();
        }
    }

    private void TryPickUpWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            WeaponPickup weaponPickup = hit.collider.GetComponent<WeaponPickup>();
            if (weaponPickup != null)
            {
                weaponPickup.PickUp();
            }
        }
    }
}
