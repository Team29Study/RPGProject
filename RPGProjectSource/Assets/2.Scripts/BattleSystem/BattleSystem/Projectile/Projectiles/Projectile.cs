using Unity.VisualScripting;
using UnityEngine;

public abstract class IProjectile: MonoBehaviour {}

// 말 그대로 투사체이므로 다형성을 가질 수 있도록 관리
// 조건에 따라 반사, 일정 시간 뒤 폭팔
public class Projectile: IProjectile
{
    public float duration = 1; 
    private float currDuration;
    public float moveSpeed = 20;
    public Transform target;
    
    public enum ProjectileType { Straight, Guided, Reflection }
    public ProjectileType currentType;

    private void Awake()
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnEnable()
    {
        currDuration = 0;
    }

    private void Update()
    {
        currDuration += Time.deltaTime;
        if (currDuration >= duration)
        {
            ProjectileManager.Instance.DestroyProjectile(this);
            return;
        }

        if (currentType == ProjectileType.Straight || currentType == ProjectileType.Reflection) {
            transform.position += transform.forward * (Time.deltaTime * moveSpeed); // 직진 공격
        }
        if (currentType == ProjectileType.Guided)
        {
            
            Vector3 newForward = Vector3.Lerp(transform.forward, target.transform.position, Time.deltaTime * 3f); // y값 감소 필요
            transform.forward = newForward;
            
            transform.position += transform.forward * (Time.deltaTime * 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (currentType == ProjectileType.Reflection)
        {
            var reflectedDirection = Vector3.Reflect(transform.forward, Vector3.forward); // 반사각 체크 필요
            transform.forward = reflectedDirection;

            return;
        }
        
        ProjectileManager.Instance.DestroyProjectile(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}

