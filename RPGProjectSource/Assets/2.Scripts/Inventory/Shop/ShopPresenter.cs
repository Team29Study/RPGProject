using System.Collections.Generic;

public class ShopPresenter
{
    private readonly ShopModel model;
    private readonly ShopView view;
    private readonly int capacity;
    private InventoryModel inventory;

    private ShopPresenter(ShopModel model, ShopView view, int capacity)
    {
        this.model = model;
        this.view = view;
        this.capacity = capacity;

        Initialize();
    }

    private void Initialize()
    {
        view.InitializeView(capacity);
    }

    private void OnModelChanged()
    {
        //판매 품목이 없어졌거나, 남은 골드
    }

    private void OnViewBuyItem(int index)
    {
        if (inventory == null)
            return;

        inventory.TryAdd(model[index], 1);
    }

    private void OnViewSellItem(int index)
    {

    }

    public class Builder
    {
        private ShopView view;
        private int capacity;
        private IList<ItemData> itemDatas;

        public Builder(ShopView view)
        {
            this.view = view;
        }

        public Builder WithSellingList(IList<ItemData> itemDatas)
        {
            this.itemDatas = itemDatas;
            this.capacity = itemDatas.Count;

            return this;
        }

        public Builder WithInventory(InventoryModel model)
        {

            return this;
        }

        public ShopPresenter Build()
        {
            return new ShopPresenter(new ShopModel(itemDatas), view, itemDatas.Count);
        }
    }
}