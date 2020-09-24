using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject victoryPanel;
    public GameObject controlMenu;

    [Header("Player")]
    public GameObject player;

    [Header("ObjectsToCollect")]
    public GameObject[] objects;

    [HideInInspector]
    public bool inPaused = false;

    private bool isReset = false;

    private void Start()
    {
        TransitionController.instance?.FadeOut();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && victoryPanel.gameObject.activeInHierarchy == false
            && controlMenu.gameObject.activeInHierarchy == false)
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                inPaused = true;
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                inPaused = false;
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (player.GetComponent<PlayerGoals>().levelFinished)
        {
            if (victoryPanel.gameObject.activeInHierarchy == false)
            {
                victoryPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (player.GetComponent<PlayerEntity>().healthPoint <= 0 && !isReset)
        {
            Time.timeScale = 0;
            isReset = true;
            TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
