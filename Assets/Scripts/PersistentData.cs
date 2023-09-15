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

    // Dictionary to store weapon data by weapon name (if needed)
    // private Dictionary<string, WeaponData> weaponDataDict = new Dictionary<string, WeaponData>();
    private string currentWeaponName;

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

    // Methods to get and set weapon data (if needed)
    // ...

    public string GetCurrentWeaponName()
    {
        return currentWeaponName;
    }

    public void SetCurrentWeaponName(string weaponName)
    {
        currentWeaponName = weaponName;
    }

    // Other methods and properties you may need (if not related to weapons)
}
