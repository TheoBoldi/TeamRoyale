using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertZone : MonoBehaviour
{
    private GameObject player;
    private GameObject yellow;
    private GameObject red;
    private FieldOfView fov;

    private bool soundEffect = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        yellow = transform.parent.parent.GetChild(2).GetChild(0).gameObject;
        red = transform.parent.parent.GetChild(2).GetChild(1).gameObject;
        fov = GetComponentInParent<FieldOfView>();
    }

    private void Update()
    {
        if (fov.lookAt)
        {
            red.SetActive(true);

            if (!soundEffect)
            {
                SoundManager.instance.Alert();
                soundEffect = true;
            }
        }

        else
        {
            red.SetActive(false);
            soundEffect = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!fov.lookAt)
            {
                yellow.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            yellow.SetActive(false);
        }
    }
}
