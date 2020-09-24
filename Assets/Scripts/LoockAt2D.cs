using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoockAt2D : MonoBehaviour
{
    public float offsetAngle = 0f;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 difPos = target.position - transform.position;
        float rotationZ = Mathf.Atan2(difPos.y, difPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ+ offsetAngle);
    }
}
