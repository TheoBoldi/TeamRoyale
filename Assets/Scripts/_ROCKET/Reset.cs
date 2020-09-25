using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public string retrySceneName;
    public string mainMenu;

    public void Resetscene()
    {
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(retrySceneName));
    }

    public void ReturnMenu()
    {
        TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(mainMenu));
    }
}
