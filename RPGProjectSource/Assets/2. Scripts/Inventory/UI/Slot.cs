using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;

    public event Action OnBeginDragEvent = delegate { };
    public event Action OnDropEvent = delegate { };
    public event Action<PointerEventData> OnDragEvent = delegate { };

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke();
    }
}
