    using UnityEngine;
    using UnityEngine.SceneManagement;
     
    public class SceneChangeOnButtonPress : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene("Desert");
        }
    }
