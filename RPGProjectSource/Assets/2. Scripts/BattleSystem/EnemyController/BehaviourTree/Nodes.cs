using UnityEngine;

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

public class AttackNode : BTNode
{
    private AttackType currentAttackType;
    public AttackNode(AttackType attackType)
    {
        currentAttackType = attackType;
    }
    
    public override void Start()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        Vector3 direction = target.position - transform.position; direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction.normalized);
        
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        // currentAttackType.Execute(transform);
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) SetState(State.Failure);
    }
}

public class DashAttackNode : BTNode
{
       
}


// 위로는 애니메이션이 처리되어 앞으로만 타겟을 따라 점진적으로 이동, 대상과 부딪히면 충돌 처리
public class JumpAttackNode : BTNode
{
    public override void Start()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public override void Update() // 애니메이션 끝날 때까지
    {
        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, 1f * Time.deltaTime);
    }
    
    
    public override void End() // 끝나면 춫격파를 발사할 수도 있음
    {
        agent.isStopped = false;
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
        if (blackBoard.data["HIT"] == false.ToString())
        {
            SetState(State.Failure);
            return;
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
        controller.animationHandler.Set(EnemyAnimationHandler.Hit);
        controller.resourceHandler.health -= 10;
        
        blackBoard.data["HIT"] = false.ToString();
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