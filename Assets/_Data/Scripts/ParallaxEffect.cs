using System;
using UnityEngine;

public class ParallaxEffect : NhoxBehaviour
{
    protected float startPos, lenght;
    [SerializeField] protected GameObject cam;
    [SerializeField] protected float parallaxEffect = 0.8f;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position.x;
    }

    protected void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (movement < startPos - lenght)
        {
            startPos -= lenght;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCam();
    }

    protected void LoadCam()
    {
        if(cam != null) return;
        cam = Camera.main.gameObject;
        Debug.Log(transform.name + " LoadCam", gameObject);
    }
}
