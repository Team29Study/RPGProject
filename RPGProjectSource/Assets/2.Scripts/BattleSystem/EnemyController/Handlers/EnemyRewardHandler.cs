using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRewardHandler: MonoBehaviour
{
    public int rewardGold;
    public List<ItemData> rewardItems;
    public List<GameObject> dropItems;
    
    public (int, ItemData) GetReward()
    {
        ItemData droppedItem = rewardItems[Random.Range(0, rewardItems.Count)];
        return (rewardGold, droppedItem);
    }

    // 단순 처리: 사망 시 드랍 아이템 추출
    public void OnDestroy()
    {
        Instantiate(dropItems[Random.Range(0, dropItems.Count)]);    
    }
}