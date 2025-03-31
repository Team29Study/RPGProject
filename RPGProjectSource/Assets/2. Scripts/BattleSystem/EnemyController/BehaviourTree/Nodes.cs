using System;
using UnityEngine;
using Random = UnityEngine.Random;

// 거리 값 조절의 역할 누구에게 둘 것인가? - movementHandler
public class IdleNode : BTNode
{
    private float currIntevalImte = 0;
    private float maxIntervalTime = 1;
    
    
    // 가만히 있는다.
    public override void Start()
    {
        // 공격이 뒤에 있다면 모든 정보 초기화
        agent.isStopped = true;
        controller.animationHandler.Set(EnemyAnimationHandler.Run, false);
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, false);
    }

    public override void Update()
    {
        currIntevalImte += Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= 8)
        {
            SetState(State.Failure);
            return;
        }
        if (currIntevalImte > maxIntervalTime) SetState(State.Success);
    }

    public override void End()
    {
        currIntevalImte = 0;
    }
}

public class PatrolNode : BTNode
{
    private float currIntevalImte = 0;
    private float maxIntervalTime = 1;
    private Vector3 currentDirection;

    private void SetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        currentDirection = new Vector3(randomX, 0, randomZ).normalized;
    }
    
    public override void Start()
    {
        controller.animationHandler.Set(EnemyAnimationHandler.Run, true);
        SetRandomDirection();
        transform.rotation = Quaternion.LookRotation(currentDirection);
    }
    
    

    public override void Update()
    {
        currIntevalImte += Time.deltaTime;
        transform.position += currentDirection * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= 8)
        {
            SetState(State.Failure);
            return;
        }

        if (currIntevalImte > maxIntervalTime)
        {
            SetState(State.Success);
        }
    }

    public override void End()
    {
        currIntevalImte = 0;
    }
}

public class TracingNode : BTNode // 추격
{
    public override void Start()
    {
        controller.animationHandler.Set(EnemyAnimationHandler.Run, true);
        agent.isStopped = false;
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 8) SetState(State.Failure);
        if (distance < agent.stoppingDistance)
        {
            SetState(State.Success);
            return;
        }
        
        agent.SetDestination(target.position); // 추격
    }
}


public class BoundaryNode : BTNode // 경계, 애니메이션 필요
{
    public override void Start()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public override void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= 3)
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);

            Vector3 direction = (transform.position - target.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, 1f * Time.deltaTime);
        }
    }

    public override void End()
    {
        agent.isStopped = false;
    }
}

public class MeleeAttackNode : BTNode
{
    public override void Start()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        Vector3 direction = target.position - transform.position; direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction.normalized);
        
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) SetState(State.Failure);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, isAttacking);
    }
}

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
        if (distance > 5) SetState(State.Failure);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateRangeAttack(transform, 0, HitBox.Caster.Enemy);
    }
}

public class MageAttack : BTNode
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
        if (distance > 5) SetState(State.Failure);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateRangeAttack(target.transform, 1, HitBox.Caster.Enemy);
    }
}

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



// hit 시퀀스 안에서 진행
public class DieNode : BTNode
{
    public override void Start()
    {
        if (controller.resourceHandler.health != 0) SetState(State.Failure);
    }
}

// 넉백이 구현된 형태
public class HitNode : BTNode
{
    public float knockBackTime = 1;
    private float curentKnockBackTime = 0;
    
    public override void Start()
    {
        if (blackBoard.data[BlackBoard.Trigger.Hit] == false.ToString())
        {
            SetState(State.Failure);
            return;
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
        controller.animationHandler.Set(EnemyAnimationHandler.Hit);
        controller.resourceHandler.health -= 10;
        
        blackBoard.data[BlackBoard.Trigger.Hit] = false.ToString();
    }

    public override void Update()
    {
        curentKnockBackTime += Time.deltaTime;
        if (curentKnockBackTime >= knockBackTime)
        {
            SetState(State.Success);
            return;
        }
        
        transform.position += Vector3.back * 3f * Time.deltaTime;
    }

    public override void End()
    {
        curentKnockBackTime = 0;
    }
}

public class FleeNode : BTNode
{
    
}