using System;
using UnityEngine;

// 플레이어의 이동에 사용되는 데이터
[Serializable]
public class PlayerGroundData
{
    // 플레이어의 기본 속도
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;

    // 기본 회전 속도
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    // 기본 속도에서 곱해줄 값
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
