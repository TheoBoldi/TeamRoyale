using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject controlMenu;

    [Header("Player")]
    public GameObject player;

    [Header("EndLevel")]
    public GameObject endLevel;

    [Header("ObjectsToCollect")]
    public GameObject[] objects;

    [HideInInspector]
    public bool inPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && victoryPanel.gameObject.activeInHierarchy == false
            && defeatPanel.gameObject.activeInHierarchy == false
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

        // TO DELETE -> JUST FOR TEST
        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (victoryPanel.gameObject.activeInHierarchy == false)
            {
                victoryPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                victoryPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (player.GetComponent<PlayerEntity>().healthPoint <= 0)
        {
            if (defeatPanel.gameObject.activeInHierarchy == false)
            {
                defeatPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
