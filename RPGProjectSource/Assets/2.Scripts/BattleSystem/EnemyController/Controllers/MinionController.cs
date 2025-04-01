using UnityEngine;

public class MinionController: EnemyController
{
    public float tracingRange;
    
    private void Start()
    {
        ProjectileManager.Instance.RegisterMeleeAttack(transform, Vector3.up + Vector3.forward,  Vector3.one * 2);
        
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new DieNode(), new HitNode()),
            new SequenceNode(new TracingNode(tracingRange), new MeleeAttackNode()),
            new SequenceNode(new IdleNode(1, tracingRange), new PatrolNode(1))
        ));
    }
}