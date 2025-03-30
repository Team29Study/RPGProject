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
    }

    protected virtual void OnAttack()
    {
        stateMachine.ChangeAttackState(stateMachine.ComboAttackState);
    }
}
