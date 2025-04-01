using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private TextMeshProUGUI description;

    [SerializeField] private Transform slot;
    [SerializeField] private ShopSlot slotPrefab;
    
    public List<ShopSlot> shopSlots = new();
    public List<ItemData> shopItemDatas = new();

    private void Start()
    {
        Debug.Log($"shopItemDatas.Count = {shopItemDatas.Count}");
        for (int i = 0; i < shopItemDatas.Count; i++)
        {
            Debug.Log($"[{i}] 아이템 이름: {shopItemDatas[i]?.itemName}");
        }

        // UIManager에 ShopUI 연결
        UIManager.Instance.SetShopUI(this);

        closeBtn.onClick.AddListener(CloseShop);
        buyBtn.onClick.AddListener(Buy);
        sellBtn.onClick.AddListener(Sell);
        Debug.Log("InitShopSlot 호출됨");
        InitShopSlot();
    }

    private void InitShopSlot()
    {
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }
        shopSlots.Clear();

        for (int i = 0; i < shopItemDatas.Count; i++)
        {
            ShopSlot newSlot = GameObject.Instantiate(slotPrefab, slot);

            if (i < shopItemDatas.Count)
            {
                Debug.Log($"SetShopItem 호출: {shopItemDatas[i].itemName}");
                newSlot.SetShopItem(shopItemDatas[i]);
            }


            shopSlots.Add(newSlot);
        }
    }

    public void SetData()
    {
        new ItemData
        {
            
        };
    }

    public void UpdateShopSlot()
    {
        for (int i = 0; shopSlots.Count > i; i++)
        {
            if (i < shopItemDatas.Count)
            {
                shopSlots[i].SetShopItem(shopItemDatas[i]);
            }
            else
            {
                shopSlots.Add(null);
            }
        }
    }

    public void ShowDescription(ItemData itemData)
    {
        description.text = $"{itemData.itemName}\n{itemData.description}\n{itemData.itemPrice}";
    }

    private void Buy()
    {

    }

    private void Sell()
    {

    }

    private void CloseShop()
    {
        UIManager.Instance.ShopUI.gameObject.SetActive(false);
    }
}
