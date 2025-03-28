using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager: MonoBehaviour
{
    public static ProjectileManager instance { get; private set; }
    public List<GameObject> projectiles = new();
    private int maxProjectiles;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(gameObject);
    }

    public void Generate(Transform target)
    {
        var instance = Instantiate(projectiles[Random.Range(0, projectiles.Count)], target.position + Vector3.up, target.rotation);
        // instance.transform.SetParent(transform);
        
    }
    
    public void Generate(Vector3 position, Quaternion rotation)
    {
        Instantiate(projectiles[Random.Range(0, projectiles.Count)], position, rotation);
    }


    // 삭제하지 않고 비활성화
    // projectile이 본인에게도 맞는 상황
    public void DestoryProjectile(Projectile projectile)
    {
        // Destroy(projectile.gameObject);
        // projectile.gameObject.SetActive(false);
    }
}