using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       RaycastHit2D circle = Physics2D.CircleCast(this.transform.position, 90, Vector2.down);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
