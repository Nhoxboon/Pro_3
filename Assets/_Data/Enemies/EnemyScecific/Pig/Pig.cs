using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    protected PigIdleState pigIdleState;
    public PigIdleState PigIdleState => pigIdleState;

    protected PigMoveState pigMoveState;
    public PigMoveState PigMoveState => pigMoveState;

    [SerializeField] private EnemyDataSO pigDataSO;

    protected override void Start()
    {
        base.Start();

        pigIdleState = new PigIdleState(this, stateMachine, "idle", pigDataSO, this);
        pigMoveState = new PigMoveState(this, stateMachine, "move", pigDataSO, this);

        stateMachine.Initialize(pigMoveState);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyDataSO();
    }

    protected void LoadEnemyDataSO()
    {
        if (pigDataSO != null) return;
        pigDataSO = Resources.Load<EnemyDataSO>("Enemies/Pig");
        Debug.Log(transform.name + " LoadEnemyDataSO", gameObject);
    }
}
