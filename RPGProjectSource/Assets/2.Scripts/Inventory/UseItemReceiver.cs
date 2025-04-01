using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseItemReceiver : MonoBehaviour
{
    [Header("Debug")]
    public Inventory inventory;

    public UnityEvent<EquipmentItemData> OnUseEquipmentItem;
    public UnityEvent<ConsumableItemData> OnUseConsumableItem;

    void Start() => inventory.Model.OnUseItem += UseItem;

    private void UseItem(Item item)
    {
        if (item == null || item.itemData == null)
            return;

        switch (item.itemData)
        {
            case EquipmentItemData equipment:
                OnUseEquipmentItem?.Invoke(equipment);
                break;

            case ConsumableItemData consumable:
                OnUseConsumableItem?.Invoke(consumable);
                break;
            default: break;
        }
    }
}
