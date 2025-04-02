using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour, IPopupUI
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
        // ShopUI를 UIManager에 연결
        UIManager.Instance.RegisterUI(this);

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

            if (i < shopItemDatas.Count)
            {
                newSlot.SetSellItem(invenItemDatas[i]); // 아이템 데이터 설정
            }

            sellSlots.Add(newSlot); // 슬롯 리스트에 추가
        }
    }

    //public void UpdateShopSlot()
    //{
    //    // 구매목록 슬롯 갱신
    //    for (int i = 0; shopSlots.Count > i; i++)
    //    {
    //        if (i < shopItemDatas.Count)
    //        {
    //            shopSlots[i].SetShopItem(shopItemDatas[i]); // 아이템 데이터 갱신
    //        }
    //        else
    //        {
    //            shopSlots.Add(null); // 빈 슬롯 추가
    //        }
    //    }

    //    // 판매목록 슬롯 갱신
    //    for (int i = 0; sellSlots.Count > i; i++)
    //    {
    //        if (i < invenItemDatas.Count)
    //        {
    //            sellSlots[i].SetSellItem(invenItemDatas[i]); // 아이템 데이터 갱신
    //        }
    //        else
    //        {
    //            sellSlots.Add(null); // 빈 슬롯 추가
    //        }
    //    }
    //}

    public void ShowDescription(ItemData itemData)
    {
        // 아이템 설명을 UI에 표시
        description.text = $"{itemData.itemName}\n{itemData.description}\nPrice: {itemData.itemPrice}G";
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

        shopSlot.SoldOut(true); // 아이템 구매 시 아이템 품절 처리
        description.text = null; // 아이템 설명 초기화

    }

    private void Sell()
    {
        // 아이템 판매 로직 (추가 필요)
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }

    public void Close()
    {
        throw new System.NotImplementedException();
    }
}
