using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, PopUpUI> popupDict = new();

    public void RegisterPopUp(PopUpUI popUpUI)
    {
        var type = popUpUI.GetType();

        if (!popupDict.TryGetValue(type, out var value))
        {
            popupDict.Add(type, popUpUI);
            popUpUI.onChanged += PopUpChange;
        }
    }

    public void DeregisterPopUp(PopUpUI popUpUI)
    {
        var type = popUpUI.GetType();

        if (popupDict.TryGetValue(type, out var value))
        {
            popupDict.Remove(type);
            popUpUI.onChanged -= PopUpChange;
        }
    }

    public T GetUI<T>() where T : PopUpUI
    {
        var type = typeof(T);

        if (popupDict.TryGetValue(type, out var value))
        {
            return value as T;
        }

        return default;
    }

    private void PopUpChange()
    {
        bool isAllClosed = popupDict.All(e => e.Value.IsOpen() == false);

        if (isAllClosed)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }    
    }
}
