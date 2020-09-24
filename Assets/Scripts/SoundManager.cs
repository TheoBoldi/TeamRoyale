using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("General Sounds")]
    public AudioSource main_theme;
    public AudioSource button_click;

    [Header("Player Sounds")]
    public AudioSource slow_time;
    public AudioSource speed_time;
    public AudioSource shield_pop;
    public AudioSource shield_parry;
    public AudioSource invisibility;
    public AudioSource player_death;
    public AudioSource collectible;

    [Header("Enemies Sounds")]
    public AudioSource alert;
    public AudioSource bullet_hit;
    public AudioSource bullet_shoot;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        instance.MainTheme();
    }

    //General
    public void MainTheme()
    {
        main_theme.Play();
    }

    public void ButtonClick()
    {
        button_click.Play();
    }

    //Player
    public void SlowTime()
    {
        slow_time.Play();
    }

    public void SpeedTime()
    {
        speed_time.Play();
    }

    public void ShieldPop()
    {
        shield_pop.Play();
    }

    public void ShieldParry()
    {
        shield_parry.Play();
    }

    public void Invisibility()
    {
        invisibility.Play();
    }

    public void PlayerDeath()
    {
        player_death.Play();
    }

    public void Collectible()
    {
        collectible.Play();
    }

    //Enemies
    public void Alert()
    {
        alert.Play();
    }

    public void BulletHit()
    {
        bullet_hit.Play();
    }

    public void BulletShoot()
    {
        bullet_shoot.Play();
    }
}
