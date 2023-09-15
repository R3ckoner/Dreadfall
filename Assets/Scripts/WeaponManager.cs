using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    // List to store all weapon names.
    public List<string> weaponNames = new List<string>();
    private HashSet<string> enabledWeapons = new HashSet<string>(); // Store enabled weapon names.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddWeapon(string weaponName)
    {
        if (!weaponNames.Contains(weaponName))
        {
            weaponNames.Add(weaponName);
            SaveWeaponData();
        }
    }

    public List<string> GetWeapons()
    {
        return weaponNames;
    }

    public bool IsWeaponEnabled(string weaponName)
    {
        return enabledWeapons.Contains(weaponName);
    }

    public void EnableWeapon(string weaponName)
    {
        enabledWeapons.Add(weaponName);
        SaveWeaponData();
    }

    public void DisableWeapon(string weaponName)
    {
        enabledWeapons.Remove(weaponName);
        SaveWeaponData();
    }

    public void SaveWeaponData()
    {
        // Save the list of weapon names and enabled weapons as JSON strings in PlayerPrefs.
        string weaponData = JsonUtility.ToJson(weaponNames);
        PlayerPrefs.SetString("WeaponData", weaponData);

        // Convert the HashSet to a List for serialization
        List<string> enabledWeaponList = enabledWeapons.ToList();
        string enabledWeaponData = JsonUtility.ToJson(enabledWeaponList);
        PlayerPrefs.SetString("EnabledWeaponData", enabledWeaponData);

        PlayerPrefs.Save();
    }

public void LoadWeaponData()
{
    // Load the list of weapon names from PlayerPrefs.
    if (PlayerPrefs.HasKey("WeaponData"))
    {
        string weaponData = PlayerPrefs.GetString("WeaponData");
        weaponNames = JsonUtility.FromJson<List<string>>(weaponData);

        // Load the list of enabled weapon names from PlayerPrefs.
        if (PlayerPrefs.HasKey("EnabledWeaponData"))
        {
            string enabledWeaponData = PlayerPrefs.GetString("EnabledWeaponData");
            List<string> enabledWeaponList = JsonUtility.FromJson<List<string>>(enabledWeaponData);
            enabledWeapons = new HashSet<string>(enabledWeaponList);

            // Enable the weapons associated with the enabled weapon names.
            foreach (string enabledWeaponName in enabledWeapons)
            {
                // Find the corresponding GameObject and enable it.
                GameObject weaponObject = GameObject.Find(enabledWeaponName);
                if (weaponObject != null)
                {
                    weaponObject.SetActive(true);
                }
            }
        }
    }
}


    public void ClearWeaponData()
    {
        // Clear the saved weapon data (for example, when starting a new game).
        weaponNames.Clear();
        enabledWeapons.Clear();
        PlayerPrefs.DeleteKey("WeaponData");
        PlayerPrefs.DeleteKey("EnabledWeaponData");
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadWeaponData();
    }
}
