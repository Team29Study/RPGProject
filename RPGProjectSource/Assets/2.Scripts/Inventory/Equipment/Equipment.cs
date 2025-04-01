using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Equipment : MonoBehaviour
{
    [SerializeField] private EquipmentView view;

    public UnityEvent<EquipmentModel.EquipmentContainer> OnEquipped;
    public UnityEvent<EquipmentModel.EquipmentContainer> OnUnEquipped;

    public UnityEvent<StatModifier<PlayerStatType>> OnAddModifier;

    private EquipmentPresenter presenter;

    public EquipmentModel Model => presenter.model;

    void Awake()
    {
        presenter = new EquipmentPresenter.Builder(view).Build();
    }

    void OnEnable()
    {
        Model.OnEquipped += InvokeEquip;
        Model.OnUnEquipped += InvokeUnEquip;
    }

    private void OnDisable()
    {
        Model.OnEquipped -= InvokeEquip;
        Model.OnUnEquipped -= InvokeUnEquip;
    }

    public void Equipped(EquipmentItemData equipData)
    {
        Model.Equipped(equipData.equipmentType, new EquipmentModel.EquipmentContainer(equipData));
    }

    private void InvokeEquip(EquipmentModel.EquipmentContainer val)
    {
        OnEquipped.Invoke(val);

        if (val != null && val.Modifiers.Length != 0)
        {
            for (int i = 0; i < val.Modifiers.Length; i++)
            {
                InvokeModifier(val.Modifiers[i]);
            }
        }
    }

    private void InvokeUnEquip(EquipmentModel.EquipmentContainer val)
    {
        OnUnEquipped.Invoke(val);

        //임의로 추가
        val.Release();
    }

    private void InvokeModifier(StatModifier<PlayerStatType> modifier)
    {
        OnAddModifier?.Invoke(modifier);
    }
}
