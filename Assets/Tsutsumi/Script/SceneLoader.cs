using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Load the specified scene asynchronously
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
