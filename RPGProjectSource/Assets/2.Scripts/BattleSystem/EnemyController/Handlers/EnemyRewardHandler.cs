using System.Collections.Generic;
using UnityEngine;

public class EnemyRewardHandler: MonoBehaviour
{
    public int rewardGold;
    public List<ItemData> rewardItems;

    public (int, ItemData) GetReward()
    {
        ItemData droppedItem = rewardItems[Random.Range(0, rewardItems.Count)];
        return (rewardGold, droppedItem);
    }
}