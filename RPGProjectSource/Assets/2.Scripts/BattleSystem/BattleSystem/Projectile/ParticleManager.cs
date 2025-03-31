using System.Collections.Generic;
using UnityEngine;

public class ParticleManager: Singleton<ParticleManager>
{
    public List<GameObject> particleList;
    private List<(int index, ParticleSystem particle)> pool = new();

    public void CreateParticle(Transform target, int index)
    {
        // var selectedProjectile = pool.Find(projectile => projectile.index == index && !projectile.gameObject.gameObject.activeSelf);
        //
        // if (selectedProjectile)
        // {
        //     selectedProjectile.gameObject.transform.SetPositionAndRotation(target.position, target.rotation);
        //     selectedProjectile.gameObject.gameObject.SetActive(true);
        //     return;
        // }
        //
        // var projectile = Instantiate(projectileList[Random.Range(0, projectileList.Count)], target.position + Vector3.up, target.rotation, transform);
        // pool.Add((index, projectile));
    }
    
    // public void DestroyProjectile(GameObject particle)
    // { 
    //     particle.SetActive(false);
    //     particle.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    // }
}