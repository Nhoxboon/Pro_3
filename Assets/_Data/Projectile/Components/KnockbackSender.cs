
using UnityEngine;
using UnityEngine.Events;

public class KnockbackSender : ProjectileComponent
{
    public UnityEvent OnKnockBack;

    [SerializeField] protected LayerMask layerMask;    

        private int direction;
        private float strength;
        private Vector2 angle;

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (!Active)
                return;

            direction = (int)Mathf.Sign(transform.right.x);
            
            foreach (var hit in hits)
            {
                if (!LayerMaskUtilities.IsLayerInMask(hit, layerMask))
                    continue;
                
                // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
                if (!hit.collider.transform.gameObject.TryGetComponent(out Knockbackable knockBackable))
                    continue;

                knockBackable.Knockback(new CombatKnockbackData(angle, strength, direction, projectile.gameObject));

                OnKnockBack?.Invoke();
                
                return;
            }
        }
        
        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if (dataPackage is not KnockBackDataPackage knockBackDataPackage)
                return;

            strength = knockBackDataPackage.Strength;
            angle = knockBackDataPackage.Angle;
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();

            projectile.ProjectileHitbox.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            projectile.ProjectileHitbox.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);
        }
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadLayerMask();
        }
    
        protected void LoadLayerMask()
        {
            if (layerMask != 0) return;
            layerMask = LayerMask.GetMask("Damageable");
            Debug.Log(transform.name + ": LoadLayerMask", gameObject);
        }

        #endregion
}
