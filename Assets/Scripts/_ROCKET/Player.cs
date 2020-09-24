using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public GameObject losetex;
    public GameObject wintex;
    public GameObject losebut;
    public GameObject quit;

    public float playerspeed = 0f;
    public float maxspeed = 2.5f;
    public float axel = 0f;

    private bool leftb = false;
    private bool rightb = false;

    AudioSource zebi;
    public AudioSource rocket;
    public AudioClip crash;
    public AudioClip win;


    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        zebi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            leftb = true;
        }

        if (Input.GetKeyUp("q"))
        {
            leftb = false;
        }

        if (Input.GetKeyDown("d"))
        {
            rightb = true;
        }

        if (Input.GetKeyUp("d"))
        {
            rightb = false;
        }


        if (leftb || rightb)
        {
            if (leftb)
            {
                transform.Translate(new Vector3(playerspeed, 0, 0) * Time.deltaTime);
                playerspeed -= axel * Time.deltaTime;
                if (playerspeed <= -maxspeed)
                {
                    playerspeed = -maxspeed;
                }
            }
            else
            {
                transform.Translate(new Vector3(playerspeed, 0, 0) * Time.deltaTime);
                playerspeed += axel * Time.deltaTime;
                if (playerspeed >= maxspeed)
                {
                    playerspeed = maxspeed;
                }
            }
           
        }

        /*if (rightb)
        {
            transform.Translate(new Vector3(1, 0, 0) * playerspeed * Time.deltaTime);
            playerspeed += axel * Time.deltaTime;
            if (playerspeed >= maxspeed)
            {
                playerspeed = maxspeed;
            }
        }*/

        else
        {
            if (playerspeed > 0)
            {
                playerspeed -= axel * Time.deltaTime;
                transform.Translate(new Vector3(playerspeed, 0, 0) * Time.deltaTime);
            }

            else if (playerspeed < 0)
            {
                playerspeed += axel * Time.deltaTime;
                transform.Translate(new Vector3(playerspeed, 0, 0) * Time.deltaTime);
            }
        }
    }


    public void Left()
    {
        leftb = true;
    }

    public void Right()
    {
        rightb = true;
    }

    public void Stop()
    {
        leftb = false;
        rightb = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ennemy")
        {
            zebi.clip = crash;
            zebi.Play();
            rocket.Stop();
            losetex.SetActive(true);
            losebut.SetActive(true);
            quit.SetActive(true);

            Time.timeScale = 0;
        }

        if (collision.gameObject.tag == "win")
        {
            wintex.SetActive(true);
            losebut.SetActive(true);
            quit.SetActive(true);

            zebi.clip = win;
            zebi.Play();
            rocket.Stop();

            Time.timeScale = 0;
        }
    }
}
