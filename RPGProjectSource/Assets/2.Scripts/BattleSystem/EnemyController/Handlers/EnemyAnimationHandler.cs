using UnityEngine;

// 트리거 목록만 static으로 추가되는 형태
public class EnemyAnimationHandler
{
    public static readonly int Walk = Animator.StringToHash("Walk");
    public static readonly int Move = Animator.StringToHash("Move");
    public static readonly int Run = Animator.StringToHash("Run");
    
    public static readonly int Hit = Animator.StringToHash("Hit");
    public static readonly int Die = Animator.StringToHash("Die");
    public static readonly int Attack = Animator.StringToHash("Attack");
    
    public static readonly int Dash = Animator.StringToHash("Dash");
    public static readonly int Rush = Animator.StringToHash("Rush");
    
    public static readonly int Defence = Animator.StringToHash("Defence");
    public static readonly int Escape = Animator.StringToHash("Escape");

    // 궁수 전용 어디에 두는 것이 맞을까? - 일단 공통으로 쓰도록 처리
    public static readonly int Spree = Animator.StringToHash("Spree");

    public Animator animator { get; private set; }

    public EnemyAnimationHandler(Animator animator)
    {
        this.animator = animator;
    }
    
    public void Set(int triggerNameHash)
    {
        animator.SetTrigger(triggerNameHash);
    }
    
    public void Set<T>(int triggerNameHash, T value)
    {
        switch (value)
        {
            case bool boolValue: animator.SetBool(triggerNameHash, boolValue); break;
            case int intValue: animator.SetInteger(triggerNameHash, intValue); break;
            case float floatValue: animator.SetFloat(triggerNameHash, floatValue); break;
            case string stringValue: animator.SetTrigger(triggerNameHash); break;
            default: Debug.LogError("animation handler : fail to change animator trigger"); break;
        }
    }
}