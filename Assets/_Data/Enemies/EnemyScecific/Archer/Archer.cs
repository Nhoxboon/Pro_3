using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    protected ArcherIdleState archerIdleState;
    public ArcherIdleState ArcherIdleState => archerIdleState;

    protected ArcherMoveState archerMoveState;
    public ArcherMoveState ArcherMoveState => archerMoveState;

    [SerializeField] Transform meleeAttackPosition;

    protected override void Start()
    {
        base.Start();

        archerIdleState = new ArcherIdleState(this, stateMachine, "idle", enemyDataSO, this);
        archerMoveState = new ArcherMoveState(this, stateMachine, "move", enemyDataSO, this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttackPosition();
    }

    protected void LoadMeleeAttackPosition()
    {
        if (meleeAttackPosition != null) return;
        meleeAttackPosition = transform.parent.Find("Attack/MeleeAttack");
        Debug.Log(transform.name + " LoadMeleeAttackPosition", gameObject);
    }
}
