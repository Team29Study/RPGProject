using UnityEngine;

public class Projectile: MonoBehaviour
{
    public float duration;
    private float currentDuration;
    public float moveSpeed;
    
    public void Update()
    {
        currentDuration += Time.deltaTime;
        if (currentDuration >= duration)
        {
            ProjectileManager.instance.DestoryProjectile(this);
            Destroy(gameObject);
            
            return;
        }
        
        transform.Translate(transform.forward * (Time.deltaTime * moveSpeed));
    }
}

public class GuidedProjectile : MonoBehaviour
{
    
}