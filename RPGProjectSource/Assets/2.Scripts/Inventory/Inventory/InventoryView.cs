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
    [SerializeField] private Button useButton;

    private List<Slot> slotsCache = new();
    private int currentSlotIndex = -1;

    public List<Slot> Slots => slotsCache;

    public event Action<int, int> OnSlotSwap = delegate { };
    public event Action<int> OnUseItem = delegate { };

    public void InitializeView(int capacity)
    {
        useButton?.gameObject.SetActive(false);

        for (int i = 0; i < capacity; i++)
        {
            int indexCache = i;
            var go = Instantiate(slotPrefab, slotParent);

            go.OnBeginDragEvent += () => OnSlotBeginDrag(indexCache);
            go.OnDragEvent += OnSlotDrag;
            go.OnDropEvent += () => OnSlotDrop(indexCache);
            go.OnClickEvent += () => OnUseButtonClick(indexCache);

            go.gameObject.SetActive(true);

            slotsCache.Add(go);
        }
    }

    private void OnUseButtonClick(int index)
    {
        if (!Slots[index].IsValid())
        {
            useButton?.gameObject.SetActive(false);
            return;
        }

        useButton?.onClick.RemoveAllListeners();
        useButton?.gameObject.SetActive(true);

        useButton?.onClick.AddListener(() =>
        {
            OnUseItem?.Invoke(index);
            useButton?.gameObject.SetActive(false);
        });
    }

    private void OnSlotDrag(PointerEventData eventData)
    {
        //Dragable Images
    }

    private void OnSlotBeginDrag(int index)
    {
        currentSlotIndex = index;
        useButton?.gameObject.SetActive(false);
    }

    private void OnSlotDrop(int index)
    {
        if (currentSlotIndex == -1)
            return;

        OnSlotSwap?.Invoke(currentSlotIndex, index);
    }

}