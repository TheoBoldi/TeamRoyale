using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public enum Power
{
    Shield,
    Time,
}

public class PlayerEntity : MonoBehaviour
{
    public Power powerType;
    public int healthPoint = 1;
    [HideInInspector]
    public float powerCooldown = 0f;

    private Action DoAction;
    private float defaultPlayerSpeed;
    private Vector3 defaultPlayerScale;

    [Header("Shield Power")]
    public Transform ShieldObj;
    public float shieldDuration = 2f;
    public float shieldCooldown = .1f;
    [Range(1f, 80f)]
    public float shieldMaxSise = 50f;
    public float shieldSpawnSpeed = 1f;
    public bool cooldownResetWhenBulletIsDetected = true;
    public float growthDurationForEachBulletDetected = 2f;
    public float stayGrowthDuration = 1f;
    public float shrinkDuration = 0.5f;
    [Range(1.001f, 2f)]
    public float maxGrowthMultiplicator = 1.3f;
    public int growthPhaseNumber = 3;

    private int activeGrowthPhasesNumber = 0;
    private int activeShrinkPhasesNumber = 0;
    private float shieldDurTime = 0f;
    private float growthDurTime = 0f;
    private float stayGrowthTime = 0f;
    private float shrinkDurTime = 0f;

    [Header("Time Power")]
    public bool alsoSlowDownPlayer = false;
    public bool alsoSpeedUpPlayer = true;
    [Range(0.001f, 0.999f)]
    public float slowSpeedPlayerMultiplicator = 0.9f;
    [Range(1.001f, 5f)]
    public float fastSpeedPlayerMultiplicator = 1.5f;
    [Range(0.001f, 0.999f)]
    public float slowSpeedMultiplicator = 0.5f;
    [Range(1.001f, 5f)]
    public float fastSpeedMultiplicator = 1.5f;
    public float slowDuration = 5f;
    public float slowCooldown = 5f;
    public float fastAfterSlowDuration = 5f;

    private float slowDurTime = 0f;
    private float fastDurTime = 0f;

    private void Awake()
    {
        defaultPlayerSpeed = gameObject.GetComponent<PlayerMovement>().moveSpeed;
        defaultPlayerScale = transform.localScale;
    }
    private void Start()
    {
        DoAction = DoActionVoid;
        ShieldObj.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inPaused)
        {
            PowerInput();
            DoAction();
            GrowthPlayer();
            StayGrowth();
            ShrinkPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") && GetComponentInChildren<Shield>().isTrigger)
        {
            if (cooldownResetWhenBulletIsDetected)
                gameObject.GetComponentInParent<PlayerEntity>().powerCooldown = 0f;
            GetComponentInChildren<Shield>().isTrigger = false;
        }
        else if (collision.CompareTag("bullet"))
            healthPoint -= collision.GetComponent<Bullet>().damage;
        else if (collision.CompareTag("Laser"))
            healthPoint = 0;
    }

    #region Powers
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
        powerCooldown = shieldCooldown;
        shieldDurTime = shieldDuration;
        if (activeGrowthPhasesNumber < growthPhaseNumber)
        {
            growthDurTime += growthDurationForEachBulletDetected;
            activeGrowthPhasesNumber++;
        }
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
        ShieldObj.localScale += shieldSpawnSpeed * Vector3.one * Time.deltaTime * shieldMaxSise;
        if (ShieldObj.localScale.x >=shieldMaxSise)
        {
            ShieldObj.localScale = Vector3.one * shieldMaxSise;
        }
    }

    public void GrowthPlayer()
    {
        if (growthDurTime <= 0f || activeGrowthPhasesNumber == 0)
            return;

        growthDurTime -= Time.deltaTime;

        Vector3 scaleObjectif = defaultPlayerScale * (1 + ((maxGrowthMultiplicator - 1) / growthPhaseNumber * activeGrowthPhasesNumber));
        if (growthDurTime <= 0f)
        {
            growthDurTime = 0f;
            transform.localScale = scaleObjectif;
        }
        else
            transform.localScale = Vector3.Lerp(transform.localScale, scaleObjectif, Time.deltaTime / growthDurTime);
        
        if (transform.localScale.x >= scaleObjectif.x)
        {
            transform.localScale = scaleObjectif;
            activeGrowthPhasesNumber--;
            stayGrowthTime += stayGrowthDuration;
        }
    }

    public void StayGrowth()
    {
        if (stayGrowthTime <= 0f)
            return;

        stayGrowthTime -= Time.deltaTime;

        if (stayGrowthTime <= 0f)
        {
            stayGrowthTime = 0f;
            shrinkDurTime += shrinkDuration;
            activeShrinkPhasesNumber++;
        }
    }

    public void ShrinkPlayer()
    {
        if (shrinkDurTime <= 0f || activeShrinkPhasesNumber == 0)
            return;

        shrinkDurTime -= Time.deltaTime;

        if (shrinkDurTime <= 0f)
        {
            shrinkDurTime = 0f;
            transform.localScale = defaultPlayerScale;
        }
        else
            transform.localScale = Vector3.Lerp(transform.localScale, defaultPlayerScale, Time.deltaTime / shrinkDurTime);

        if (transform.localScale.x <= defaultPlayerScale.x)
        {
            transform.localScale = defaultPlayerScale;
            activeShrinkPhasesNumber--;
        }
    }
    #endregion Shild Power

    #region Time Power
    public void StartTime()
    {
        powerCooldown = slowCooldown;

        if (!alsoSlowDownPlayer)
            gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed * (1 / slowSpeedMultiplicator);
        else
            gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed * slowSpeedPlayerMultiplicator;
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
                gameObject.GetComponent<PlayerMovement>().moveSpeed = defaultPlayerSpeed * fastSpeedPlayerMultiplicator;
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
    #endregion Powers
}
