using System.Collections;
using UnityEngine;

public class ProjectileComponent : NhoxBehaviour
{
    protected Projectile projectile;

    protected Rigidbody2D rb => projectile.Rb;

    public bool Active { get; private set; }


    // This function is called whenever the projectile is fired, indicating the start of it's journey
    protected virtual void Init()
    {
        SetActive(true);
        transform.parent.gameObject.SetActive(true);
    }

    protected virtual void ResetProjectile()
    {
    }

    /* Handles receiving specific data from the weapon. Implemented in any component that needs to use it. Automatically subscribed for all projectile
    components by this base class (see Awake and OnDestroy) */
    protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
    }

    public virtual void SetActive(bool value)
    {
        Active = value;
    }

    public virtual void SetActiveNextFrame(bool value)
    {
        StartCoroutine(SetActiveNextFrameCoroutine(value));
    }

    public IEnumerator SetActiveNextFrameCoroutine(bool value)
    {
        yield return null;
        SetActive(value);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadProjectile();
    }

    private void LoadProjectile()
    {
        if (projectile != null) return;
        projectile = GetComponentInParent<Projectile>();
        // Debug.Log(transform.name + ": Loading projectile", gameObject);
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();
        projectile.OnInit += Init;
        projectile.OnReset += ResetProjectile;
        projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnDestroy()
    {
        projectile.OnInit -= Init;
        projectile.OnReset -= ResetProjectile;
        projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
    }

    #endregion
}