using UnityEngine;

public class MageAttack : BTNode
{
    public override void Start()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        controller.animationHandler.Set(EnemyAnimationHandler.Attack, true);
        
    }

    public override void Update()
    {
        Vector3 direction = target.position - transform.position; direction.y = 0; // 공격하면서 회전 필요
        transform.rotation = Quaternion.LookRotation(direction.normalized);

        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) SetState(State.Failure);
    }

    public override void OnAttackAnimated(bool isAttacking)
    {
        ProjectileManager.Instance.CreateRangeAttack(target.transform, 1, HitBox.Caster.Enemy);
    }
}