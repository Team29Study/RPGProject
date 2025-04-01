using System;
using UnityEngine;


// 레인지 어택
public class RangeAttackNode : BTNode
{
    public override void Start()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        
    }

    public override void Update()
    {
        Vector3 direction = target.position - transform.position; direction.y = 0; // 공격하면서 회전 필요
        transform.rotation = Quaternion.LookRotation(direction.normalized);

        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 10) SetState(State.Failure); // 공격 범위
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateRangeAttack(transform, 0, HitBox.Caster.Enemy);
    }
}

public class SpreeShotNode : BTNode
{
    
    private float interval = 1f;
    private float currInterval = 0;
    
    
    public override void Start()
    {

        agent.enabled = false;
        
        controller.animationHandler.Set(EnemyAnimationHandler.Spree, true);
        
        // Vector3 direction = target.position - transform.position; direction.y = 0;
        // transform.rotation = Quaternion.LookRotation(direction.normalized);

    }

    public override void Update()
    {
        
        currInterval += Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, 2, 0);
        // 여기선 오히려 멀어지도록 처리
    }

    public override void End()
    {
        agent.enabled = true;
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateRangeAttack(transform, 0, HitBox.Caster.Enemy);
    }
}

public class ParabolaShotNode: BTNode
{
    private float timer = 0;
    private Vector3 startPos;
    private Vector3 endPos;

    public override void Start()
    {
        agent.enabled = false;
        
        this.startPos = transform.position;
        // this.endPos = target.transform.position + (endPos - startPos).normalized * Vector3.forward) * 3f;
    }
    
    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x; // 공식 이해 필요

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
    
    public override void Update()
    {
        timer += Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

        if (transform.position.y < startPos.y) // 이동이 완료된 경우
        {
            return;
        }
        Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
        transform.position = tempPos;
    }
}