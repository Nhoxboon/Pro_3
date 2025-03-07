using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : NhoxBehaviour
{
    [SerializeField] protected PlayerStateMachine stateMachine;
    public PlayerStateMachine StateMachine => stateMachine;

    [SerializeField] protected PlayerIdleState playerIdleState;
    public PlayerIdleState PlayerIdleState => playerIdleState;

    [SerializeField] protected PlayerMoveState playerMoveState;
    public PlayerMoveState PlayerMoveState => playerMoveState;

    [SerializeField] protected PlayerDataSO playerDataSO;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(stateMachine, playerDataSO, "idle");
        playerMoveState = new PlayerMoveState(stateMachine, playerDataSO, "move");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(playerIdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
