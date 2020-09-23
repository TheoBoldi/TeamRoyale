using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource backgroundMusic;
    public AudioSource pickupSound;
    public AudioSource fallSound;
    public AudioSource spawnSound;
    public AudioSource checkout;
    public AudioClip fall1;
    public AudioClip fall2;
    public AudioClip fall3;
    public AudioClip fall4;
    public AudioClip fall5;


    public AudioClip background2;



    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //instance.BackgroundMusic();
    }

    public void BackgroundMusic()
    {
        backgroundMusic.Play();
    }


    public void PickUpSound()
    {
        pickupSound.Play();
    }

    public void FallSound()
    {
        int rand = Random.Range(0, 5);

        if (rand == 0)
        {
            fallSound.clip = fall1;
        }
        else if (rand == 1)
        {
            fallSound.clip = fall2;
        }
        else if (rand == 2)
        {
            fallSound.clip = fall3;
        }
        else if (rand == 3)
        {
            fallSound.clip = fall4;
        }
        else if (rand == 4)
        {
            fallSound.clip = fall5;
        }

        fallSound.Play();
    }

    public void SpawnSound()
    {
        spawnSound.Play();
    }

    public void CheckoutSound()
    {
        checkout.Play();
    }
}
