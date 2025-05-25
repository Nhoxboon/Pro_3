using System;
using UnityEngine;

public class SpawnByDistance : NhoxBehaviour
{
    public Transform player;
    public float currentDis;
    public float limitDis;
    public float respawnDis;

    protected void FixedUpdate()
    {
        GetDistance();
        Spawning();
    }

    protected void Spawning()
    {
        if(currentDis < limitDis) return;
        Vector3 pos = transform.position;
        pos.x += respawnDis;
        transform.position = pos;
    }
    
    protected void GetDistance()
    {
        currentDis = player.position.x - transform.position.x;
    }
}
