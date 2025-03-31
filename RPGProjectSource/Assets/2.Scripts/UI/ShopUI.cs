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
        // UIManager에 ShopUI 연결
        UIManager.Instance.SetShopUI(this);

        closeBtn.onClick.AddListener(CloseShop);
        buyBtn.onClick.AddListener(Buy);
        sellBtn.onClick.AddListener(Sell);

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

    public void ShowDescription(ItemData itemdata)
    {
        description.text = $"{itemdata.description}";
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
