using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private List<IWindow> windows = new();
    private List<PopUpUI> popUpUIs = new();

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

    public void RegisterUI(IWindow window)
    {
        if (!windows.Contains(window))
            windows.Add(window);

        // 타입별로 자동 연결
        switch (window)
        {
            case BaseUI baseUI:
                BaseUI = baseUI;
                break;

            case ShopUI shopUI:
                ShopUI = shopUI;
                break;
        }
    }

    public void RegisterPopUp(PopUpUI popUpUI)
    {
        if (!popUpUIs.Contains(popUpUI))
            popUpUIs.Add(popUpUI);

        popUpUI.onChanged += PopUpChange;
    }

    private void PopUpChange()
    {

    }
}
