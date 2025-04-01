using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Item Details/Equipment")]
public class EquipmentItemData : ItemData
{
    [Header("Equipment Setting")]
    public EquipmentType equipmentType;
    public List<EquipmentStat> stats = new();

    [Serializable]
    public class EquipmentStat
    {
        public PlayerStatType modifierStatType;
        public OperationType operationType;
        public float statValue;
    }
}

public enum OperationType
{
    Add,
    Multiply
}

public enum EquipmentType
{
    Weapon,
    Armor,
    Head,

    Count
}