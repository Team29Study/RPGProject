using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button closeBtn;

    private void Start()
    {
        // UIManager에 BaseUI 연결
        UIManager.Instance.SetBaseUI(this);
        shopBtn.onClick.AddListener(OpenShop);
        closeBtn.onClick.AddListener(CloseShop);
        
    }

    private void OpenShop()
    {
        UIManager.Instance.ShopUI.gameObject.SetActive(true);
    }

    private void CloseShop()
    {
        UIManager.Instance.ShopUI.gameObject.SetActive(false);
    }

    private void SetHpBar()
    {

    }
    
    private void SetMpBar()
    {

    }

    private void SetExpBar()
    {

    }

    private void SetLevel()
    {

    }

    private void SetGold()
    {

    }
}