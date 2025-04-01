using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // 땅에 붙어있는 상태가 아니고 Controller의 y 속도가 원래의 중력값보다 작다면 떨어지고 있는 상태임
        if (!stateMachine.Player.CharController.isGrounded 
            && stateMachine.Player.CharController.velocity.y < Physics.gravity.y * Time.fixedDeltaTime
            && stateMachine.Player.InputController.CheckFallHeight())
        {
            stateMachine.ChangeMovementState(stateMachine.FallState);
        }
    }

    

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // 원래 방향키 입력이 없었다면 실행하지 않음
        if (stateMachine.MovementInput == Vector2.zero) return;

        // 기존에 입력이 있었다면 Idle로 전환
        stateMachine.ChangeMovementState(stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }
}
