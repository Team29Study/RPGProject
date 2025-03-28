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

    void Start()
    {
        // UIManager에 BaseUI 연결
        UIManager.Instance.SetBaseUI(this);
    }
}