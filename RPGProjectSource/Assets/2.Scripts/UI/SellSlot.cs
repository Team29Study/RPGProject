using UnityEngine;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour
{
    [SerializeField] Image sellItemIcon;
    [SerializeField] Button sellItemBtn;

    private ItemData itemData;

    // Start is called before the first frame update
    void Start()
    {
        sellItemBtn.onClick.AddListener(SelectSellItem);
    }

    public void SetSellItem(ItemData data)
    {
        itemData = data;

        // 아이템 아이콘이 없는 경우 경고 메시지 출력
        if (itemData.icon == null)
        {
            Debug.LogAssertion("itemData.icon 없음");
        }
        else
        {
            // 아이템 아이콘을 버튼에 설정
            sellItemIcon.sprite = itemData.icon;
        }
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    private void SelectSellItem()
    {
        // 아이템 데이터가 있을 경우, UI에 아이템 선택 처리
        if (itemData != null)
        {
            var shopUI = UIManager.Instance.GetUI<ShopUI>();

            //shopUI?.SelectSellSlot(this);
            //shopUI?.ShowSellDescription(itemData);
        }
    }
}
