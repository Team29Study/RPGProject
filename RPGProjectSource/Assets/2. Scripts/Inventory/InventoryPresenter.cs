using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryPresenter
{
    //Test
    public readonly InventoryModel model;
    private readonly InventoryView view;
    private readonly int capacity;

    private InventoryPresenter(InventoryModel model, InventoryView view, int capacity)
    {
        this.model = model;
        this.view = view;
        this.capacity = capacity;

        Initialize();

        //connected model & view
    }

    private void Initialize()
    {
        view.InitializeView(model);

        model.OnValueChanged += OnModelChanged;
        view.OnSlotSwap += OnViewSlotSwapped;

        RefreshView();
    }

    private void OnModelChanged(IList<Item> items)
    {
        RefreshView();
    }

    private void OnViewSlotSwapped(int index1, int index2)
    {
        //Combine 추가해야됨
        model.Swap(index1, index2);
    }

    private void RefreshView()
    {
        for (int i = 0; i < capacity; i++)
        {
            var item = model.GetItemAt(i);

            view.Slots[i].Set(item?.itemData.icon ?? null, item?.quantity ?? 0);
        }
    }

    public class Builder
    {
        private InventoryView view;
        private int capacity;

        public Builder(InventoryView view)
        {
            this.view = view;
        }

        public Builder WithCapacity(int capacity)
        {
            this.capacity = capacity;
            return this;
        }

        public InventoryPresenter Build()
        {
            return new InventoryPresenter(new InventoryModel(capacity), view, capacity);
        }
    }
}
