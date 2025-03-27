using UnityEngine;

public enum ItemType
{
    Equippable,
    Consumable,
    Other
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;
    [SerializeField] private ItemType type;
    [SerializeField] private string description;
}
