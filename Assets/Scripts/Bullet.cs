﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particle;
    private float timer = 10f;
    public int damage = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var tmp = Instantiate(particle, this.transform);
            tmp.transform.parent = null;
            tmp.transform.localScale = new Vector3(1, 1, 1);
            SoundManager.instance.BulletHit();
            SoundManager.instance.PlayerDeath();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            var tmp = Instantiate(particle, this.transform);
            tmp.transform.parent = null;
            tmp.transform.localScale = new Vector3(1, 1, 1);
            SoundManager.instance.BulletHit();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            SoundManager.instance.ShieldParry();
            Destroy(gameObject);
        }
    }

}
