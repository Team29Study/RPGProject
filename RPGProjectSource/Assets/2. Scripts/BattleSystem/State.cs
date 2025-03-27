using UnityEngine;
using UnityEngine.AI;

public abstract class IState
{
    protected StateMachine stateMachine;

    public IState(StateMachine newStateMachine)
    {
        stateMachine = newStateMachine;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : IState
{
    public IdleState(StateMachine newStateMachine) : base(newStateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entered Idle State");
    }
    public override void Update() {}
    public override void Exit() {}
}

public class TracingState : IState
{
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");

    // 어디서 관리?
    private Animator animator;
    
    public TracingState(StateMachine newStateMachine) : base(newStateMachine)
    {
        animator = stateMachine.GetComponent<Animator>();
    }

    public override void Enter()
    {
        Debug.Log("Entered the tracing state");
        animator.SetBool(Run, true);
        animator.SetBool(Attack, false);

    }

    public override void Update()
    {
        if (Mathf.Approximately(stateMachine.target.rotation.y, stateMachine.transform.rotation.y))
        {
            Debug.Log("show back position"); // 뒤인지 체크 (안됨)
        }
        
        stateMachine.agent.SetDestination(stateMachine.target.position);
    }
    public override void Exit() {}
}

public class AttackState : IState
{
    private Animator animator;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private HitBox hitbox;
    public AttackState(StateMachine newStateMachine) : base(newStateMachine)
    {
        animator = stateMachine.GetComponent<Animator>();
        hitbox = stateMachine.GetComponentInChildren<HitBox>(true);
    }

    public override void Enter()
    {
        Debug.Log("enter attack State");
        hitbox.gameObject.SetActive(true);
        animator.SetBool(Attack, true);

    }

    public override void Update()
    {
    }
    public override void Exit() {}
}