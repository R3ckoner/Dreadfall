using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int currentWeaponIndex;
    public List<string> playerInventory = new List<string>(); // Example inventory list for storing weapon names
    // Add other player-related data fields here as needed
}

