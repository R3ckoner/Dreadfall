using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // This method is called when the button is clicked
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
