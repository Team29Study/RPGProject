using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity = 10;
    [SerializeField] private InventoryView view;

    private InventoryPresenter presenter;

    public InventoryModel Model => presenter.model;

    [SerializeField, Header("Debug")]
    private ItemData[] __debugItems;
    [SerializeField]
    private ItemData __removeItem;

    void Awake()
    {
        presenter = new InventoryPresenter.Builder(view).WithCapacity(maxCapacity).Build();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            presenter.model.TryAdd(__debugItems[Random.Range(0, __debugItems.Length)], 1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            presenter.model.TryRemove(__removeItem, 1);
        }
    }

    public void DebugAddOneItem(EquipmentModel.EquipmentContainer item)
    {
        Model.TryAdd(item.Data, 1);
    }

    public void AddOneItem(ItemData item)
    {
        Model.TryAdd(item, 1);
    }
}
