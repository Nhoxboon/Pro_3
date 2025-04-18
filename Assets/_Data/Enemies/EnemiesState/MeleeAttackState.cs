using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected EnemyMeleeAttackStateSO stateData;

    public MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition, EnemyMeleeAttackStateSO stateData) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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