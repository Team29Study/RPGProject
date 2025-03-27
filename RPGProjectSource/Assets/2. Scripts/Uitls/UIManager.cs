using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    // 항상 활성화 되는 UI 참조
    private BaseUI baseUI;
    public BaseUI BaseUI
    {
        get { return baseUI; }
        private set { baseUI = value; } }

    // 상점 UI 참조
    private ShopUI shopUI;
    public ShopUI ShopUI
    {
        get { return shopUI; }
        private set { shopUI = value; }
    }

    private void Awake()
    {
        // 중복 방지
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
