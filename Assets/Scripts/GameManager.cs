using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern to ensure there's only one GameManager.
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // List to store weapons.
    private List<Weapon> weapons = new List<Weapon>();

    // Add a weapon to the GameManager.
    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    // Remove a weapon from the GameManager.
    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
    }

    // Get a list of all weapons.
    public List<Weapon> GetWeapons()
    {
        return weapons;
    }

    private void Awake()
    {
        // Ensure there's only one GameManager.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
