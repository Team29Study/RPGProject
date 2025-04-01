using System.Collections.Generic;

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
        view.InitializeView(capacity);

        model.OnValueChanged += OnModelChanged;

        view.OnSlotSwap += OnViewSlotSwapped;
        view.OnUseItem += OnViewUseItem;

        RefreshView();
    }

    private void OnModelChanged(IList<Item> items)
    {
        RefreshView();
    }

    private void OnViewSlotSwapped(int source, int target)
    {
        //Combine 추가해야됨
        model.Swap(source, target);
    }

    private void OnViewUseItem(int index)
    {
        var item = model.GetItemAt(index);

        if (item != null && item.usableCondition.Evaluate())
        {
            model.UseByIndex(index);
            model.RemoveItemByIndex(index, 1);
        }
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
