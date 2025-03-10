using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : NhoxBehaviour
{
    private static EnemyCtrl instance;
    public static EnemyCtrl Instance => instance;

    [SerializeField] protected Enemy enemy;
    public Enemy Enemy => enemy;

    [SerializeField] protected PlayAnimation enemyAnimation;
    public PlayAnimation EnemyAnimation => enemyAnimation;

    [SerializeField] protected TouchingDirection touchingDirection;
    public TouchingDirection TouchingDirection => touchingDirection;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("EnemyCtrl already exists in the scene. Deleting duplicate...");
            return;
        }
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy();
        this.LoadPlayAnimation();
        this.LoadTouchingDirection();
    }

    protected void LoadEnemy()
    {
        if (this.enemy != null) return;
        this.enemy = this.GetComponentInChildren<Enemy>();
        Debug.Log(transform.name + " LoadEnemy", gameObject);
    }

    protected void LoadPlayAnimation()
    {
        if (this.enemyAnimation != null) return;
        this.enemyAnimation = this.GetComponentInChildren<PlayAnimation>();
        Debug.Log(transform.name + " LoadPlayAnimation", gameObject);
    }

    protected void LoadTouchingDirection()
    {
        if (this.touchingDirection != null) return;
        this.touchingDirection = this.GetComponentInChildren<TouchingDirection>();
        Debug.Log(transform.name + " LoadTouchingDirection", gameObject);
    }
}
