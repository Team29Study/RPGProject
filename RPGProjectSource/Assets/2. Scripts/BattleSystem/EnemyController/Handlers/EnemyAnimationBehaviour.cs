using UnityEngine;

public class EnemyAnimationBehaviour : StateMachineBehaviour
{
    // 애니메이션 상태가 시작될 때 실행
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("애니메이션 상태 진입: ");
    }

    // 애니메이션 상태가 업데이트될 때 실행
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Mathf.Approximately(stateInfo.normalizedTime % 1, 1))
        {
            Debug.Log("애니메이션 종료 직전 (루프 애니메이션의 끝)");
        }
        
        // Debug.Log("애니메이션 진행 중...");
    }

    // 애니메이션 상태가 종료될 때 실행
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("애니메이션 상태 종료");
    }
}