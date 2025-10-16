using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Change this to match your main game scene name exactly
    [SerializeField] private string mainSceneName = "Level";

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(mainSceneName);
        //whos ready for halloweeknd
    }

    public void OnQuitPressed()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
