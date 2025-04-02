using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCShop : NPC
{
    [SerializeField] private List<ItemData> sellingDatas = new();

    public override void Interaction(Player player)
    {
        var shopRef = UIManager.Instance.GetUI<ShopUI>();
        shopRef.Open();

        Debug.Log(shopRef);

        base.Interaction(player);

        if (shopRef)
        {
            var inven = player.Inventory.Model;

            List<ItemData> myItems = new();

            for (int i = 0; i < inven.Count(); i++)
            {
                if (inven.GetItemDataAt(i) != default)
                {
                    myItems.Add(inven.GetItemDataAt(i));
                }
            }

            shopRef.InitializeSellingDatas(myItems);
            shopRef.InitializeBuyDatas(sellingDatas);
        }
    }
}
