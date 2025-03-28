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
        Instantiate(projectiles[Random.Range(0, projectiles.Count)], target.position + Vector3.up, target.rotation);
    }
    
    public void Generate(Vector3 position, Quaternion rotation)
    {
        Instantiate(projectiles[Random.Range(0, projectiles.Count)], position, rotation);
    }


    // 삭제하지 않고 비활성화
    public void DestoryProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }
}