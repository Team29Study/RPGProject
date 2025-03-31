using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    // 플레이어 상태들이 받아갈 값들 세팅
    public Player Player { get; }
    
    // 공격 중인지 체크
    public bool IsAttacking { get; set; }
    // 몇 번째 공격이 진행 중인지
    public int ComboIndex { get; set; }

    // TODO : 적절한 위치가 어디인지 고민한번 해보자
    // 움직임 입력 값
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCamTransform { get; set; }

    // 플레이어 상태들
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }

    public PlayerFallState FallState { get; private set; }

    public PlayerComboAttackState ComboAttackState { get; private set; }

    // 클래스 초기화
    public PlayerStateMachine(Player player)
    {
        Player = player;

        MainCamTransform = Camera.main.transform;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        FallState = new PlayerFallState(this);

        ComboAttackState = new PlayerComboAttackState(this);

        MovementSpeed = Player.Data.GroundData.BaseSpeed;
        RotationDamping = Player.Data.GroundData.BaseRotationDamping;

    }
}
