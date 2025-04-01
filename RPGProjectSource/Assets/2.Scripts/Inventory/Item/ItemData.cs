using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemsDetails")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    [TextArea]
    public string description;
    public int itemPrice;
    public int maxStack = 1;

    public virtual Item Create(int quantity = 1)
    {
        return new Item(this, quantity);
    }
}
