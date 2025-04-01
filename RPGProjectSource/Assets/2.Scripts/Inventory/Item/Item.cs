using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public readonly ItemData itemData;
    public int quantity;
    public IPredicate usableCondition;

    public Item(ItemData itemData, int quantity = 1)
    {
        usableCondition = new FuncPredicate(() => true);

        this.itemData = itemData;
        this.quantity = quantity;
    }
}

public interface IPredicate
{
    bool Evaluate();
}

public class FuncPredicate : IPredicate
{
    public readonly Func<bool> func;

    public FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate() => func.Invoke();
}