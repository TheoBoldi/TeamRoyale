using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject controlMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && victoryPanel.gameObject.activeInHierarchy == false
            && defeatPanel.gameObject.activeInHierarchy == false
            && controlMenu.gameObject.activeInHierarchy == false)
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
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

        // TO DELETE -> JUST FOR TEST
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (defeatPanel.gameObject.activeInHierarchy == false)
            {
                defeatPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                defeatPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
