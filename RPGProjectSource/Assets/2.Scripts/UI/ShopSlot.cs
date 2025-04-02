using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] Image shopItemIcon;
    [SerializeField] Image soldOutPanel;
    [SerializeField] Button shopItemBtn;

    private ItemData itemData;

    public bool isSelected = false;

    private void Start()
    {
        shopItemBtn.onClick.AddListener(SelectShopItem); // 아이템 슬롯 선택 버튼에 리스너 추가
    }

    public void SetShopItem(ItemData data)
    {
        itemData = data; // 아이템 데이터 설정

        // 아이템 아이콘이 없는 경우 경고 메시지 출력
        if (itemData.icon == null)
        {
            Debug.LogAssertion("itemData.icon 없음");
        }
        else
        {
            // 아이템 아이콘을 버튼에 설정
            shopItemIcon.sprite = itemData.icon;
        }
    }

    private void SelectShopItem()
    {
        // 아이템 데이터가 있을 경우, UI에 아이템 선택 처리
        if (itemData != null)
        {
            UIManager.Instance.ShopUI.SelectSlot(this); // 현재 슬롯 선택
            UIManager.Instance.ShopUI.ShowDescription(itemData); // 아이템 설명 표시
            isSelected = true; // 선택된 상태로 변경
        }
    }

    public void SoldOut(bool isSoldOut)
    {
        // 품절 상태에 따라 품절 패널을 표시하고 버튼 비활성화
        if (isSoldOut)
        {
            soldOutPanel.gameObject.SetActive(true); // 품절 패널 활성화
            shopItemBtn.interactable = !isSoldOut; // 버튼 비활성화
        }
    }
}
