using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] Image shopItemIcon;
    [SerializeField] Button shopItemBtn;

    ItemData itemData;

    private void Start()
    {
        shopItemBtn.onClick.AddListener(SelectShopItem);
    }

    public void SetShopItem(ItemData data)
    {
        itemData = data;

        if (shopItemIcon == null)
        {
            Debug.LogAssertion("iconImage가 연결되지 않음");
            return;
        }

        if (itemData.icon == null)
        {
            Debug.LogAssertion("itemData.icon 없음");
        }
        else
        {
            shopItemIcon.sprite = itemData.icon;
        }
    }

    private void SelectShopItem()
    {
        if (itemData != null)
        {
            UIManager.Instance.ShopUI.ShowDescription(itemData);
        }
    }
}
