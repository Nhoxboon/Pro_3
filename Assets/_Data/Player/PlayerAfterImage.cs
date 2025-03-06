using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : NhoxBehaviour
{
    [SerializeField] protected float activeTime = 1f;
    [SerializeField] protected float timeActivated;
    [SerializeField] protected float alpha;
    [SerializeField] protected float alphaSet = 0.8f;
    [SerializeField] protected float alphaMultiplier = 0.85f;

    [SerializeField] protected Transform player;
    [SerializeField] protected SpriteRenderer SR;
    [SerializeField] protected SpriteRenderer playerSR;
    [SerializeField] protected Color color;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
        LoadPlayer();
        LoadPlayerSR();
    }

    private void OnEnable()
    {
        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }

    protected void LoadSpriteRenderer()
    {
        if (SR != null) return;
        SR = GetComponent<SpriteRenderer>();
        //Debug.Log(transform.name + " LoadSpriteRenderer", gameObject);
    }

    protected void LoadPlayer()
    {
        if (player != null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log(transform.name + " LoadPlayer", gameObject);
    }

    protected void LoadPlayerSR()
    {
        if (playerSR != null) return;
        playerSR = player.GetComponentInChildren<SpriteRenderer>();
        //Debug.Log(transform.name + " LoadPlayerSR", gameObject);
    }
}
