using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRandom : MonoBehaviour
{
    public GameObject mainCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            mainCamera.GetComponent<RandomPlayer>().isRolling = false;
    }
}
