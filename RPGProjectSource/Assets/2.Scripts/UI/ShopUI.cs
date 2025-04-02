using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : PopUpUI
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Buy")]
    [SerializeField] private Transform buyContent;
    [Header("Sell")]
    [SerializeField] private Transform sellContent;

    [Header("Reference")]
    [SerializeField] private ShopSlot slotPrefab;

    private List<ShopSlot> buySlotsCache = new List<ShopSlot>();
    private List<ShopSlot> sellSlotsCache = new List<ShopSlot>();

    private ItemData targetBuyItems = null;
    private ItemData targetSellItems = null;

    private void Start()
    {
        UIManager.Instance.RegisterPopUp(this);
        Close();
    }

    public void InitializeSellingDatas(IList<ItemData> itemDatas, Action<Inventory> action = null)
    {
        sellSlotsCache?.ForEach(e => Destroy(e.gameObject));
        sellSlotsCache?.Clear();

        for (int i = 0; i < itemDatas.Count; i++)
        {
            int indexCache = i;

            var slot = Instantiate(slotPrefab, sellContent);
            slot.gameObject.SetActive(true);
            slot.Set(itemDatas[i].icon);
            slot.OnButtonClicked += (val) =>
            {
                description.gameObject.SetActive(true);
                description.text = itemDatas[indexCache].description;
                targetSellItems = itemDatas[indexCache];
            };

            sellSlotsCache.Add(slot);
        }
    }

    public void InitializeBuyDatas(IList<ItemData> itemDatas, Inventory inventory = null)
    {
        buySlotsCache?.ForEach(e => Destroy(e.gameObject));
        buySlotsCache?.Clear();

        Debug.Log("a");

        for (int i = 0; i < itemDatas.Count; i++)
        {
            int indexCache = i;

            var slot = Instantiate(slotPrefab, buyContent);
            slot.gameObject.SetActive(true);
            slot.Set(itemDatas[i].icon);
            slot.OnButtonClicked += (val) =>
            {
                description.gameObject.SetActive(true);
                description.text = itemDatas[indexCache].description;
                targetBuyItems = itemDatas[indexCache];
            };        

            buySlotsCache.Add(slot);
        }
    }

    //private void InitShopSlot()
    //{
    //    // 기존 슬롯들 삭제
    //    foreach (Transform child in slot)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //    shopSlots.Clear(); // 슬롯 리스트 초기화

    //    // 새로운 슬롯들을 생성하여 상점에 추가
    //    for (int i = 0; i < shopItemDatas.Count; i++)
    //    {
    //        ShopSlot newSlot = GameObject.Instantiate(slotPrefab, slot);

    //        if (i < shopItemDatas.Count)
    //        {
    //            newSlot.SetShopItem(shopItemDatas[i]); // 아이템 데이터 설정
    //        }

    //        shopSlots.Add(newSlot); // 슬롯 리스트에 추가
    //    }
    //}

    //private void InitSellSlot()
    //{
    //    // 기존 슬롯들 삭제
    //    foreach (Transform child in sSlot)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //    sellSlots.Clear(); // 슬롯 리스트 초기화

    //    // 새로운 슬롯들을 생성하여 상점에 추가
    //    for (int i = 0; i < invenItemDatas.Count; i++)
    //    {
    //        SellSlot newSlot = GameObject.Instantiate(sellSlotPrefab, sSlot);

    //        if (i < invenItemDatas.Count)
    //        {
    //            newSlot.SetSellItem(invenItemDatas[i]); // 아이템 데이터 설정
    //        }

    //        sellSlots.Add(newSlot); // 슬롯 리스트에 추가
    //    }
    //}

    //public void ShowDescription(ItemData itemData)
    //{
    //    // 아이템 설명을 UI에 표시
    //    description.text = $"{itemData.itemName}\n{itemData.description}\n가격: {itemData.itemPrice}G";
    //}

    //public void ShowSellDescription(ItemData itemData)
    //{
    //    // 아이템 설명을 UI에 표시
    //    description.text = $"{itemData.itemName}\n{itemData.description}\n판매가: {itemData.itemPrice / 2}G";
    //}

    //public void SelectSlot(ShopSlot slot)
    //{
    //    shopSlot = slot; // 슬롯 선택
    //}

    //public void SelectSellSlot(SellSlot slot)
    //{
    //    sellSlot = slot;
    //}

    //private void Buy()
    //{
    //    // 선택하지 않은 경우
    //    if (shopSlot == null)
    //    {
    //        return;
    //    }

    //    ItemData itemData = shopSlot.GetItemData();
    //    if (itemData != null)
    //    {
    //        invenItemDatas.Add(itemData);
    //    }

    //    shopSlot.SoldOut(true); // 아이템 구매 시 아이템 품절 처리
    //    description.text = null; // 아이템 설명 초기화
    //    shopSlot = null;

    //    InitSellSlot();
    //}

    //private void Sell()
    //{
    //    // 선택하지 않은 경우
    //    if (sellSlot == null)
    //    {
    //        return;
    //    }

    //    ItemData itemData = sellSlot.GetItemData();
    //    if (itemData != null)
    //    {
    //        invenItemDatas.Remove(itemData);
    //    }

    //    description.text = null; // 아이템 설명 초기화
    //    sellSlot = null;

    //    InitSellSlot();
    //}
}
