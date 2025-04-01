using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // 기본 UI 참조
    [SerializeField] private BaseUI baseUI;
    public BaseUI BaseUI
    {
        get { return baseUI; }
        private set { baseUI = value; }
    }

    // 상점 UI 참조
    [SerializeField] private ShopUI shopUI;
    public ShopUI ShopUI
    {
        get { return shopUI; }
        private set { shopUI = value; }
    }

    // 기본 UI 연결
    public void SetBaseUI(BaseUI baseUI)
    {
        BaseUI = baseUI;
    }

    // 상점 UI 연결
    public void SetShopUI(ShopUI shopUI)
    {
        ShopUI = shopUI;
    }
}
