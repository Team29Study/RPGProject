using UnityEngine;

public class IdleNode : BTNode
{
    private float currTime;
    
    private float duration;
    private float detectionRange;

    public IdleNode(float duration, float detectionRange)
    {
        this.duration = duration;
        this.detectionRange = detectionRange;
    }
    
    // 가만히 있는다.
    public override void Start()
    {
        currTime = 0;
        agent.enabled = false;
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 0f);
    }

    public override void Update()
    {
        if (currTime > duration) SetState(State.Success);

        if (Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            SetState(State.Failure);
            return;
        }
        
        currTime += Time.deltaTime;
    }

    public override void End()
    {
        agent.enabled = true;
    }
}

public class PatrolNode : BTNode
{
    private float currTime;
    private float duration;
    
    private Vector3 randomDirection;

    public PatrolNode(float duration)
    {
        this.duration = duration;
    }
    
    public override void Start()
    {
        currTime = 0;
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 1f);
        
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        transform.rotation = Quaternion.LookRotation(randomDirection);
    }
    
    

    public override void Update()
    {
        currTime += Time.deltaTime;
        transform.position += randomDirection * Time.deltaTime * 2f;

        if (Vector3.Distance(transform.position, target.position) <= 8)
        {
            SetState(State.Failure);
            return;
        }

        if (currTime > duration)
        {
            SetState(State.Success);
        }
    }
}

public class TracingNode : BTNode // 추격
{
    protected float tracingRange;
    public TracingNode(float tracingRange)
    {
        this.tracingRange = tracingRange;
    }

    public override void Start()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > tracingRange) { SetState(State.Failure); return; }
        
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 1.8f);
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
 
        if (distance > tracingRange)
        {
            SetState(State.Failure);
            return;
        }
        
        if (distance < agent.stoppingDistance)
        {
            SetState(State.Success);
            return;
        }
        
        agent.SetDestination(target.position);
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



// hit 시퀀스 안에서 진행
public class DieNode : BTNode
{
    private float currTime;
    private float duration = 1f;
    
    private bool isDeath = false;
    public override void Start()
    {
        if (controller.resourceHandler.health != 0) {
            SetState(State.Success); // 통과
            return;
        }

        if (isDeath == false)
        {
            isDeath = true;
            currTime = 0;
            controller.animationHandler.Set(EnemyAnimationHandler.Die, true);
        }
    }

    public override void Update()
    {
        currTime += Time.deltaTime;
        if (currTime > duration)
        {
            controller.respawnArea.DestroyEnemy(controller.gameObject);
        }
    }
}

// 넉백이 구현된 형태
public class HitNode : BTNode
{
    public float knockBackTime = 1;
    private float curentKnockBackTime = 0;
    
    public override void Start()
    {
        if (blackBoard.data[BlackBoard.Trigger.Hit] != true.ToString())
        {
            SetState(State.Failure);
            return;
        }

        agent.enabled = false;
        curentKnockBackTime = 0;

        // 체력 감소
        controller.animationHandler.Set(EnemyAnimationHandler.Hit);
        controller.resourceHandler.health -= controller.currDamagedInfo.Item1;
        
        blackBoard.SetData(BlackBoard.Trigger.Hit, false.ToString());
    }

    public override void Update()
    {
        // 넉백을 주는 경우
        // curentKnockBackTime += Time.deltaTime;
        // if (curentKnockBackTime >= knockBackTime)
        // {
            SetState(State.Success);
            // return;
        // }
        
        // transform.position += Vector3.back * 3f * Time.deltaTime;
    }

    public override void End()
    {
        agent.enabled = true;
    }
}

public class FleeNode : BTNode
{
    
}