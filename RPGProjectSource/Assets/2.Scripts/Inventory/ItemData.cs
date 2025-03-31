using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemsDetails")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName = "New Item";
    [TextArea]
    public string description;
    public int maxStack = 1;

    public Item Create(int quantity = 1)
    {
        return new Item(this, quantity);
    }
}
