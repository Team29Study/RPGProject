
using System;
using UnityEngine;

public class DefenceNode : BTNode // 일단은 제자리에서 방어하도록 처리, 추적과 함께 할 것인지?
{
    private float duration = 4f; // 파훼법이 없기 때문에 일정 시간으로 변경
    private float currTime;
    
    
    public override void Start()
    {
        currTime = 0f;
        agent.enabled = false;
        
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 0f);
        controller.animationHandler.Set(EnemyAnimationHandler.Defence, true);

        if (controller is WarriorController warriorController) { warriorController.isDefenced = true; } // 컨트롤러가 다른 부분 처리 필요
    }

    public override void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z));
        
        currTime += Time.deltaTime;
        if (currTime >= duration)
        {
            if (controller is WarriorController warriorController) { warriorController.isDefenced = false; } // 컨트롤러가 다른 부분 처리 필요
            SetState(State.Success);
        }
    }

    public override void End()
    {
        agent.enabled = true;
        controller.animationHandler.Set(EnemyAnimationHandler.Defence, false);
    }
}

// 직전 이동, 유도 이동, 튕기는 이동
public class RushAttackMode : BTNode // 점프든 대시든 같은 상황 돌진이므로
{
    private Vector3 destination; // 도착지 방식
    private float moveSpeed;
    
    private float duration; // 파훼법이 없기 때문에 일정 시간으로 변경
    private float currTime;

    public RushAttackMode(float duration = 1f, float moveSpeed = 3f)
    {
        this.duration = duration;
        this.moveSpeed = moveSpeed;
    }
    
    public override void Start()
    {
        if (controller is WarriorController warriorController && warriorController.isDefenced)
        {
            SetState(State.Success);
            return;
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero; // 중복적인 부분

        
        ProjectileManager.Instance.CreateMeleeAttack(transform, true); // 몸체 부분에서 진행되어야 함
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 0f);
        controller.animationHandler.Set(EnemyAnimationHandler.Rush, true);
        
        currTime = 0;
    }
    
    public override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2f * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0, 8, 0); // 애니메이션 잠금 뒤 직접 회전
        
        currTime += Time.deltaTime;
        if (currTime >= duration)
        {
            SetState(State.Success);
        }
    }
    
    public override void End() // 끝나면 춫격파를 발사할 수도 있음
    {
        agent.isStopped = false;

        ProjectileManager.Instance.CreateMeleeAttack(transform, false);
        controller.animationHandler.Set(EnemyAnimationHandler.Rush, false);
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
