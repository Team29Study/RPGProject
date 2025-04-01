using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] Image shopItemIcon;
    [SerializeField] Image soldOutPanel;
    [SerializeField] Button shopItemBtn;

    private ItemData itemData;

    private void Start()
    {
        shopItemBtn.onClick.AddListener(SelectShopItem);
    }

    public void SetShopItem(ItemData data)
    {
        itemData = data;

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

    public void SoldOut(bool isSoldOut)
    {
        if (isSoldOut)
        {
            shopItemIcon = soldOutPanel;
        }
    }
}
