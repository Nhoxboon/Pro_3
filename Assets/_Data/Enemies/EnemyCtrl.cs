using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : NhoxBehaviour
{
    [SerializeField] protected Enemy enemy;
    public Enemy Enemy => enemy;

    [SerializeField] protected PlayAnimation enemyAnimation;
    public PlayAnimation EnemyAnimation => enemyAnimation;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy();
        this.LoadPlayAnimation();
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
}
