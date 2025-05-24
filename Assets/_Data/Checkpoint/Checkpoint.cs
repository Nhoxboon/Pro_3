
using System;
using UnityEngine;

public class Checkpoint : NhoxBehaviour
{
    [SerializeField] protected string checkpointId;
    [SerializeField] protected bool isActive;

    [ContextMenu("Get Checkpoint Id")]
    protected void GetCheckpointId()
    {
        checkpointId = System.Guid.NewGuid().ToString();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
        }
    }
    
    public void Activate()
    {
        isActive = true;
    }
}
