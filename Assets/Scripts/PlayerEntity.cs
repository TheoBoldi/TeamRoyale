using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Power
{
    Shield,
    Time,
}

public class PlayerEntity : MonoBehaviour
{
    public Power powerType;
    public Action DoAction;

    private float powerCooldown = 0f;
    private float defaultPlayerSpeed;

    [Header("Shield Power")]
    public Transform ShieldObj;
    public float shieldDuration = 2f;
    public float shieldCooldown = .1f;
    public float shieldMaxSise = 1f;

    private float shieldDurTime = 0f;

    [Header("Time Power")]
    public bool alsoSlowDownPlayer = false;
    public bool alsoSpeedUpPlayer = true;
    [Range(0.1f, 0.9f)]
    public float slowSpeedMultiplicator = 0.5f;
    [Range(1.1f, 5f)]
    public float fastSpeedMultiplicator = 1.5f;
    public float slowDuration = 5f;
    public float slowCooldown = 5f;
    public float fastAfterSlowDuration = 5f;

    private float slowDurTime = 0f;
    private float fastDurTime = 0f;

    private void Start()
    {
        DoAction = DoActionVoid;
        ShieldObj.localScale = Vector3.zero;
        defaultPlayerSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PowerInput();
        DoAction();
    }

    public void DoActionVoid()
    {
    }

    public void PowerInput()
    {
        powerCooldown -= Time.unscaledDeltaTime;
        if (Input.GetMouseButtonDown(0) && powerCooldown <= 0f)
        {
            Debug.Log("Click Gauche");
            gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed;

            switch (powerType)
            {
                case global::Power.Shield:
                    StartShield();
                    break;
                case global::Power.Time:
                    StartTime();
                    break;
                default:
                    break;
            }
        }
    }

    #region Shild Power
    public void StartShield()
    {
        Debug.Log("Start Shield");
        powerCooldown = shieldCooldown;
        shieldDurTime = shieldDuration;
        DoAction = DoShield;
    }

    public void DoShield()
    {
        shieldDurTime -= Time.deltaTime;
        if (shieldDurTime <= 0f)
        {
            ShieldObj.localScale = Vector3.zero;
            DoAction = DoActionVoid;
            return;
        }

        // grow shild
        ShieldObj.localScale += Vector3.one * Time.deltaTime * shieldMaxSise;
        if (ShieldObj.localScale.x >=shieldMaxSise)
        {
            ShieldObj.localScale = Vector3.one * shieldMaxSise;
        }
    }
    #endregion Shild Power

    #region Time Power
    public void StartTime()
    {
        Debug.Log("Start Time");
        powerCooldown = slowCooldown;

        if (!alsoSlowDownPlayer)
            gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed * (1 / slowSpeedMultiplicator);
        Time.timeScale = slowSpeedMultiplicator;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        slowDurTime = slowDuration;
        DoAction = DoSlowTime;
    }

    public void DoSlowTime()
    {
        slowDurTime -= Time.unscaledDeltaTime;
        if (slowDurTime <= 0f)
        {
            if (!alsoSpeedUpPlayer)
                gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed * (fastSpeedMultiplicator - 1);
            else
                gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed;
            Time.timeScale = fastSpeedMultiplicator;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            fastDurTime = fastAfterSlowDuration;
            DoAction = DoFastTime;
        }
    }

    public void DoFastTime()
    {
        fastDurTime -= Time.unscaledDeltaTime;
        if (fastDurTime <= 0f)
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            DoAction = DoActionVoid;
        }
    }
    #endregion Time Power
}
