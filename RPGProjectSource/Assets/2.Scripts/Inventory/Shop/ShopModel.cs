using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopModel
{
    private readonly IList<ItemData> sellingList;

    public ItemData this[int index] => sellingList[index];

    public ShopModel(IList<ItemData> sellingDates)
    {
        sellingList = sellingDates.ToList();
    }
}