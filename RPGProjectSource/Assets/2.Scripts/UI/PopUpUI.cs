using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : MonoBehaviour, IWindow
{
    protected bool isOpen = false;
    public event Action onChanged = delegate { };

    public virtual void Open()
    {
        onChanged?.Invoke();   
    }

    public virtual void Close()
    {
        onChanged?.Invoke();
    }

    public virtual bool IsOpen()
    {
        return isOpen;
    }
}
