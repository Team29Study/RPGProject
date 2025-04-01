
using System;
using UnityEngine;

public class DashAttackNode : BTNode // 점프든 대시든 같은 상황 돌진이므로
{
    public override void Start()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
        ProjectileManager.Instance.CreateMeleeAttack(transform, true);
    }
    
    public override void Update() // 애니메이션 끝날 때까지
    {
        transform.rotation *= Quaternion.Euler(0, 4, 0);
        // 너무 붙어버림
        transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
    }

    
    public override void End() // 끝나면 춫격파를 발사할 수도 있음
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, false);
        agent.isStopped = false;
    }
}


// 위로는 애니메이션이 처리되어 앞으로만 타겟을 따라 점진적으로 이동, 대상과 부딪히면 충돌 처리
public class JumpAttackNode : BTNode
{
    private int maxHeight= 3;
    private bool isJumpingSuccess = false;
    private Transform landingPoint;
    
    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x; // 공식 이해 필요

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    
    public override void Start()
    {
        agent.enabled = false; // 활성화 시 공중으로 이동 불가
        landingPoint = target.transform;
    }

    public override void Update() // 애니메이션 끝날 때까지
    {

        transform.position = Vector3.MoveTowards(transform.position, landingPoint.position, 3f * Time.deltaTime);
        
        if (!isJumpingSuccess && transform.position.y >= maxHeight)
        {
            isJumpingSuccess = true;
            return;
        }
        
        if (isJumpingSuccess)
        {
            if (transform.position.y <= 0)
            {
                var vector3 = transform.position; vector3.y = 0;
                transform.position = vector3; // 위치 초기화
                return;
            }
            
            transform.position -= Vector3.up * 3f * Time.deltaTime;
            return;
        }


        if(!isJumpingSuccess && transform.position.y <= maxHeight) // 애니메이션에 의존하는 것이 나을 듯
        {
            transform.position += Vector3.up * 3f * Time.deltaTime;
        }
        
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        if(!isAttacking) SetState(State.Success); // 공격 완료
    }

    public override void End() // 끝나면 춫격파를 발사할 수도 있음
    {
        agent.isStopped = false;
    }
}
