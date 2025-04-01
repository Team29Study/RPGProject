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

        if(CanAttack())
        {
            OnAttack();
            return;
        }

        if(CanBlock())
        {
            OnBlock();
            return;
        }
    }

    // 공격이 가능한 상태인지 확인
    private bool CanAttack()
    {
        return Cursor.lockState == CursorLockMode.None && stateMachine.IsAttacking && stateMachine.IsGroundState();
    }

    // 방어가 가능한 상태인지 확인
    private bool CanBlock()
    {
        return Cursor.lockState == CursorLockMode.None && stateMachine.IsBlocking && stateMachine.IsGroundState();
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
