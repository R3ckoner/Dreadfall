using System.Collections.Generic; // To use Dictionary<>
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Define a dictionary to store weapons by their names
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    void Start()
    {
        // Fill the weapons dictionary with weapons found in the scene
        Weapon[] foundWeapons = FindObjectsOfType<Weapon>();
        foreach (Weapon weapon in foundWeapons)
        {
            // Ensure each weapon has a unique name
            if (!weapons.ContainsKey(weapon.weaponName))
            {
                weapons.Add(weapon.weaponName, weapon);
            }
            else
            {
                Debug.LogWarning("Duplicate weapon name found: " + weapon.weaponName);
            }
        }
    }

    public Dictionary<string, Weapon> GetWeapons()
    {
     return weapons;
    }   

    public Weapon GetWeaponByName(string weaponName)
    {
        if (weapons.ContainsKey(weaponName))
        {
            return weapons[weaponName];
        }
        else
        {
            Debug.LogWarning("Weapon not found with name: " + weaponName);
            return null;
        }
    }



    public void AddWeapon(Weapon weapon)
    {
        if (!weapons.ContainsKey(weapon.weaponName))
        {
            weapons.Add(weapon.weaponName, weapon);
        }
        else
        {
            Debug.LogWarning("Weapon already exists in the weapon manager: " + weapon.weaponName);
        }
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (weapons.ContainsKey(weapon.weaponName))
        {
            weapons.Remove(weapon.weaponName);
        }
        else
        {
            Debug.LogWarning("Weapon not found in the weapon manager: " + weapon.weaponName);
        }
    }

    // Optional: You can add more methods or functionality as needed
}
