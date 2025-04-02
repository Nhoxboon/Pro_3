
using UnityEngine;
using UnityEngine.Events;

public class KnockbackSender : ProjectileComponent
{
    public UnityEvent OnKnockBack;

        [field: SerializeField] public LayerMask LayerMask { get; private set; }

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
                // Is the object under consideration part of the LayerMask that we can damage?
                if (!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                    continue;
                
                // NOTE: We need to use .collider.transform instead of just .transform to get the GameObject the collider we detected is attached to, otherwise it returns the parent
                if (!hit.collider.transform.gameObject.TryGetComponent(out Knockbackable knockBackable))
                    continue;

                knockBackable.Knockback(new CombatKnockbackData(angle, strength, direction, projectile.gameObject));

                OnKnockBack?.Invoke();
                
                return;
            }
        }

        // Handles checking to see if the data is relevant or not, and if so, extracts the information we care about
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

            projectile.ProjectileImpact.OnRaycastHit2D.AddListener(HandleRaycastHit2D);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            projectile.ProjectileImpact.OnRaycastHit2D.RemoveListener(HandleRaycastHit2D);
        }

        #endregion
}
