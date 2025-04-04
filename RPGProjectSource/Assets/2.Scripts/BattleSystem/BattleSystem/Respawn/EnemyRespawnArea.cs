using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// do: 리스폰 영역과 시간, 몬스터 생성 및 삭제 관리, 바운더리 접근 시 타겟 설정
// notice : 리스폰이 아닌 던전에서 동적 생성 용도로도 사용될 예정
// do : respawn area 가 로컬 좌표가 되도록
// notice: 애너미 매니저로 분리해버리면 라이프 사이클을 알필요 없음
public class EnemyRespawnArea: MonoBehaviour
{
    [HideInInspector] public float duration;
    
    public int maxEnemyCount;
    public List<GameObject> enemyList;
    private List<GameObject> currentEnemies = new();

    public float respawnTime; // 전멸 또는 일정 시간 아무런 동작도 없을 경우
    public Bounds respawnArea;
    private bool isGenerated = false;

    public Action onClearWave;

    private BoxCollider collider;
    
    public void GenerateEnemy(int amount = 1) // 단일로 생성도 가능하도록 처리
    {
        if (enemyList.Count == 0) { Debug.LogWarning("No enemies assigned"); return; }

        for (var index = 0; index < maxEnemyCount; index++)
        {
            // drawing 영역을 벗어나는 현상 발생
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(respawnArea.min.x, respawnArea.max.x), 0, Random.Range(respawnArea.min.z, respawnArea.max.z));
            GameObject instance = Instantiate(enemyList[Random.Range(0, enemyList.Count)], spawnPosition, Quaternion.identity, transform);
            instance.GetComponent<EnemyController>().SetArea(this);
            currentEnemies.Add(instance);
        }
    }
    
    // 삭제 후 매니저에서 처리
    public void DestroyEnemy(GameObject gameObject)
    {
        var selectedEnemy = currentEnemies.Find(enemy => enemy == gameObject);
        currentEnemies.Remove(selectedEnemy);
        
        if(currentEnemies.Count == 0) onClearWave?.Invoke();
        
        Destroy(gameObject);
    }
    public void ClearEnemies() {}

    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + respawnArea.center, respawnArea.size);
    }

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }
    
    // 영역 체크로 동적으로 관리
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isGenerated == false)
            {
                isGenerated = true;
                GenerateEnemy();   
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }

    public void Set(int maxCount, Vector3 detectSize, Vector3 respawnedSize)
    {
        this.maxEnemyCount = maxCount;
        collider.size = detectSize;
        respawnArea.size = respawnedSize;
    }
}