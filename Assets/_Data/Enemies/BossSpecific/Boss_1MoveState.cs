using UnityEngine;

public class Boss_1MoveState : MoveState
{
    private Boss_1 boss;

    private float randomCoolDown;

    public Boss_1MoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Boss_1 boss) : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        randomCoolDown = Random.Range(boss.RangedAttackStateSO.minAttackCooldown, boss.RangedAttackStateSO.maxAttackCooldown);
        if (boss.IsPhaseChange)
        {
            moveSpeed = enemyDataSO.movementSpeed * 2f;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Vector3 playerPosition = enemyStateManager.CheckPlayerPosition();
        int directionToPlayer = playerPosition.x > core.Movement.Rb.position.x ? 1 : -1;
        
        if (directionToPlayer != core.Movement.FacingDirection)
        {
            core.Movement.Flip();
        }

        if (Time.time >= boss.lastRangedAttackTime + randomCoolDown)
        {
            boss.lastRangedAttackTime = Time.time;
            stateMachine.ChangeState(boss.BossRangedAttackState);
        }

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(boss.BossMeleeAttackState);
        }
        else if (isDetectingWall)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(boss.BossIdleState);
        }
    }
}