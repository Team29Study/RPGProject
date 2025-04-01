using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEditor.Progress;

public class EquipmentModel
{
    private EquipmentContainer[] Equips { get; }

    public event Action<EquipmentContainer> OnEquipped = delegate { };
    public event Action<EquipmentContainer> OnUnEquipped = delegate { };

    public EquipmentContainer this[EquipmentType type] => Equips[(int)type];

    public EquipmentModel(int capacity = (int)EquipmentType.Count, IList<EquipmentContainer> initialEquips = null)
    {
        Equips = new EquipmentContainer[capacity];

        if (initialEquips != null)
        {
            initialEquips.Take(capacity).ToArray().CopyTo(Equips, 0);
        }
    }

    public void Equipped(EquipmentType type, EquipmentContainer container)
    {
        if (container != null)
        {
            UnEquipped(type);

            Equips[(int)type] = container;
            OnEquipped?.Invoke(Equips[(int)type]);
        }
    }

    public void UnEquipped(EquipmentType type)
    {
        if (Equips[(int)type] != null)
        {
            OnUnEquipped?.Invoke(Equips[(int)type]);
        }

        Equips[(int)type] = default;
    }

    public class EquipmentContainer
    {
        public EquipmentItemData Data { get; }
        public StatModifier<PlayerStatType>[] Modifiers { get; }

        public EquipmentContainer(EquipmentItemData data)
        {
            Data = data;
            Modifiers = new StatModifier<PlayerStatType>[data.stats.Count];

            for (int i = 0; i < data.stats.Count; i++)
            {
                var source = data.stats[i];

                Modifiers[i] = new StatModifier<PlayerStatType>(
                    source.modifierStatType,
                    source.operationType == OperationType.Add ? 
                    new AddOperation(source.statValue) : 
                    new MultiplyOperation(source.statValue)
);
            }
        }

        public void Release()
        {
            for (int i = 0; i < Modifiers.Length; i++)
            {
                Modifiers[i].Dispose();
            }
        }
    }
}
