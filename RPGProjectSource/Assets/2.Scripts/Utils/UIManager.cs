using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager>
{
    private List<IPopupUI> windows = new();
    private List<PopUpUI> popUpUIs = new();

    private Dictionary<Type, PopUpUI> popupDict = new();

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

    public void RegisterUI(IPopupUI window)
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
        var type = popUpUI.GetType();

        if (!popupDict.TryGetValue(type, out var value))
        {
            popupDict.Add(type, value);
            popUpUI.onChanged += PopUpChange;
        }

        if (!popUpUIs.Contains(popUpUI))
            popUpUIs.Add(popUpUI);

        popUpUI.onChanged += PopUpChange;
    }

    public T GetUI<T>(T target) where T : PopUpUI
    {
        var type = target.GetType();

        if (popupDict.TryGetValue(type, out var value))
        {
            return value as T;
        }

        return default;
    }

    private void PopUpChange()
    {

    }
}
