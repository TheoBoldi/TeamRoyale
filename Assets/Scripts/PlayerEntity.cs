using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power
{
    Shild,
}

public class PlayerEntity : MonoBehaviour
{
    public Power powerType;
    public Action DoAction;

    [Header("Shild")]
    public Transform ShildObj;

    public float shildDuration = 2f;
    private float shildDurTime = 0f;
    
    public float shildCooldawn = .1f;

    public float shildMaxSise = 1f;

    private void Start()
    {
        DoAction = DoActionVoid;
        ShildObj.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        PowerInput();

        DoAction();
    }

    public void DoActionVoid()
    {
        Debug.Log("Void");
    }

    public void PowerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Gauche");

            switch (powerType)
            {
                case global::Power.Shild:
                    StartShild();
                    break;
                default:
                    break;
            }
        }
    }

    public void StartShild()
    {
        Debug.Log("Start Shild");
        shildDurTime = shildDuration;
        DoAction = DoShild;
    }

    public void DoShild()
    {
        Debug.Log("DoShild");
        shildDurTime -= Time.deltaTime;
        if (shildDurTime <= 0f)
        {
            ShildObj.localScale = Vector3.zero;
            DoAction = DoActionVoid;
            return;
        }

        // grow shild
        ShildObj.localScale += Vector3.one * Time.deltaTime * shildMaxSise;
        if (ShildObj.localScale.x >=shildMaxSise)
        {
            ShildObj.localScale = Vector3.one * shildMaxSise;
        }
    }
}
