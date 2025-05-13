using System.Collections.Generic;
using UnityEngine;

public class MoveByPointState : State
{
    protected List<Transform> movePoints;
    protected float pointReachedThreshold = 0.1f;
    protected Transform targetPoint;
    protected Transform lastPoint;

    public MoveByPointState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, List<Transform> movePoints)
        : base(enemyStateManager, stateMachine, animBoolName, enemyDataSO, audioDataSO)
    {
        this.movePoints = movePoints;
    }

    public override void Enter()
    {
        base.Enter();

        targetPoint = FindClosestPoint();
        lastPoint = targetPoint;

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
    
    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityZero();
    }

    protected Transform FindClosestPoint()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = enemyStateManager.transform.position;

        foreach (Transform point in movePoints)
        {
            float dist = Vector2.Distance(currentPos, point.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = point;
            }
        }

        return closest;
    }

    protected Transform FindNextClosestPoint()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = enemyStateManager.transform.position;

        foreach (Transform point in movePoints)
        {
            if (point == lastPoint) continue;

            float dist = Vector2.Distance(currentPos, point.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = point;
            }
        }

        return closest != null ? closest : lastPoint;
    }

    protected virtual void OnReachPoint()
    {
        lastPoint = targetPoint;
        targetPoint = FindNextClosestPoint();
        FlipTowardsPoint();
    }

    protected void FlipTowardsPoint()
    {
        float deltaX = targetPoint.position.x - enemyStateManager.transform.position.x;

        if (deltaX != 0)
        {
            int direction = deltaX > 0 ? 1 : -1;
            if (core.Movement.FacingDirection != direction)
            {
                core.Movement.Flip();
            }
        }
    }

    protected void MoveToTarget()
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

    protected bool IsAtTarget()
    {
        Vector2 position = enemyStateManager.transform.position;
        Vector2 targetPosition = targetPoint.position;

        float deltaX = Mathf.Abs(targetPosition.x - position.x);
        float deltaY = Mathf.Abs(targetPosition.y - position.y);

        return deltaX <= pointReachedThreshold && deltaY <= pointReachedThreshold;
    }
}
