using UnityEngine;

public class MeleeAttackNode : BTNode
{
    public override void Start()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        Vector3 direction = target.position - transform.position; direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction.normalized);
        
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) SetState(State.Failure);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, isAttacking);
    }
}