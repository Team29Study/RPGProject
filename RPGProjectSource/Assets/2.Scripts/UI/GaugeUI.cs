using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    private Image fillImage;

    void Awake() => fillImage = GetComponent<Image>();

    public void Set(BoundedValue value) => fillImage.fillAmount = value.Ratio;
}
