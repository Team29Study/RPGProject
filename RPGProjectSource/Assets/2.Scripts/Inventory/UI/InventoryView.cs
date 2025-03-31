using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot slotPrefab;

    private List<Slot> slotsCache = new();
    private int currentSlotIndex = -1;

    public List<Slot> Slots => slotsCache;
    public event Action<int, int> OnSlotSwap = delegate { };

    public void InitializeView(InventoryModel model)
    {
        for (int i = 0; i < model.Count(); i++)
        {
            int indexCache = i;
            var go = Instantiate(slotPrefab, slotParent);

            go.OnBeginDragEvent += () => OnSlotBeginDrag(indexCache);
            go.OnDragEvent += OnSlotDrag;
            go.OnDropEvent += () => OnSlotDrop(indexCache);
            go.gameObject.SetActive(true);

            slotsCache.Add(go);
        }
    }

    private void OnSlotDrag(PointerEventData eventData)
    {
        //Dragable Images
    }

    private void OnSlotBeginDrag(int index)
    {
        currentSlotIndex = index;
    }

    private void OnSlotDrop(int index)
    {
        if (currentSlotIndex == -1)
            return;

        OnSlotSwap?.Invoke(currentSlotIndex, index);
    }

}