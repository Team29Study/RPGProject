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
    
    List<ShopSlot> shopSlots = new List<ShopSlot>();
    List<ItemData> shopItemDatas;

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
        // 기존 슬롯 제거
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
                newSlot.SetSlot(shopItemDatas[i]);
            }
            else
            {
                newSlot.gameObject.SetActive(true); // 또는 빈 슬롯용 처리
            }
            shopSlots.Add(newSlot);
        }
    }

    public void SetData()
    {

    }

    private void CloseShop()
    {
        UIManager.Instance.ShopUI.gameObject.SetActive(false);
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
}
