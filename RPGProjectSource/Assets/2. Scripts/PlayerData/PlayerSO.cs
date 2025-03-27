using System;
using UnityEngine;

// �÷��̾��� �̵��� ���Ǵ� ������
[Serializable]
public class PlayerGroundData
{
    // �÷��̾��� �⺻ �ӵ�
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;

    // �⺻ ȸ�� �ӵ�
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    // �⺻ �ӵ����� ������ ��
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

    [field:Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}

//[Serializable]
//public class PlayerAttackData
//{
//    [field:Header("")]
//}

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field:SerializeField]public PlayerGroundData GroundData { get; private set; }
}
