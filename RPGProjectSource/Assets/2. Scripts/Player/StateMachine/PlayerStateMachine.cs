using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    // �÷��̾� ���µ��� �޾ư� ���� ����
    public Player Player { get; }
    
    // TODO : ������ ��ġ�� ������� ����ѹ� �غ���
    // ������ �Է� ��
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Transform MainCamTransform { get; set; }

    // �÷��̾� ���µ�
    public PlayerIdleState IdleState { get; set; }

    // Ŭ���� �ʱ�ȭ
    public PlayerStateMachine(Player player)
    {
        Player = player;

        MainCamTransform = Camera.main.transform;

        MovementSpeed = Player.Data.GroundData.BaseSpeed;
        RotationDamping = Player.Data.GroundData.BaseRotationDamping;

        IdleState = new PlayerIdleState(this);
    }
}
