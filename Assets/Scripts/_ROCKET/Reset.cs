using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public string retrySceneName;
    public string mainMenu;
    public string aleatoire2;

    public void Resetscene()
    {
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(retrySceneName));
    }

    public void ReturnMenu()
    {
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(mainMenu));
    }

    public void NextScene()
    {
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(aleatoire2));
    }
}
