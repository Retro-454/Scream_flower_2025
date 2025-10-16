using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("StartMenu"); // Loads the Start Menu scene
    }
}
