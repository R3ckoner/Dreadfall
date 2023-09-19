using UnityEngine;

public class EnableOnPickup : MonoBehaviour
{
    private bool isPickedUp = false;

    public void PickUp()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            gameObject.SetActive(true);
        }
    }
}
