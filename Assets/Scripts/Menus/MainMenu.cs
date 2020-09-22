using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nameLevel;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(nameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
