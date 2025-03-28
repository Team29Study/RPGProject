using Unity.VisualScripting;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    public float duration = 1;
    private float currentDuration;
    public float moveSpeed = 20;

    public Transform target;
    
    public void Update()
    {
        currentDuration += Time.deltaTime;
        if (currentDuration >= duration)
        {
            ProjectileManager.instance.DestoryProjectile(this);
            return;
        }
        
        transform.position += transform.forward * (Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileManager.instance.DestoryProjectile(this);
    }
}

public class GuidedProjectile : MonoBehaviour
{
    
}