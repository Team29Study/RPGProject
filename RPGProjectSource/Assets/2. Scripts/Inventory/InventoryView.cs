using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot slotPrefab;

    private List<Slot> slotsCache = new();
    public List<Slot> Slots => slotsCache;

    void Start()
    {

    }

    public void InitializeView(InventoryModel model)
    {
        for (int i = 0; i < model.Count(); i++)
        {
            var go = Instantiate(slotPrefab, slotParent);
            go.gameObject.SetActive(true);
            go.Set(model.Get(i));

            slotsCache.Add(go);
        }
    }
}

//Debug code


//[SerializeField] private GameObject __testSlot;
//[SerializeField] private Inventory __testInventory;
//[SerializeField] private Transform __testParent;

//private List<GameObject> __cache = new();

//void Start()
//{
//    __testInventory.OnValueChanged += Bind;
//}

//void Bind(Item[] items)
//{
//    __cache.ForEach(c => Destroy(c));
//    __cache.Clear();


//    foreach (var iter in items)
//    {
//        var go = Instantiate(__testSlot, __testParent);

//        go.transform.GetChild(0).GetComponent<Image>().sprite =
//            iter?.itemData?.icon;

//        go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
//            iter?.quantity.ToString();

//        go.SetActive(true);

//        __cache.Add(go);
//    }
//}