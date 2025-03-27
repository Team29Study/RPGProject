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

    [SerializeField, Header("Debug")]
    private ItemData[] __debugItems;

    void Awake()
    {
        presenter = new InventoryPresenter.Builder(view).WithCapacity(maxCapacity).Build();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            presenter.model.TryAdd(__debugItems[Random.Range(0, __debugItems.Length)], 6);
        }
    }
}
