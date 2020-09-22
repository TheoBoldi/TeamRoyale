using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Transform origin;
    [Header("Destination points")]
    public List<Transform> destination;

    [Header("Speed")]
    public float speed = 3f;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        origin = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            float ratio = Time.deltaTime * speed;
            transform.position = Vector2.MoveTowards(origin.position, destination[i].position, ratio);

            if (Vector2.Distance(this.transform.position, destination[i].position) < 0.5f)
            {
                origin = this.transform;
                i++;
                if (i >= destination.Count)
                {
                    i = 0;
                }
            }
        }
    }
}
