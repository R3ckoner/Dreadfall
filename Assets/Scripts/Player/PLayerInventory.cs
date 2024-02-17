using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public KeyCode[] gunSwitchKeys; // Assign keys for switching guns
    public KeyCode pickupKey = KeyCode.E;

    private Transform[] guns;
    private int currentGunIndex = 0;

    void Start()
    {
        // Get all child guns of the camera
        guns = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            guns[i] = transform.GetChild(i);
        }

        SwitchGun(currentGunIndex);
    }

    void Update()
    {
        // Check for gun switching input
        for (int i = 0; i < gunSwitchKeys.Length; i++)
        {
            if (Input.GetKeyDown(gunSwitchKeys[i]))
            {
                SwitchGun(i);
            }
        }

        // Check for picking up a gun
        if (Input.GetKeyDown(pickupKey))
        {
            TryPickupGun();
        }
    }

    void SwitchGun(int index)
    {
        // Deactivate the current gun
        if (currentGunIndex >= 0 && currentGunIndex < guns.Length)
        {
            guns[currentGunIndex].gameObject.SetActive(false);
        }

        // Activate the new gun
        currentGunIndex = index;
        if (currentGunIndex >= 0 && currentGunIndex < guns.Length)
        {
            guns[currentGunIndex].gameObject.SetActive(true);
        }
    }

    void TryPickupGun()
    {
        // Check if there's a gun in front of the player to pick up
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            Transform gunToPickup = hit.collider.transform;

            // Check if the picked-up gun is a child of the camera
            for (int i = 0; i < guns.Length; i++)
            {
                if (gunToPickup == guns[i])
                {
                    SwitchGun(i);
                    break;
                }
            }
        }
    }
}
