using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField] protected Attribute health;
    public Attribute Health => health;

    [SerializeField] protected Attribute poise;
    public Attribute Poise => poise;

    [SerializeField] protected EntityStatsDataSO entityStatsDataSO;


    protected override void Awake()
    {
        base.Awake();

        health.SetMaxValue(entityStatsDataSO.health);
        poise.SetMaxValue(entityStatsDataSO.stagger);
        health.Init();
        poise.Init();
    }

    private void Update()
    {
        if (poise.CurrentValue.Equals(poise.MaxValue)) return;

        poise.Increase(entityStatsDataSO.poiseRecoveryRate * Time.deltaTime);
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
}
