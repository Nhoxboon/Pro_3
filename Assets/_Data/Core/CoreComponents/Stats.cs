using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    [SerializeField] protected EntityStatsDataSO entityStatsDataSO;

    [SerializeField] protected float currentHealth;

    protected override void Awake()
    {
        base.Awake();

        SetHealth();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEntityStatsDataSO();
    }

    protected void LoadEntityStatsDataSO()
    {
        if (entityStatsDataSO != null) return;
        if(transform.parent.parent.CompareTag("Player"))
        {
            entityStatsDataSO = Resources.Load<EntityStatsDataSO>("Player/PlayerStats");
        }
        else
        {
            entityStatsDataSO = Resources.Load<EntityStatsDataSO>("Enemies/Stats/" + transform.parent.parent.name + "Stats");
        }
        Debug.Log(transform.name + " LoadEntityStatsDataSO", gameObject);
    }

    protected void SetHealth()
    {
        currentHealth = entityStatsDataSO.health;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, entityStatsDataSO.health);
    }
}
