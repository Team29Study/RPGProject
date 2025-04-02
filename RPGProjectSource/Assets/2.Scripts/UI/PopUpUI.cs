using System;
using UnityEngine;

public class PopUpUI : MonoBehaviour, IPopupUI
{
    protected bool isOpen = false;
    public event Action onChanged = delegate { };

    public virtual void Open()
    {
        gameObject.SetActive(true);
        onChanged?.Invoke();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        onChanged?.Invoke();
    }

    public virtual bool IsOpen() => isOpen;
}
