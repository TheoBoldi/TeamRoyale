using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoals : MonoBehaviour
{
    [HideInInspector]
    public bool levelFinished = false;
    [HideInInspector]
    public int objectsCollected = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish")
            && GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().objects.Length <= objectsCollected)
            levelFinished = true;
    }
}
