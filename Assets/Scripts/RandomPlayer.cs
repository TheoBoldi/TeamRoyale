using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomPlayer : MonoBehaviour
{
    public float speed;
    public float durationAfterRoll;
    public GameObject roll;
    public GameObject imageToChange;

    [Header("Sprites")]
    public Sprite time;
    public Sprite shield;
    public Sprite invisibility;

    [Header("SceneToLoad")]
    public string timeScene;
    public string shieldScene;
    public string invisibilityScene;

    [HideInInspector]
    public bool isRolling = true;

    private bool isWaiting = true;
    private bool isLoading = true;
    private int playerChosen;

    private void Start()
    {
        TransitionController.instance?.FadeOut();
        playerChosen = UnityEngine.Random.Range(1, 4);
        Debug.Log(playerChosen.ToString());

        // Change the player img in the animation
        if (playerChosen == 1) // Time
        {
            imageToChange.GetComponent<Image>().color = new Color32(91, 124, 121, 255);
            imageToChange.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = time;
        }
        else if (playerChosen == 2) // Shield
        {
            imageToChange.GetComponent<Image>().color = new Color32(181, 82, 82, 255);
            imageToChange.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = shield;
        }
        else // Invisibility
        {
            imageToChange.GetComponent<Image>().color = new Color32(74, 128, 203, 255);
            imageToChange.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = invisibility;
        }

    }

    void Update()
    {
        if (isRolling)
            roll.transform.position -= new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
        else if (isWaiting)
        {
            durationAfterRoll -= Time.deltaTime;
            if (durationAfterRoll <= 0f)
                isWaiting = false;
        }
        else if (isLoading)
        {
            isLoading = false;
            if (playerChosen == 1)
                TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(timeScene));
            else if (playerChosen == 2)
                TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(shieldScene));
            else
                TransitionController.instance?.FadeIn(() => SceneManager.LoadScene(invisibilityScene));
        }
    }
}
