using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// do: 리스폰 영역과 시간, 몬스터 생성 및 삭제 관리, 바운더리 접근 시 타겟 설정
public class EnemyArea: MonoBehaviour
{
    public int maxEnemies;
    public List<GameObject> enemies;
    private List<GameObject> currentEnemies;

    public float respawnTime; // 전멸 또는 일정 시간 아무런 동작도 없을 경우
    
    public Rect respawnArea;

    private void Start()
    {
        if (enemies == null)
        {
            // Debug.LogError("No enemies assigned");
            return;
        }

        for (var index = 0; index < maxEnemies; index++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(respawnArea.min.x, respawnArea.max.x),
                0,
                Random.Range(respawnArea.min.y, respawnArea.max.y)
            );
            
            GameObject instance = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPosition, Quaternion.identity);
            currentEnemies.Add(instance);
        }
            
    }
    

    // 영역 체크로 동적으로 관리
    public void OnTriggerEnter(Collider other)
    {
        
    }
}