using System;
using System.Collections;
using UnityEngine;

public class BossRoomTrigger : NhoxBehaviour
{
    [SerializeField] protected Collider2D col;
    public event Action OnPlayerEnter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBoxCollider2D();
    }

    protected void LoadBoxCollider2D()
    {
        if(col != null) return;
        col = GetComponent<Collider2D>();
        Debug.Log(transform.name + " :LoadBoxCollider2D", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();

            //TODO: Temporary solution to prevent multiple triggers
            StartCoroutine(DisableTriggerAfterDelay());
        }
    }

    private IEnumerator DisableTriggerAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
    }
}