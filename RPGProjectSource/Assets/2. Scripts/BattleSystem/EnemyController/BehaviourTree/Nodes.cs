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
        
        if (Vector3.Distance(transform.position, target.position) <= 8) SetState(State.Failure);
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
        
        if (Vector3.Distance(transform.position, target.position) <= 8) SetState(State.Failure);
        if (currIntevalImte > maxIntervalTime) SetState(State.Success);
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
        Debug.Log(3);

        controller.animationHandler.Set(EnemyAnimationHandler.Run, true);
        agent.isStopped = false;
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 8) SetState(State.Failure);
        if (distance < agent.stoppingDistance) SetState(State.Success);

        Vector3 direction = target.position - transform.position; direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction.normalized);
        agent.SetDestination(target.position); // 추격
    }
}

public class MeleeAttackNode : BTNode
{
    private float attackRange;
    public MeleeAttackNode(float attackRange)
    {
        this.attackRange = attackRange;
    }
    
    public override void Start()
    {
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > attackRange) SetState(State.Failure);
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

public class HitNode : BTNode
{
    public override void Start()
    {
        if (blackBoard.data["HIT"] == true.ToString())
        {
            controller.animationHandler.Set(EnemyAnimationHandler.Hit);
            controller.resourceHandler.health -= 10;
            blackBoard.data["HIT"] = false.ToString();
        }
        SetState(State.Failure);
    }
}