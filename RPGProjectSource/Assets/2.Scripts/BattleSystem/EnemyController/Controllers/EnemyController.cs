using JetBrains.Annotations;
using System;
using UnityEngine;

// 중재자 패턴
public class EnemyController: MonoBehaviour, IDamagable
{
    [HideInInspector] public static Transform target { get; private set; }
    public EnemyRespawnArea respawnArea { get; private set; }

    public EnemyResourceHandler resourceHandler { get; private set; }
    public EnemyAnimationHandler animationHandler { get; private set; }
    // public EnemyRewardHandler rewardHandler { get; private set; }
    public BehaviourTree behaviourTree { get; private set; }

    public (int, Transform) currDamagedInfo { get; private set; }
    
    public enum EnemyType { Minion, Boss }
    public EnemyType currentEnemyType; // 조건에 따라 BT 로직 변경
    

    private void Awake()
    {
        if(!target) target = GameObject.FindGameObjectWithTag("Player")?.transform; // 비용이 들 수 있음
        
        resourceHandler = GetComponent<EnemyResourceHandler>(); // 리소스도 추후 SO로 변경 가능
        animationHandler = new EnemyAnimationHandler(GetComponent<Animator>());
        // rewardHandler = GetComponent<EnemyRewardHandler>();
        
        behaviourTree = new BehaviourTree();
    }
    
    protected virtual void Update()
    {
        behaviourTree.Run();
    }

    public virtual void TakeDamage(int damage, Transform attackTr = null) // 방향 정보 어떻게?
    {
        // 워리어 또는 보스인 경우 Hit으로 상태 전의 발생 X
        currDamagedInfo = new(damage, attackTr);
        behaviourTree.notify(BlackBoard.Trigger.Hit, true.ToString());
    }
    
    // 애니메이션 이벤트랑 상호작용
    public void OnAttackAnimated(int isAttacking)// 상태 변경의 순간은 제외 필요
    {
        behaviourTree.currentnode?.OnAttackAnimated(isAttacking == 1);
    }

    public void SetArea(EnemyRespawnArea respawnArea)
    {
        this.respawnArea = respawnArea;
    }
}