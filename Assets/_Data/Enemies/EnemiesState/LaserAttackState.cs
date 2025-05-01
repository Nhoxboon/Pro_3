using UnityEngine;

public class LaserAttackState : AttackState
{
    protected EnemyLaserAttackStateSO stateData;
    

    public LaserAttackState(EnemyStateManager enemyStateManager, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, EnemyAudioDataSO audioDataSO, Transform attackPosition,
        EnemyLaserAttackStateSO stateData) : base(enemyStateManager,
        stateMachine, animBoolName, enemyDataSO, audioDataSO, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityZero();
        // AudioManager.Instance.PlaySFX(audioDataSO.chargeClip); 
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (Time.time >= startTime + stateData.chargeTime + stateData.laserDuration)
        {
            FinishAttack();
        }
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        
    }
}
