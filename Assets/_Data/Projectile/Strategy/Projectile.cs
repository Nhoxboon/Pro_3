using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nhoxboon.Projectile.Strategy
{
    /// <summary>
    /// This class is the interface between projectile components and any entity that spawns a projectile.
    /// </summary>
    public class Projectile : NhoxBehaviour
    {
        // This event is used to notify all projectile components that Init has been called
        public event Action OnInit;
        public event Action OnReset;

        public event Action<ProjectileDataPackage> OnReceiveDataPackage;

        public Rigidbody2D Rigidbody2D { get; private set; }

        public void Init()
        {
            OnInit?.Invoke();
        }

        public override void Reset()
        {
            base.Reset();
            OnReset?.Invoke();
        }

        /* This function is called before Init from the weapon. Any weapon component can use this to function to pass along information that the projectile might need that is
        weapon specific, such as: damage amount, draw length modifiers, etc. */
        public void SendDataPackage(ProjectileDataPackage dataPackage)
        {
            OnReceiveDataPackage?.Invoke(dataPackage);
        }

        #region Plumbing

        protected override void Awake()
        {
            base.Awake();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        } 

        #endregion
    }
}

