using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager: Singleton<ProjectileManager> 
{
    public List<GameObject> projectiles;
    private int maxProjectiles = 5;

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

    public void Generate(Transform target, HitBox.Caster caster)
    {
        var projectile = Instantiate(projectiles[Random.Range(0, projectiles.Count)], target.position + Vector3.up, target.rotation);
        var hitBox = projectile.AddComponent<HitBox>(); // 또는 무기 종류가 각자 정해졌다면 인스펙터에서 직접 등록하도록 처리
        hitBox.caster = caster;
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