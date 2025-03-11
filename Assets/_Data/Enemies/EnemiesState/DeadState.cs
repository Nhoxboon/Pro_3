using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO) : base(enemy, stateMachine, animBoolName, enemyDataSO)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(enemyDataSO.deathBloodParticle, enemy.transform.parent.position, enemyDataSO.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(enemyDataSO.deathChunkParticle, enemy.transform.parent.position, enemyDataSO.deathChunkParticle.transform.rotation);

        enemy.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
