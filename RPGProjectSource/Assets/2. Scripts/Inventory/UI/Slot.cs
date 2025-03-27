using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;

    public event Action OnSlotPointerDownEvent = delegate { };
    public event Action OnSlotPointerUpEvent = delegate { };
    public event Action OnDragEvent = delegate { };

    void Awake()
    {
        iconImage ??= GetComponentInChildren<Image>();
        countText ??= GetComponentInChildren<TextMeshProUGUI>();

        if (iconImage == null || countText == null)
            Debug.LogError("Reference not set");
    }

    public void Set(Item item)
    {
        if (item == null || item.itemData == null)
        {
            iconImage.sprite = null;
            countText.gameObject.SetActive(false);

            return;
        }

        countText.gameObject.SetActive(true);
        iconImage.sprite = item.itemData.icon;
        countText.text = item.quantity.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSlotPointerDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnSlotPointerUpEvent?.Invoke();
    }
}
