using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Equipment : MonoBehaviour
{
    [SerializeField] private EquipmentView view;

    public UnityEvent<EquipmentItemData> OnEquipped;
    public UnityEvent<EquipmentItemData> OnUnEquipped;

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

    //외부 접근용
    public void Equipped(EquipmentItemData equipData)
    {
        Model.Equipped(equipData.equipmentType, new EquipmentModel.EquipmentContainer(equipData));
    }

    private void InvokeEquip(EquipmentModel.EquipmentContainer val)
    {
        OnEquipped.Invoke(val.Data);

        if (val != null && val.Modifiers.Length != 0)
        {
            for (int i = 0; i < val.Modifiers.Length; i++)
            {
                InvokeAddModifier(val.Modifiers[i]);
            }
        }
    }

    private void InvokeUnEquip(EquipmentModel.EquipmentContainer val)
    {
        OnUnEquipped.Invoke(val.Data);

        //임의로 추가
        val.Release();
    }

    private void InvokeAddModifier(StatModifier<PlayerStatType> modifier)
    {
        OnAddModifier?.Invoke(modifier);
    }
}
