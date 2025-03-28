using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;


    private void Start()
    {
        // UIManager에 ShopUI 연결
        UIManager.Instance.SetShopUI(this);
        buyBtn.onClick.AddListener(Buy);
        sellBtn.onClick.AddListener(Sell);
    }

    private void Buy()
    {

    }

    private void Sell()
    {

    }
}
