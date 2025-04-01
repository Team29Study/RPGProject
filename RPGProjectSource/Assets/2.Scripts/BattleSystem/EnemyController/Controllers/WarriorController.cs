using UnityEngine;

public class WarriorController: EnemyController
{
    public bool isDefenced = false;
    public float tracingRange;

    private void Start()
    {
        ProjectileManager.Instance.RegisterMeleeAttack(transform, Vector3.up + Vector3.forward,  Vector3.one * 2);

        behaviourTree.Generate(this, new SelectorNode(
        new SequenceNode(new HitNode(), new DieNode()),
        new SequenceNode(new IdleNode(1, tracingRange), new PatrolNode(1)), // movementHandler로 처리
        new SequenceNode(new TracingNode(tracingRange), new DefenceNode(), new MeleeAttackNode())
        ));
    }
    
    // public override void TakeDamage(int damage, Transform attackTr = null) {}
}