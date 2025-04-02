using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : NPC
{
    public override void Interaction(Player player)
    {
        Debug.Log("Show");
        UIManager.Instance.GetUI<ShopUI>()?.Open();

        base.Interaction(player);
    }
}
