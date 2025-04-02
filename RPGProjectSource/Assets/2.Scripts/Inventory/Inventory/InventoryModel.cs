using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class InventoryModel
{
    private ObserverArray<Item> Items { get; }
    private readonly int capacity;

    public int Gold { get; private set; }

    public event Action<Item[]> OnValueChanged
    {
        add => Items.AnyValueChanged += value;
        remove => Items.AnyValueChanged -= value;
    }

    public event Action<Item> OnUseItem = delegate { };

    public InventoryModel(int capacity)
    {
        this.capacity = capacity;
        Items = new(capacity);
    }

    public InventoryModel(int capacity, IList<Item> initialItems)
    {
        this.capacity = capacity;
        Items = new(capacity, initialItems);
    }

    public int Count() => Items.Count;

    public int GetItemCount() => Items.items.Count(x => x != default);

    public Item GetItemAt(int index) => Items[index];


    public ItemData GetItemDataAt(int index) => Items[index]?.itemData;

    public bool TryAdd(ItemData item, int quantity)
    {
        if (CanAdd(item, quantity))
        {
            AddItem(item, quantity);
        }
        return false;
    }

    public bool CanAdd(ItemData item, int quantity = 1)
    {
        int remaining = quantity;

        remaining -= Items.items.Where(i => i?.itemData == item).Sum(i => Mathf.Min(item.maxStack -
            i.quantity, remaining));

        if (remaining <= 0) return true;

        int emptySlotCount = Items.items.Count(i => i == null);
        return remaining <= emptySlotCount * item.maxStack;
    }

    public void RemoveAt(int index) => Items.TryRemoveAt(index);

    public bool TryRemove(ItemData item, int quantity)
    {
        if (CanRemove(item, quantity))
        {
            RemoveItem(item, quantity);
        }
        return false;
    }

    public bool CanRemove(ItemData item, int quantity) => ItemCount(item) >= quantity;

    public int ItemCount(ItemData item) => Items.items.Where(i => i != null && i.itemData == item).Sum(i => i.quantity);

    public void Clear() => Items.Clear();

    public void Swap(int source, int target) => Items.Swap(source, target);

    public bool RemoveItemByIndex(int index, int quantity)
    {
        if (index < 0 || index >= Count()) return false;

        if (Items[index].quantity < quantity) return false;

        Items[index].quantity -= quantity;

        if (Items[index].quantity == 0) RemoveAt(index);

        Invoke();
        return true;
    }

    public bool UseByIndex(int index)
    {
        OnUseItem?.Invoke(Items[index]);

        return true;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }

    public bool TryUsedGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }

        return false;
    }

    #region internal method
    private void AddItem(ItemData data, int quantity)
    {
        int remaining = quantity;

        foreach (var iter in Items.items.Where(i => i?.itemData == data && i.quantity < data.maxStack))
        {
            int toAdd = Math.Min(data.maxStack - iter.quantity, remaining);
            iter.quantity += toAdd;
            remaining -= toAdd;

            if (remaining <= 0)
            {
                Invoke();
                return;
            }
        }

        foreach (int i in Enumerable.Range(0, Items.Count).Where(i => Items[i] == null))
        {
            int toAdd = Math.Min(data.maxStack, remaining);
            Items.TryAdd(data.Create(toAdd));
            remaining -= toAdd;

            if (remaining <= 0) return;
        }

        Invoke();
    }

    private void RemoveItem(ItemData item, int quantity)
    {
        int remaining = quantity;

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            var current = Items[i];

            if (current != null && current.itemData == item)
            {
                if (current.quantity <= remaining)
                {
                    remaining -= current.quantity;
                    RemoveAt(i);
                }
                else
                {
                    current.quantity -= remaining;
                    Invoke();
                    return;
                }
            }

            if (remaining <= 0)
                return;
        }
    }

    private void Invoke() => Items.Invoke();
    #endregion
}