using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI countText;

    public event Action OnBeginDragEvent = delegate { };
    public event Action OnDropEvent = delegate { };
    public event Action<PointerEventData> OnDragEvent = delegate { };
    public event Action OnClickEvent = delegate { };

    void Awake()
    {
        iconImage ??= GetComponentInChildren<Image>();
        countText ??= GetComponentInChildren<TextMeshProUGUI>();

        if (iconImage == null || countText == null)
            Debug.LogError("Reference not set");
    }

    public void Set(Sprite icon, int count)
    {
        iconImage.gameObject.SetActive(icon != null ? true : false);
        iconImage.sprite = icon;
        countText.text = count.ToString();
        countText.gameObject.SetActive(count > 1 ? true : false);
    }

    public bool IsValid() => iconImage.gameObject.activeSelf;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }
}
