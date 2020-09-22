using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenu;
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
