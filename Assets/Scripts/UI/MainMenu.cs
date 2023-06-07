using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void PlayGame ()
    {
        levelLoader.LoadNextLevel();
    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
