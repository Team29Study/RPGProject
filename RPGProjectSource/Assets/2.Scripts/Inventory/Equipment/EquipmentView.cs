using System.Collections.Generic;
using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    [SerializeField]
    private List<Slot> slots = new();

    public List<Slot> Slots => slots;

    public void InitializeView()
    {
        Slots.ForEach(e => e.Set(null));
    }
}