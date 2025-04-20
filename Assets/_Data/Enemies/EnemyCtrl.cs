using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCtrl : NhoxBehaviour
{
    [SerializeField] protected SpriteRenderer sr;
    public SpriteRenderer Sr => sr;
    
    [SerializeField] protected EnemyStateManager enemyStateManager;
    public EnemyStateManager EnemyStateManager => enemyStateManager;

    [SerializeField] protected PlayAnimation enemyAnimation;
    public PlayAnimation EnemyAnimation => enemyAnimation;
    
    [SerializeField] protected EnemyGetAnimationEvent getAnimEvent;
    public EnemyGetAnimationEvent GetAnimEvent => getAnimEvent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
        LoadEnemyStateManager();
        LoadPlayAnimation();
        LoadAnimationEvent();
    }
    
    protected void LoadSpriteRenderer()
    {
        if (sr != null) return;
        sr = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(transform.name + " LoadSpriteRenderer", gameObject);
    }

    protected void LoadEnemyStateManager()
    {
        if (enemyStateManager != null) return;
        enemyStateManager = GetComponent<EnemyStateManager>();
        Debug.Log(transform.name + " LoadEnemy", gameObject);
    }

    protected void LoadPlayAnimation()
    {
        if (enemyAnimation != null) return;
        enemyAnimation = GetComponentInChildren<PlayAnimation>();
        Debug.Log(transform.name + " LoadPlayAnimation", gameObject);
    }
    
    protected void LoadAnimationEvent()
    {
        if (getAnimEvent != null) return;
        getAnimEvent = GetComponentInChildren<EnemyGetAnimationEvent>();
        Debug.Log(transform.name + " LoadAnimationEvent", gameObject);
    }
}
