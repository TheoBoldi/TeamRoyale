using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nameLevel;
    public string creditsSceneName;

    void Start()
    {
        TransitionController.instance?.FadeOut();
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(nameLevel));
    }

    public void PlayCredits()
    {
        Time.timeScale = 1;
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(creditsSceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
