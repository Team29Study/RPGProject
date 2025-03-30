using System;
using UnityEngine;

// 중재자 패턴
public class EnemyController: MonoBehaviour
{
    public Transform target;
    public EnemyResourceHandler resourceHandler { get; private set; }
    public EnemyAnimationHandler animationHandler { get; private set; }
    // public EnemyRewardHandler rewardHandler { get; private set; }
    public BehaviourTree behaviourTree { get; private set; } 

    private void Awake()
    {
        if(!target) target = GameObject.FindGameObjectWithTag("Player").transform;
        
        resourceHandler = GetComponent<EnemyResourceHandler>(); // 리소스도 추후 SO로 변경 가능
        animationHandler = new EnemyAnimationHandler(GetComponent<Animator>());
        // rewardHandler = GetComponent<EnemyRewardHandler>();
        
        behaviourTree = new BehaviourTree();
    }
    
    protected virtual void Update()
    {
        behaviourTree.Run();
    }

    protected void OnTriggerEnter(Collider other)
    {
        behaviourTree.notify(BlackBoard.Trigger.Hit, true.ToString());
    }

    // 애니메이션 이벤트랑 상호작용
    public void OnAttackAnimated(int isAttacking)
    {
        behaviourTree.currentnode.OnAttackAnimated(isAttacking == 1);
    }
}