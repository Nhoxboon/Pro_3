using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : NhoxBehaviour
{
    [Header("Core Component")]
    [SerializeField] protected Core core;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCore();
    }

    protected void LoadCore()
    {
        if (core != null) return;
        core = GetComponentInParent<Core>();
        //Debug.Log(transform.name + " LoadCore", gameObject);
    }
}
