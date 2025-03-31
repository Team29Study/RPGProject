using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] Sprite shopItemIcon;
    [SerializeField] Button shopItemBtn;

    [SerializeField] ItemData itemData;

    private void Start()
    {
        shopItemBtn.onClick.AddListener(SelectShopItem);
    }

    public void SetSlot(ItemData data)
    {
        itemData = data;
        shopItemIcon = itemData.icon;
    }

    private void SelectShopItem()
    {
        if (shopItemIcon != null)
        {
            UIManager.Instance.ShopUI.ShowDescription(itemData);
        }
        else
        {
            Debug.LogAssertion("Empty");
        }
    }
}
