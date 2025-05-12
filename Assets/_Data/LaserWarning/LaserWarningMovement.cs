using UnityEngine;
public class LaserWarningMovement : NhoxBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected LineRenderer lineRenderer;

    [SerializeField] protected float laserLength = 10f;
    [SerializeField] protected float minAngle = -15f;
    [SerializeField] protected float maxAngle = 1f;

    protected Vector3 currentDirection;
    public Vector3 CurrentDirection => currentDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
        LoadLineRenderer();
    }
    
    protected void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + " :LoadEnemyCtrl", gameObject);
    }

    protected void LoadLineRenderer()
    {
        if (lineRenderer != null) return;
        lineRenderer = GetComponentInChildren<LineRenderer>();
        Debug.Log(transform.name + " :LoadLineRenderer", gameObject);
    }

    public void EnableLaser()
    {
        SetRandomLaserDirection();
        lineRenderer.enabled = true;
        UpdateLaserLine(currentDirection);
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    public void StopLaser()
    {
        UpdateLaserLine(currentDirection);
    }

    protected void SetRandomLaserDirection()
    {
        float actualMinAngle = enemyCtrl.EnemyStateManager.Core.Movement.FacingDirection > 0 ? minAngle : -maxAngle;
        float actualMaxAngle = enemyCtrl.EnemyStateManager.Core.Movement.FacingDirection > 0 ? maxAngle : -minAngle;
        
        float randomAngle = Random.Range(actualMinAngle, actualMaxAngle);
        currentDirection = CalculateLaserDirection(randomAngle);
    }

    protected Vector3 CalculateLaserDirection(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Vector3 baseDirection = transform.TransformDirection(Vector3.right);
        return rotation * baseDirection;
    }

    protected void UpdateLaserLine(Vector3 direction)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + direction.normalized * laserLength);
    }

    public Vector3 GetLaserDirection()
    {
        return currentDirection.normalized;
    }

    private void OnDrawGizmosSelected()
    {

        Vector3 basePosition = transform.position;
        Vector3 forward = transform.TransformDirection(Vector3.right);

        int facingDir = 1;
        if (enemyCtrl.EnemyStateManager.Core.Movement != null)
            facingDir = enemyCtrl.EnemyStateManager.Core.Movement.FacingDirection;
        
        float actualMinAngle = facingDir > 0 ? minAngle : -maxAngle;
        float actualMaxAngle = facingDir > 0 ? maxAngle : -minAngle;

        Vector3 minDir = Quaternion.Euler(0f, 0f, actualMinAngle) * forward;
        Vector3 maxDir = Quaternion.Euler(0f, 0f, actualMaxAngle) * forward;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(basePosition, basePosition + minDir.normalized * laserLength);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(basePosition, basePosition + maxDir.normalized * laserLength);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(basePosition, basePosition + currentDirection.normalized * laserLength);
    }
}