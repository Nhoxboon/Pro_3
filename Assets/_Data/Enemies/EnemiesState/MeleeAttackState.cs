using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{

    public MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Transform attackPosition) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition)
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

        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(attackPosition.position, enemyDataSO.attackRadius, enemyDataSO.whatIsPlayer);

        foreach(Collider2D col in detectedObjs)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.Damage(enemyDataSO.attackDamage);
            }

            IKnockbackable knockbackable = col.GetComponent<IKnockbackable>();

            if(knockbackable != null)
            {
                knockbackable.Knockback(enemyDataSO.knockbackAngle, enemyDataSO.knockbackStrength, core.Movement.FacingDirection);
            }    
        }
    }
}
