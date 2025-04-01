using UnityEngine;

public class MeleeAttackNode : BTNode
{
    
    public override void Start()
    {
        
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > agent.stoppingDistance)
        {
            SetState(State.Failure);
            return;
        }

        agent.enabled = false;
        Vector3 direction = target.position - transform.position; direction.y = 0;
        
        transform.rotation = Quaternion.LookRotation(direction.normalized);
        
        controller.animationHandler.Set(EnemyAnimationHandler.Move, 0f);
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        
    }

    public override void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > agent.stoppingDistance)
        {
            SetState(State.Failure);
            return;
        }
    }

    public override void End()
    {
        agent.enabled = true;
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, false);
    }
    
    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateMeleeAttack(transform, isAttacking);
    }
}