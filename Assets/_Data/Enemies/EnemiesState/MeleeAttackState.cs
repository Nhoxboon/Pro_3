using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected float attackDamage;
    protected float knockbackStrength;
    protected float poiseDamage;
    
    protected EnemyMeleeAttackStateSO stateData;

    public MeleeAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO,
        Transform attackPosition, EnemyMeleeAttackStateSO stateData) : base(enemyStateManager, stateMachine,
        animBoolName, enemyDataSO, audioDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        attackDamage = stateData.attackDamage;
        knockbackStrength = stateData.knockbackStrength;
        poiseDamage = stateData.poiseDamage;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        AudioManager.Instance.PlaySFX(audioDataSO.meleeAttackClip);
        var detectedObjs =
            Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (var col in detectedObjs)
        {
            if (col.TryGetComponent<DamageReceiver>(out var damageable))
                damageable.Damage(new CombatDamageData(attackDamage, core.Root));

            if (col.TryGetComponent<Knockbackable>(out var knockbackable))
                knockbackable.Knockback(new CombatKnockbackData(stateData.knockbackAngle, knockbackStrength,
                    core.Movement.FacingDirection, core.Root));

            if (col.TryGetComponent<PoiseReceiver>(out var stunnable))
                stunnable.Poise(new CombatPoiseData(poiseDamage, core.Root));
        }
    }
}