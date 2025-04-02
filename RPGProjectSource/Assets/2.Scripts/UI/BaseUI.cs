using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : PopUpUI
{
    [Header("Bar")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private Image expBar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI expText;

    [Header("Pause")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject pauseWindow;

    private void Start()
    {
        // UIManager에 BaseUI 연결
        UIManager.Instance.RegisterPopUp(this);

        // 일시정지 및 실행 버튼 연결
        pauseBtn.onClick.AddListener(OnPause);
        resumeBtn.onClick.AddListener(OffPause);
        exitBtn.onClick.AddListener(ExitGame);
    }

    // 일시정지
    private void OnPause()
    {
        Time.timeScale = 0f;
        pauseWindow.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    // 일시정지 해제
    private void OffPause()
    {
        Time.timeScale = 1f;
        pauseWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 게임 종료(임시)
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
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