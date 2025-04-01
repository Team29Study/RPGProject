using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public event Action<EquipmentItemData> OnEquipped = delegate { };
    public event Action<EquipmentItemData> OnUnEquipped = delegate { };

    void Awake()
    {

    }
}

public class EquipmentModel
{
    private ObserverArray<EquipmentItemData> Equips { get; }

    public event Action<EquipmentItemData[]> OnValueChanged
    {
        add => Equips.AnyValueChanged += value;
        remove => Equips.AnyValueChanged += value;
    }

    public EquipmentItemData this[SampleEquipmentType type] => Equips[(int)type];

    public EquipmentModel()
    {
        Equips = new((int)SampleEquipmentType.Count);
    }

    public EquipmentModel(int capacity, IList<EquipmentItemData> initialEquips)
    {
        Equips = new(capacity, initialEquips);
    }

    public void Set(SampleEquipmentType type, EquipmentContainer container)
    {

    }

    public class EquipmentContainer
    {
        public EquipmentItemData Data { get; }
        public IOperationStrategy OperationStrategy { get; }

        public EquipmentContainer(EquipmentItemData data, IOperationStrategy operationStrategy)
        {
            Data = data;
            OperationStrategy = operationStrategy;
        }
    }
}

public class EquipmentPresenter
{

}