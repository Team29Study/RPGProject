using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Item Details/Equipment")]
public class EquipmentItemData : ItemData
{
    [Header("Equipment Setting")]
    public SampleEquipmentType equipmentType;
    public List<EquipmentStat> stats = new();

    [Serializable]
    public class EquipmentStat
    {
        public SampleStat modifierStatType;
        public SampleOperationType operationType;
        public float statValue;
    }
}

public enum SampleStat
{
    AttackPower,
    Defensive
}

public enum SampleOperationType
{
    Add,
    Multiply
}

public enum SampleEquipmentType
{
    Weapon,

    Count
}