using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    protected PigIdleState pigIdleState;
    public PigIdleState PigIdleState => pigIdleState;

    protected PigMoveState pigMoveState;
    public PigMoveState PigMoveState => pigMoveState;

    protected PigDetectedPlayerState pigDetectedPlayerState;
    public PigDetectedPlayerState PigDetectedPlayerState => pigDetectedPlayerState;

    protected PigChargeState pigChargeState;
    public PigChargeState PigChargeState => pigChargeState;

    protected PigLookForPlayerState pigLookForPlayerState;
    public PigLookForPlayerState PigLookForPlayerState => pigLookForPlayerState;

    protected override void Start()
    {
        base.Start();

        pigIdleState = new PigIdleState(this, stateMachine, "idle", enemyDataSO, this);
        pigMoveState = new PigMoveState(this, stateMachine, "move", enemyDataSO, this);
        pigDetectedPlayerState = new PigDetectedPlayerState(this, stateMachine, "detectedPlayer", enemyDataSO, this);
        pigChargeState = new PigChargeState(this, stateMachine, "charge", enemyDataSO, this);
        pigLookForPlayerState = new PigLookForPlayerState(this, stateMachine, "lookForPlayer", enemyDataSO, this);

        stateMachine.Initialize(pigMoveState);
    }
    
}
