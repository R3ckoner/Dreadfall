using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weaponsInventory = new List<GameObject>();
    public List<GameObject> playerChildrenToDisable = new List<GameObject>();

    private List<bool> childrenEnabledStates = new List<bool>();

    private void Start()
    {
        StoreChildrenEnabledStates();
        LoadChildrenEnabledStates();
        ApplyLoadedChildrenStates();
    }

    private void StoreChildrenEnabledStates()
    {
        foreach (var child in playerChildrenToDisable)
        {
            childrenEnabledStates.Add(child.activeSelf);
        }
    }

    private void LoadChildrenEnabledStates()
    {
        for (int i = 0; i < playerChildrenToDisable.Count; i++)
        {
            string key = "Child_" + i.ToString();
            int state = PlayerPrefs.GetInt(key, 1);
            childrenEnabledStates[i] = (state == 1);
        }
    }

    private void ApplyLoadedChildrenStates()
    {
        for (int i = 0; i < playerChildrenToDisable.Count; i++)
        {
            playerChildrenToDisable[i].SetActive(childrenEnabledStates[i]);
        }
    }

    public void AddWeapon(GameObject weaponObject)
    {
        weaponsInventory.Add(weaponObject);
    }

    // Add any other methods or functionality for the player's inventory here

    private void OnDestroy()
    {
        SaveChildrenEnabledStates();
    }

    private void SaveChildrenEnabledStates()
    {
        for (int i = 0; i < playerChildrenToDisable.Count; i++)
        {
            string key = "Child_" + i.ToString();
            int state = childrenEnabledStates[i] ? 1 : 0;
            PlayerPrefs.SetInt(key, state);
        }

        PlayerPrefs.Save();
    }
}
