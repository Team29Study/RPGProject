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
    [SerializeField] private Transform slot;
    [SerializeField] private ShopSlot slotPrefab;

    [Header("Sell")]
    [SerializeField] private Transform sSlot;
    [SerializeField] private SellSlot sellSlotPrefab;

    public List<ShopSlot> shopSlots = new(); // 상점 UI에 표시될 아이템 슬롯 리스트
    public List<ItemData> shopItemDatas = new(); // 상점에 판매되는 아이템 데이터 리스트

    public List<SellSlot> sellSlots = new(); // 판매 목록 아이템 슬롯 리스트
    public List<ItemData> invenItemDatas = new(); // 인벤토리 아이템 리스트 (임시)

    private ShopSlot shopSlot;
    private SellSlot sellSlot;

    private void Start()
    {
        UIManager.Instance.RegisterPopUp(this);
        Close();

        closeBtn.onClick.AddListener(Close); // 상점 닫기 버튼에 리스너 추가
        buyBtn.onClick.AddListener(Buy); // 아이템 구매 버튼에 리스너 추가
        sellBtn.onClick.AddListener(Sell); // 아이템 판매 버튼에 리스너 추가

        InitShopSlot(); // 상점 슬롯 초기화
        InitSellSlot();
        description.text = null;
    }

    private void InitShopSlot()
    {
        // 기존 슬롯들 삭제
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }
        shopSlots.Clear(); // 슬롯 리스트 초기화

        // 새로운 슬롯들을 생성하여 상점에 추가
        for (int i = 0; i < shopItemDatas.Count; i++)
        {
            ShopSlot newSlot = GameObject.Instantiate(slotPrefab, slot);

            if (i < shopItemDatas.Count)
            {
                newSlot.SetShopItem(shopItemDatas[i]); // 아이템 데이터 설정
            }

            shopSlots.Add(newSlot); // 슬롯 리스트에 추가
        }
    }

    private void InitSellSlot()
    {
        // 기존 슬롯들 삭제
        foreach (Transform child in sSlot)
        {
            Destroy(child.gameObject);
        }
        sellSlots.Clear(); // 슬롯 리스트 초기화

        // 새로운 슬롯들을 생성하여 상점에 추가
        for (int i = 0; i < invenItemDatas.Count; i++)
        {
            SellSlot newSlot = GameObject.Instantiate(sellSlotPrefab, sSlot);

            if (i < invenItemDatas.Count)
            {
                newSlot.SetSellItem(invenItemDatas[i]); // 아이템 데이터 설정
            }

            sellSlots.Add(newSlot); // 슬롯 리스트에 추가
        }
    }

    public void ShowDescription(ItemData itemData)
    {
        // 아이템 설명을 UI에 표시
        description.text = $"{itemData.itemName}\n{itemData.description}\n가격: {itemData.itemPrice}G";
    }

    public void ShowSellDescription(ItemData itemData)
    {
        // 아이템 설명을 UI에 표시
        description.text = $"{itemData.itemName}\n{itemData.description}\n판매가: {itemData.itemPrice / 2}G";
    }

    public void SelectSlot(ShopSlot slot)
    {
        shopSlot = slot; // 슬롯 선택
    }

    public void SelectSellSlot(SellSlot slot)
    {
        sellSlot = slot;
    }

    private void Buy()
    {
        // 선택하지 않은 경우
        if (shopSlot == null)
        {
            return;
        }

        ItemData itemData = shopSlot.GetItemData();
        if (itemData != null)
        {
            invenItemDatas.Add(itemData);
        }

        shopSlot.SoldOut(true); // 아이템 구매 시 아이템 품절 처리
        description.text = null; // 아이템 설명 초기화
        shopSlot = null;

        InitSellSlot();
    }

    private void Sell()
    {
        // 선택하지 않은 경우
        if (sellSlot == null)
        {
            return;
        }

        ItemData itemData = sellSlot.GetItemData();
        if (itemData != null)
        {
            invenItemDatas.Remove(itemData);
        }

        description.text = null; // 아이템 설명 초기화
        sellSlot = null;

        InitSellSlot();
    }
}
