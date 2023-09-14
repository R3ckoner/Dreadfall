using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public bool isEnabled;
    public bool isEquipped;

    public WeaponData(bool enabled, bool equipped)
    {
        isEnabled = enabled;
        isEquipped = equipped;
    }
}

public class PersistentData : MonoBehaviour
{
    private static PersistentData instance;

    // Dictionary to store weapon data by weapon name
    private Dictionary<string, WeaponData> weaponDataDict = new Dictionary<string, WeaponData>();
    private string currentWeaponName;
    private WeaponManager weaponManager; // Add a reference to the WeaponManager

    public static PersistentData Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("PersistentData");
                instance = go.AddComponent<PersistentData>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // Add this method to set the WeaponManager reference
    public void SetWeaponManager(WeaponManager manager)
    {
        weaponManager = manager;
    }

    // Add a method to retrieve the player's weapons
    public Dictionary<string, WeaponData> GetPlayerWeapons()
    {
        // Clear the existing weapon data dictionary
        weaponDataDict.Clear();

        // Check if the weapon manager is set
        if (weaponManager != null)
        {
            // Iterate through the weapons in the WeaponManager
            foreach (var weapon in weaponManager.GetWeapons())
            {
                // Add each weapon to the weapon data dictionary
                weaponDataDict.Add(weapon.Key, new WeaponData(false, false));
            }
        }
        else
        {
            Debug.LogWarning("WeaponManager reference is not set in PersistentData.");
        }

        return weaponDataDict;
    }

    public string GetCurrentWeaponName()
    {
        return currentWeaponName;
    }

    public void SetCurrentWeaponName(string weaponName)
    {
        currentWeaponName = weaponName;
    }
}
