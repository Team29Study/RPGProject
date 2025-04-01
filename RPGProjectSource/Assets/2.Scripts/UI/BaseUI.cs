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
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private GameObject pauseWindow;

    private void Start()
    {
        // UIManager에 BaseUI 연결
        UIManager.Instance.SetBaseUI(this);
        pauseBtn.onClick.AddListener(OnPause);
        resumeBtn.onClick.AddListener(OffPause);
    }

    private void OnPause()
    {
        Time.timeScale = 0f;
        pauseWindow.SetActive(true);
    }

    private void OffPause()
    {
        Time.timeScale = 1f;
        pauseWindow.SetActive(false);
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