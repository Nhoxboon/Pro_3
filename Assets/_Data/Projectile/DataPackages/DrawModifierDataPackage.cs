using System;
using UnityEngine;


    [Serializable]
    public class DrawModifierDataPackage : ProjectileDataPackage
    {
        public float DrawPercentage
        {
            get => drawPercentage;
            set => drawPercentage = Mathf.Clamp01(value);
        }

        private float drawPercentage;
    }
