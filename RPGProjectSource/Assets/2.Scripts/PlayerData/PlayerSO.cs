using JetBrains.Annotations;
using System;
using System.Collections.Generic;
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

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfoData(int index) { return AttackInfoDatas[index]; }
}

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; private set; }
    // 다음 공격 인덱스
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    // 콤보 전환 시간
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field:Range(0f, 3f)]public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}

[Serializable]
public class PlayerStatData
{
    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public int Def { get; set; }
}

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field:SerializeField]public PlayerGroundData GroundData { get; private set; }
    [field:SerializeField] public PlayerAttackData AttackData { get; private set; }
    [field: SerializeField] public PlayerStatData StatData { get; set; }
}
