using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable<Player>
{
    [SerializeField] private List<ItemData> sellingList = new();
    [SerializeField] private ShopView view;

    private ShopPresenter presenter;

    public void Interaction(Player player)
    {
        presenter = new ShopPresenter.Builder(view).WithSellingList(sellingList).Build();

        view.gameObject.SetActive(true); //Show UI
    }
}
