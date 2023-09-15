using UnityEngine;

public class TaggedObjectManager : MonoBehaviour
{
    // Method to enable objects with a specified tag.
    public void EnableObjectsWithTag(string targetTag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(true);
        }
    }

    // Method to disable objects with a specified tag.
    public void DisableObjectsWithTag(string targetTag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(false);
        }
    }
}

