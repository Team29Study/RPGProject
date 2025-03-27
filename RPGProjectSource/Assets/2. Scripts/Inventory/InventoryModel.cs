using UnityEngine;
using System;
using System.Linq;

public class InventoryModel
{
    private ObserverArray<Item> Items { get; }
    private readonly int capacity;

    public event Action<Item[]> OnValueChanged
    {
        add => Items.AnyValueChanged += value;
        remove => Items.AnyValueChanged -= value;
    }

    public InventoryModel(int capacity)
    {
        this.capacity = capacity;
        Items = new(capacity);
    }

    public int Count() => Items.Count;

    public Item Get(int index) => Items[index];

    public bool TryAdd(ItemData item, int quantity)
    {
        if (CanAdd(item, quantity))
        {
            AddItem(item, quantity);
        }
        return false;
    }

    public bool CanAdd(ItemData data, int quantity = 1)
    {
        int remaining = quantity;

        remaining -= Items.items.Where(i => i?.itemData == data).Sum(i => Mathf.Min(data.maxStack -
            i.quantity, remaining));

        if (remaining <= 0) return true;

        int emptySlotCount = Items.items.Count(i => i == null);
        return remaining <= emptySlotCount * data.maxStack;
    }

    public void RemoveAt(int index) => Items.TryRemoveAt(index);

    public bool TryRemove(ItemData item, int quantity)
    {
        return false;
    }

    public void Clear() => Items.Clear();

    public void Swap(int source, int target) => Items.Swap(source, target);

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

    private void Invoke() => Items.Invoke();
}
