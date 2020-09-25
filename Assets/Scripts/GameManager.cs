using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menus;

    private GameObject image;
    private GameObject controlMenu;
    private GameObject pauseMenu;
    private GameObject victoryPanel;

    [Header("Player")]
    public GameObject player;

    [Header("ObjectsToCollect")]
    public GameObject[] objects;

    [Header("SceneAfterDefeat")]
    public string sceneAfterDefeatName;

    [HideInInspector]
    public bool inPaused = false;

    private bool isReset = false;

    private void Awake()
    {
        image = menus.transform.GetChild(0).gameObject;
        controlMenu = menus.transform.GetChild(1).gameObject;
        pauseMenu = menus.transform.GetChild(2).gameObject;
        victoryPanel = menus.transform.GetChild(3).gameObject;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TransitionController.instance?.FadeOut();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && victoryPanel.activeInHierarchy == false
            && controlMenu.activeInHierarchy == false)
        {
            if (pauseMenu.activeInHierarchy == false)
            {
                inPaused = true;
                image.SetActive(true);
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                inPaused = false;
                pauseMenu.SetActive(false);
                image.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (player.GetComponent<PlayerGoals>().levelFinished)
        {
            if (victoryPanel.activeInHierarchy == false)
            {
                image.SetActive(true);
                victoryPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (player.GetComponent<PlayerEntity>().healthPoint <= 0 && !isReset)
        {
            Time.timeScale = 0;
            isReset = true;
            TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(sceneAfterDefeatName));
        }
    }
}
