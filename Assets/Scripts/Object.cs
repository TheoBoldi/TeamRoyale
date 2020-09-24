using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType() == typeof(UnityEngine.CapsuleCollider2D))
        {
            SoundManager.instance.Collectible();
            collision.GetComponent<PlayerGoals>().objectsCollected++;
            Destroy(gameObject);
        }
    }
}
