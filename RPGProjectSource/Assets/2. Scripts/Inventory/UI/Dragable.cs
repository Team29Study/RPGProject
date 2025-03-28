using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dragable : MonoBehaviour
{
    [SerializeField] private InputAction dragAction;

    public event Action<InputAction.CallbackContext> OnStartDrag
    {
        add => dragAction.started += value;
        remove => dragAction.started -= value;
    }

    public event Action<InputAction.CallbackContext> OnCancelDrag
    {
        add => dragAction.canceled += value;
        remove => dragAction.canceled -= value;
    }

    public event Action<InputAction.CallbackContext> OnDrag
    {
        add => dragAction.performed += value;
        remove => dragAction.performed -= value;
    }

    void OnEnable() => dragAction.Enable();
    void OnDisable() => dragAction.Disable();
}
