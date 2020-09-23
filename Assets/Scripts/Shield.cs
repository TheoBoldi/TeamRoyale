using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [HideInInspector]
    public bool isTrigger = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
            isTrigger = true;
    }
}
