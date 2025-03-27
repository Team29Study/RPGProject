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

    void Awake()
    {
        presenter = new InventoryPresenter.Builder(view).WithCapacity(maxCapacity).Build();
    }
}
