using UnityEngine;

public class EnemyAnimationBehaviour : StateMachineBehaviour
{
    public enum Status { Enter, Exit }
    // 애니메이션 상태가 시작될 때 실행
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BehaviourTree behaviourTree = animator.GetComponent<EnemyController>().behaviourTree;
        behaviourTree.currentnode?.OnAnimated(Status.Enter, stateInfo);
    }

    // 애니메이션 상태가 업데이트될 때 실행
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Mathf.Approximately(stateInfo.normalizedTime % 1, 1))
        {
        }
        // Debug.Log("애니메이션 진행 중...");
    }

    // 애니메이션 상태가 종료될 때 실행
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BehaviourTree behaviourTree = animator.GetComponent<EnemyController>().behaviourTree;
        behaviourTree.currentnode?.OnAnimated(Status.Exit, stateInfo);
    }
}