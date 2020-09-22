using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public string mainMenuSceneName;
    public string nextLevelSceneName;

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevelSceneName);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
