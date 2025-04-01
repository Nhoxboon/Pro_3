using System;
using UnityEngine;

public class Draw : WeaponComponent<DrawData, AttackDraw>
{
    private float drawPercentage;

    private bool hasEvaluatedDraw;
    public event Action<float> OnEvaluateCurve;

    protected override void HandleEnter()
    {
        base.HandleEnter();

        hasEvaluatedDraw = false;
    }

    private void HandleCurrentInputChange(bool newInput)
    {
        if (newInput || hasEvaluatedDraw)
            return;

        EvaluateDrawPercentage();
    }

    private void EvaluateDrawPercentage()
    {
        hasEvaluatedDraw = true;
        drawPercentage =
            currentAttackData.DrawCurve.Evaluate(
                Mathf.Clamp((Time.time - attackStartTime) / currentAttackData.DrawTime, 0f, 1f));
        OnEvaluateCurve?.Invoke(drawPercentage);
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();

        weapon.OnCurrentInputChange += HandleCurrentInputChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
    }

    #endregion
}