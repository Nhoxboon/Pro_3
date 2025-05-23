using UnityEngine;

public class Boss_1MoveState : MoveState
{
    private Boss_1 boss;

    private float randomRangeAtkCoolDown;

    public Boss_1MoveState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Boss_1 boss)
        : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();

        randomRangeAtkCoolDown = Random.Range(boss.RangedAttackStateSO.minAttackCooldown, boss.RangedAttackStateSO.maxAttackCooldown);

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
        
        if (Time.time >= boss.lastRangedAttackTime + randomRangeAtkCoolDown)
        {
            boss.lastRangedAttackTime = Time.time;

            if (boss.IsPhaseChange && Random.value < 0.5f)
                stateMachine.ChangeState(boss.MoveByPointState); 
            else
                stateMachine.ChangeState(boss.RangedAttackState);
        }
        
        else if(directionToPlayer != core.Movement.FacingDirection && PlayerCtrl.Instance.gameObject.activeInHierarchy)
        {
            core.Movement.Flip();
        }

        else if (performCloseRangeAction)
        {
            stateMachine.ChangeState(boss.MeleeAttackState);
        }

        else if (isDetectingWall)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(boss.IdleState);
        }
    }
}
