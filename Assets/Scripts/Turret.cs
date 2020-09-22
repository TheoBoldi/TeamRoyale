using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float shootRate = 2f;
    private float shootTimer;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    private void Start()
    {
        shootTimer = shootRate;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <=0f)
        {
            Shoot();
            shootTimer = shootRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
