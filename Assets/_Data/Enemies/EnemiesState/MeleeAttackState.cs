using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected EnemyMeleeAttackStateSO stateData;

    public MeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition, EnemyMeleeAttackStateSO stateData) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        var detectedObjs =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (var col in detectedObjs)
        {
            if (col.TryGetComponent<DamageReceiver>(out var damageable))
                damageable.Damage(new CombatDamageData(stateData.attackDamage, core.Root));

            if (col.TryGetComponent<Knockbackable>(out var knockbackable))
                knockbackable.Knockback(new CombatKnockbackData(stateData.knockbackAngle, stateData.knockbackStrength,
                    core.Movement.FacingDirection, core.Root));

            if (col.TryGetComponent<PoiseReceiver>(out var stunnable)) stunnable.Poise(new CombatPoiseData(stateData.poiseDamage, core.Root));
        }
    }
}