using UnityEngine;

public class Projectile: MonoBehaviour
{
    [HideInInspector] public ProjectileManager projectileManager;
    
    public float duration;
    private float currentDuration;
    
    
    public float moveSpeed;
    public void Update()
    {
        currentDuration += Time.deltaTime;
        if (currentDuration >= duration)
        {
            projectileManager.Destory();
            return;
        }
        
        transform.Translate(transform.forward * Time.deltaTime);
    }
}

public class GuidedProjectile : MonoBehaviour
{
    
}