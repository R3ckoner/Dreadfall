using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    private void Awake()
    {
        // This ensures that the GameObject this script is attached to won't be destroyed
        // when a new scene is loaded.
        DontDestroyOnLoad(this.gameObject);
    }
}