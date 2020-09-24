using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ennemy1 : MonoBehaviour
{

    public float movespeed = 5f;
    public float time;
    private float tmp;

    private int yes = 1;

    // Start is called before the first frame update
    void Start()
    {
        tmp = time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(yes, -1, 0) * movespeed * Time.deltaTime);
        //transform.position = new Vector2(Mathf.Lerp(transform.position.x, yes, movespeed), -1*movespeed*Time.deltaTime);

        time -= Time.deltaTime;

        if(time <= 0)
        {
            yes *= -1;
            time = tmp;
        }

    }
}
