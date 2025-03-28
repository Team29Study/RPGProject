using UnityEngine;

public class BaseUI : MonoBehaviour
{
    void Start()
    {
        // UIManager에 BaseUI 연결
        UIManager.Instance.SetBaseUI(this);
    }
}