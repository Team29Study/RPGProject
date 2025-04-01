using System.Collections;
using System.Collections.Generic;

public class EquipmentPresenter
{
    public readonly EquipmentModel model;
    public readonly EquipmentView view;

    public EquipmentPresenter(EquipmentModel model, EquipmentView view)
    {
        this.model = model;
        this.view = view;

        Initialize();
    }

    private void Initialize()
    {
        model.OnEquipped += OnModelEquipped;
        model.OnUnEquipped += OnModelUnEquipped;

        view.InitializeView();
    }

    private void OnModelEquipped(EquipmentModel.EquipmentContainer equipInfo)
    {
        view.Slots[(int)equipInfo.Data.equipmentType].Set(equipInfo.Data.icon);
    }

    private void OnModelUnEquipped(EquipmentModel.EquipmentContainer equipInfo)
    {
        view.Slots[(int)equipInfo.Data.equipmentType].Set(null);
    }

    private void OnViewUnEquippedItem(int index)
    {
        model.UnEquipped((EquipmentType)index);
    }

    public class Builder
    {
        private EquipmentView view;
        private IList<EquipmentModel.EquipmentContainer> initialEquips;

        public Builder(EquipmentView view)
        {
            this.view = view;
        }

        public Builder WithInitialEquips(IList<EquipmentModel.EquipmentContainer> initialEquips)
        {
            this.initialEquips = initialEquips;

            return this;
        }

        public EquipmentPresenter Build()
        {
            return new EquipmentPresenter(new EquipmentModel(initialEquips: initialEquips ?? null), view);
        }
    }

}
