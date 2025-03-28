using System.Collections;
using System.Collections.Generic;
using Nhoxboon.Projectile;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected GameObject arrow;
    protected Projectile projectile;

    public RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Transform attackPosition) : base(enemy, stateMachine, animBoolName, enemyDataSO, attackPosition)
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

        Transform arrowTransform = ProjectileSpawner.Instance.SpawnProjectile("Arrow", attackPosition.position, attackPosition.rotation);
        arrow = arrowTransform.gameObject;
        arrow.transform.position = attackPosition.position;
        arrow.transform.rotation = attackPosition.rotation;

        projectile = arrow.GetComponent<Projectile>();
        projectile.FireProjectile(enemyDataSO.projectileSpeed, enemyDataSO.projectileTravelDistance, enemyDataSO.projectileDamage);
    }
}
