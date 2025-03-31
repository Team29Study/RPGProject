using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoneAttackState : PlayerBaseState
{
    public PlayerNoneAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.IsAttacking && stateMachine.IsGroundState())
        {
            OnAttack();
            return;
        }

        if(stateMachine.IsBlocking && stateMachine.IsGroundState())
        {
            OnBlock();
            return;
        }
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeAttackState(stateMachine.ComboAttackState);
    }
    protected virtual void OnBlock()
    {
        stateMachine.ChangeAttackState(stateMachine.BlockState);
    }
}
