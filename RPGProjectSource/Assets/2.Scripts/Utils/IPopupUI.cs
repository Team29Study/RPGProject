using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopupUI
{
    void Open();
    void Close();
    void Toggle();
    bool IsOpen();
}
