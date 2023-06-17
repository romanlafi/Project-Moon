using UnityEngine;
using UnityEngine.UI;

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

    public void SwitchMenu (GameObject targetMenu) 
    {
        if (targetMenu != null)
        {
            Button[] buttons = targetMenu.GetComponentsInChildren<Button>();

            if (buttons.Length > 0)
            {
                buttons[0].Select();
                buttons[0].OnSelect(null);
            }
        }
    }
}
