using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public int detectDistance;
    
    private IState previousState;
    private IState currentState;
    
    private IState idleState;
    private IState tracingState;
    private IState attackState;
    
    [HideInInspector] public NavMeshAgent agent; // 어떤 위치에 있어야할까?
    public Transform target;
    

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        newState.Enter();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // 프리팹 정보 외 씬의 플레이어 인스턴스 등록할 방법이 없음
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Start()
    {
        // 확장될 때마다 생김
        idleState = new IdleState(this);
        tracingState = new TracingState(this);
        attackState = new AttackState(this);
        
        currentState = idleState;
    }

    // 여기서 관리하면? 몬스터마다 상태가 달라질 수 있음(의존성이 떨어짐)
    // 상태에서 관리하면? ...
    private void Update()
    {
        if(currentState != null) {currentState.Update();}
        
        if (currentState != tracingState && Vector3.Distance(transform.position, target.position) <= detectDistance && Vector3.Distance(transform.position, target.position) > agent.stoppingDistance)
        {
            ChangeState(tracingState);
            return;
        }

        if (currentState != attackState && Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance)
        {
            ChangeState(attackState);
            return;
        }
    }
}
