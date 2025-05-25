using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class BossRoomTrigger : NhoxBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Collider2D col;
    [SerializeField] protected PlayableDirector bossIntroTimeline;
    [SerializeField] protected Animator anim;
    
    public event Action OnPlayerEnter;

    protected void OnEnable()
    {
        enemyCtrl.EnemyStateManager.Core.Stats.Health.OnCurrentValueZero += HandleBossDeath;
    }
    
    protected void OnDisable()
    {
        enemyCtrl.EnemyStateManager.Core.Stats.Health.OnCurrentValueZero -= HandleBossDeath;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
        LoadBoxCollider2D();
        LoadBossIntroTimeline();
        LoadAnimator();
    }
    
    protected void LoadEnemyCtrl()
    {
        if(enemyCtrl != null) return;
        enemyCtrl = transform.parent.GetComponentInChildren<EnemyCtrl>();
        Debug.Log(transform.name + " :LoadEnemyCtrl", gameObject);
    }

    protected void LoadBoxCollider2D()
    {
        if(col != null) return;
        col = GetComponent<Collider2D>();
        Debug.Log(transform.name + " :LoadBoxCollider2D", gameObject);
    }
    
    protected void LoadBossIntroTimeline()
    {
        if(bossIntroTimeline != null) return;
        bossIntroTimeline = transform.parent.GetComponentInChildren<PlayableDirector>();
        Debug.Log(transform.name + " :LoadBossIntroTimeline", gameObject);
    }

    protected void LoadAnimator()
    {
        if(anim != null) return;
        anim = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + " :LoadAnimator", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossIntroTimeline.Play();
        }
    }
    
    public void EnableControls()
    {
        col.isTrigger = false;
        anim.SetTrigger("active");
        OnPlayerEnter?.Invoke();
        InputManager.Instance.Unpause();
    }

    public void DisableControls()
    {
        InputManager.Instance.Pause();
    }
    
    protected void HandleBossDeath()
    {
        col.enabled = false;
        anim.SetTrigger("inactive");
    }
}