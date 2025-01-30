using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    /// <summary>
    /// loads the main menu
    /// </summary>
    public void LoadMainMenu() 
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// loads the level
    /// </summary>
    /// <param name="scene">level number</param>
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
