using System.Collections.Generic;
using UnityEngine;

public class MoveByPointState : State
{
    protected List<Transform> movePoints;
    protected float pointReachedThreshold = 0.1f;
    protected Transform targetPoint;

    public MoveByPointState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, List<Transform> movePoints) 
        : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.movePoints = movePoints;
    }

    public override void Enter()
    {
        base.Enter();

        if (movePoints.Count > 0)
        {
            targetPoint = movePoints[enemyStateManager.currentPointIndex];
        }
        FlipTowardsPoint();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        MoveToTarget();

        if (IsAtTarget())
        {
            OnReachPoint();
        }
    }
    
    protected virtual void FlipTowardsPoint()
    {
        float deltaX = targetPoint.position.x - enemyStateManager.transform.position.x;

        if (deltaX > 0f && core.Movement.FacingDirection != 1)
        {
            core.Movement.Flip();
        }
        else if (deltaX < 0f && core.Movement.FacingDirection != -1)
        {
            core.Movement.Flip();
        }
    }

    protected void FlipTowardsPlayer()
    {
        float deltaX = PlayerCtrl.Instance.transform.position.x - enemyStateManager.transform.position.x;

        if (deltaX > 0f && core.Movement.FacingDirection != 1)
        {
            core.Movement.Flip();
        }
        else if (deltaX < 0f && core.Movement.FacingDirection != -1)
        {
            core.Movement.Flip();
        }
    }
    
    protected virtual void MoveToTarget()
    {
        Vector2 position = enemyStateManager.transform.position;
        Vector2 targetPosition = targetPoint.position;

        float deltaX = targetPosition.x - position.x;
        float deltaY = targetPosition.y - position.y;

        bool reachedX = Mathf.Abs(deltaX) <= pointReachedThreshold;
        bool reachedY = Mathf.Abs(deltaY) <= pointReachedThreshold;

        if (!reachedX)
        {
            int directionX = deltaX > 0 ? 1 : -1;
            core.Movement.SetVelocityX(directionX * 10f);
        }
        else
        {
            core.Movement.SetVelocityX(0f);
        }

        if (reachedX && !reachedY)
        {
            int directionY = deltaY > 0 ? 1 : -1;
            core.Movement.SetVelocityY(directionY * 10f);
        }
        else
        {
            core.Movement.SetVelocityY(0f);
        }
    }


    protected virtual bool IsAtTarget()
    {
        Vector2 position = enemyStateManager.transform.position;
        Vector2 targetPosition = targetPoint.position;

        float deltaX = Mathf.Abs(targetPosition.x - position.x);
        float deltaY = Mathf.Abs(targetPosition.y - position.y);

        return deltaX <= pointReachedThreshold && deltaY <= pointReachedThreshold;
    }


    protected virtual void OnReachPoint()
    {
        enemyStateManager.currentPointIndex++;
        
        if (enemyStateManager.currentPointIndex >= movePoints.Count)
        {
            OnFinishAllPoints();
        }
        else
        {
            targetPoint = movePoints[enemyStateManager.currentPointIndex];
        }
    }

    protected virtual void OnFinishAllPoints()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityZero();
    }
}
