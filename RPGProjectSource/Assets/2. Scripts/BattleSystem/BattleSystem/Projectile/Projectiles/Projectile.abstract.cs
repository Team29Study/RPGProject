using System;
using UnityEngine;

// 말 그대로 투사체이므로 다형성을 가질 수 있도록 관리
// 조건에 따라 반사, 일정 시간 뒤 폭팔
public class Projectile: MonoBehaviour
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

        if (currentType == ProjectileType.Straight) {
            transform.position += transform.forward * (Time.deltaTime * moveSpeed); // 직진 공격
        }
        if (currentType == ProjectileType.Guided)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position); // 방향을 계산하여 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            
            transform.position += transform.forward * (Time.deltaTime * moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentType == ProjectileType.Reflection) // 반사
        {
            var reflectedDirection = Vector3.Reflect(transform.forward, other.transform.up);
            transform.forward = reflectedDirection;
        }
        
        ProjectileManager.Instance.DestroyProjectile(this);
    }
}

