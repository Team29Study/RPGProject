using UnityEngine;

public class ShopUI : MonoBehaviour
{
    void Start()
    {
        // UIManager에 ShopUI 연결
        UIManager.Instance.SetShopUI(this);
    }
}
