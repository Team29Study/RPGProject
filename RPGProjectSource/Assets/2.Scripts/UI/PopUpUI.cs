using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : MonoBehaviour, IPopupUI
{
    protected bool isOpen = false;
    public event Action onChanged = delegate { };

    public virtual void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
        onChanged?.Invoke();

        Debug.Log("!1");
    }

    public virtual void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
        onChanged?.Invoke();
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        gameObject.SetActive(gameObject.activeInHierarchy ? false : true);
        onChanged?.Invoke();
    }

    public virtual bool IsOpen() => isOpen;
}
