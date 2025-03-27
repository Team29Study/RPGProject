using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager: MonoBehaviour
{
    public static ProjectileManager instance { get; private set; }
    public List<GameObject> projectiles = new();

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
        Debug.Log(target);
        
        var instance = Instantiate(projectiles[Random.Range(0, projectiles.Count)]);
        // var instance = Instantiate(projectiles[Random.Range(0, projectiles.Count)], transform, true);
        instance.transform.position = new Vector3(target.position.x , 1, target.position.z);
        
        // instance.transform.rotation = target.rotation;
        instance.transform.rotation = Quaternion.LookRotation(target.forward);
        // instance.transform.rotation = Quaternion.LookRotation(target.position - instance.transform.position).normalized;
    }


    public void DestoryProjectile(Projectile projectile)
    {
    }
}