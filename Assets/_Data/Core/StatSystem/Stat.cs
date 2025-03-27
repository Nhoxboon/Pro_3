using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    public event Action OnCurrentValueZero;

    [SerializeField] protected float maxValue;
    public float MaxValue => maxValue;
    [SerializeField] protected float currentValue;
    public float CurrentValue
    {
        get => currentValue;
        protected set
        {
            currentValue = Mathf.Clamp(value, 0f, maxValue);

            if(currentValue <= 0)
            {
                OnCurrentValueZero?.Invoke();
            }
        }
    }

    public void SetMaxValue(float maxValueData)
    {
        this.maxValue = maxValueData;
    }

    public void Init() => CurrentValue = maxValue;

    public void Increase(float amount) => CurrentValue += amount;

    public void Decrease(float amount) => CurrentValue -= amount;
}
