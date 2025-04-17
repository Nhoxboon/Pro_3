using UnityEngine;

public class RangedAttackState : AttackState
{
    protected GameObject arrow;
    protected Projectile projectile;

    protected EnemyRangedAttackStateSO stateData;

    public RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,
        Transform attackPosition, EnemyRangedAttackStateSO stateData) : base(enemy, stateMachine, animBoolName,
        enemyDataSO, attackPosition)
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

        // var arrowTransform =
        //     ProjectileSpawner.Instance.Spawn("Arrow", attackPosition.position, attackPosition.rotation);
        // arrow = arrowTransform.gameObject;
        // arrow.transform.position = attackPosition.position;
        // arrow.transform.rotation = attackPosition.rotation;
        // arrow.SetActive(true);
        //
        // projectile = arrow.GetComponent<Projectile>();
        //TODO: after fix Spawner projectile
        // projectile.FireProjectile(enemyDataSO.projectileSpeed, enemyDataSO.projectileTravelDistance,
        //     enemyDataSO.projectileDamage);
    }
}