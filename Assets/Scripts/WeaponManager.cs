using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons; // List of available weapons
    private int currentWeaponIndex; // Index of the current weapon
    private Weapon currentWeapon; // Reference to the current weapon instance

    private void Start()
    {
        if (weapons.Count > 0)
            EquipWeapon(0); // Equip the first weapon in the list on start
    }

    private void Update()
    {
        // Handle weapon switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2)
            EquipWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count >= 3)
            EquipWeapon(2);

        // Handle firing
        if (currentWeapon != null && Input.GetMouseButtonDown(0))
            currentWeapon.Fire();
    }

    private void EquipWeapon(int index)
    {
        // Disable the current weapon if it exists
        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        // Update the current weapon index and reference
        currentWeaponIndex = index;
        currentWeapon = weapons[currentWeaponIndex];

        // Enable the newly equipped weapon
        currentWeapon.gameObject.SetActive(true);
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if (!weapons.Contains(weapon))
        {
            // Add the weapon to the weapons list
            weapons.Add(weapon);

            // Set the picked up weapon as the current weapon
            EquipWeapon(weapons.Count - 1);

            Debug.Log("Picked up weapon: " + weapon.weaponName);
        }
    }
}
