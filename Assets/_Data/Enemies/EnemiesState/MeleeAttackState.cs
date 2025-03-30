using UnityEngine;

public class MeleeAttackState : AttackState
{
    public MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition)
    {
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
            Physics2D.OverlapCircleAll(attackPosition.position, enemyDataSO.attackRadius, enemyDataSO.whatIsPlayer);

        foreach (var col in detectedObjs)
        {
            if (col.TryGetComponent<DamageReceiver>(out var damageable))
                damageable.Damage(new CombatDamageData(enemyDataSO.attackDamage, core.Root));

            if (col.TryGetComponent<Knockbackable>(out var knockbackable))
                knockbackable.Knockback(enemyDataSO.knockbackAngle, enemyDataSO.knockbackStrength,
                    core.Movement.FacingDirection);

            if (col.TryGetComponent<PoiseReceiver>(out var stunnable)) stunnable.Poise(enemyDataSO.attackDamage);
        }
    }
}