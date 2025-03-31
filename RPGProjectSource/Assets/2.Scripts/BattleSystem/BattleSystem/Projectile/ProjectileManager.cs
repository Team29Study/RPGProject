using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager: Singleton<ProjectileManager>
{
    public GameObject hitBox; // 근접 공격도 발사체로 간주
    public List<GameObject> projectileList;
    
    private int maxProjectiles = 5;
    private List<(int index, GameObject gameObject)> pool = new();

    // 총알이 종류가 다르거나, 최대 pool 용량을 가진 경우 active된 대상 중 가장 빠른(맨 앞) 친구 가져오기 허용할 지 고려
    public void CreateRangeAttack(Transform target, int index, HitBox.Caster caster)
    {
        var selectedProjectile = pool.Find(projectile => projectile.index == index && !projectile.gameObject.gameObject.activeSelf);
        
        if (selectedProjectile.gameObject)
        {
            selectedProjectile.gameObject.transform.SetPositionAndRotation(target.position + Vector3.up, target.rotation);
            selectedProjectile.gameObject.gameObject.SetActive(true);
            return;
        }
        
        var projectile = Instantiate(projectileList[Random.Range(0, projectileList.Count)], target.position + Vector3.up, target.rotation, transform);
        // var hitBox = projectile.AddComponent<HitBox>(); // 또는 무기 종류가 각자 정해졌다면 인스펙터에서 직접 등록하도록 처리
        // hitBox.caster = caster;
        pool.Add((index, projectile));
    }
    
    public void CreateRangeAttack(Vector3 position, Quaternion rotation, HitBox.Caster caster)
    {
        var projectile = Instantiate(projectileList[Random.Range(0, projectileList.Count)], position, rotation, transform);
        var hitBox = projectile.AddComponent<HitBox>();
        hitBox.caster = caster;
    }


    // caster 알 필요 있음
    public void CreateMeleeAttack(Transform target, bool isActive)
    {
        HitBox selectedHitBox = target.GetComponentInChildren<HitBox>(true);

        if (isActive)
        {
            if (selectedHitBox)
            {
                selectedHitBox.gameObject.SetActive(true);
                return;
            }
        
            // 앞쪽 위치 또는 크기는 사정거리로 간주
            Instantiate(this.hitBox, target.position + (target.transform.forward * 1.2f) + target.transform.up, target.rotation, target.transform);
            return;
        }

        if(selectedHitBox) selectedHitBox.gameObject.SetActive(false);
    }
    
    // 삭제하지 않고 비활성화
    // projectile이 본인에게도 맞는 상황
    public void DestroyProjectile(IProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}