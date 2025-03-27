using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        view.OnSlotEvent += OnViewSlotEvent;
    }

    private void OnModelChanged(IList<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Item item = model.Get(i);

            view.Slots[i].Set(item);
        }
    }

    private void OnViewSlotEvent(Slot source, Slot target)
    {

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
