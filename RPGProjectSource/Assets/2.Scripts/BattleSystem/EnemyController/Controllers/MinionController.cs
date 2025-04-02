using UnityEngine;

public class MinionController: EnemyController
{
    public float tracingRange;
    
    private void Start()
    {
        HitBox hitBox = ProjectileManager.Instance.RegisterMeleeAttack(transform, Vector3.up + Vector3.forward * 1.6f,  Vector3.one);
        hitBox.onTrigger += (other) =>
        {
            if (!other.TryGetComponent(out PlayerHealth damagable)) return;
            
            damagable.TakeDamage(resourceHandler.attack, transform);
            hitBox.gameObject.SetActive(false);
        };
        
        behaviourTree.Generate(this, new SelectorNode(
            new SequenceNode(new DieNode(), new HitNode()),
            new SequenceNode(new TracingNode(tracingRange), new MeleeAttackNode()),
            new SequenceNode(new IdleNode(1, tracingRange), new PatrolNode(1))
        ));
    }
}