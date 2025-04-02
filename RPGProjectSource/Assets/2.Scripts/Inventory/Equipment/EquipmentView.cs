using System.Collections.Generic;
using UnityEngine;

public class EquipmentView : PopUpUI
{
    [SerializeField]
    private List<Slot> slots = new();

    public List<Slot> Slots => slots;

    void Start()
    {
        UIManager.Instance.RegisterPopUp(this);
        Close();
    }

    public void InitializeView()
    {
        Slots.ForEach(e => e.Set(null));
    }
}