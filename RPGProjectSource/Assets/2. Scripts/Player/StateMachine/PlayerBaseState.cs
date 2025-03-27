using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 공통된 기능을 가진 BaseState
public class PlayerBaseState : IPlayerState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        Move();
    }
    
    // 모든 상태에 필요한 것들
    // 애니메이션 전환
    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }
    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    // 움직이는 입력값을 받음
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.InputController.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        Rotate(movementDirection);
    }

    // 움직일 방향을 구해주는 함수
    private Vector3 GetMovementDirection()
    {
        // 메인 카메라의 forward, right 방향을 받아옴
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        // forward, right의 y방향을 없앰
        forward.y = 0;
        right.y = 0;

        // 단위 벡터로 만들어 줌
        forward.Normalize();
        right.Normalize();

        // 입력받은 값에 방향을 곱해줌
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.Player.CharController.Move((direction * movementSpeed) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        return stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
    }

    // 캐릭터 회전
    private void Rotate(Vector3 direction)
    {
        // 방향키 입력이 있다면
        if(direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }
}
