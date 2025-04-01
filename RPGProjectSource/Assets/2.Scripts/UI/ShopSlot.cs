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
        Debug.Log("✅ SetShopItem 호출됨");

        itemData = data;

        if (shopItemIcon == null)
        {
            Debug.LogError("❌ iconImage가 연결되어 있지 않습니다.");
            return;
        }

        if (itemData.icon == null)
        {
            Debug.LogWarning("⚠️ itemData.icon이 null입니다.");
        }
        else
        {
            Debug.Log($"🎯 아이콘 설정됨: {itemData.icon.name}");
            shopItemIcon.sprite = itemData.icon;
        }
    }


    private void SelectShopItem()
    {
        if (itemData != null)
        {
            UIManager.Instance.ShopUI.ShowDescription(itemData);
        }
        else
        {
            Debug.LogAssertion("Empty");
        }
    }
}
