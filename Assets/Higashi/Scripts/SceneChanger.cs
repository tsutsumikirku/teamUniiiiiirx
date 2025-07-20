using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChanceScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
