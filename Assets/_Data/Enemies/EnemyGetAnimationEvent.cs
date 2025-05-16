using System;
using UnityEngine;

public class EnemyGetAnimationEvent : NhoxBehaviour
{
    [SerializeField] protected EnemyStateManager enemyStateManager;
    public AttackState attackState;
    public DeadState deadState;

    protected void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    protected void FinishAttack()
    {
        attackState.FinishAttack();
    }
    
    protected void FinishDead()
    {
        deadState.FinishDead();
    }

    protected void SetParryWindowActive(int value)
    {
        attackState.SetParryWindowActive(Convert.ToBoolean(value));
    }

    protected void MoveAnimationAudioEvent()
    {
        AudioManager.Instance.PlaySFX(enemyStateManager.AudioDataSO.moveClip);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyStateManager();
    }

    protected void LoadEnemyStateManager()
    {
        if (enemyStateManager != null) return;
        enemyStateManager = transform.parent.GetComponent<EnemyStateManager>();
        Debug.Log(transform.name + " :LoadEnemyStateManager", gameObject);
    }
}